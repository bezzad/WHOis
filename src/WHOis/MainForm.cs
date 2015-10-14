using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading;

namespace WHOis
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource _cts;
        readonly List<string> _selectedExtensions;


        public MainForm()
        {
            InitializeComponent();

            _selectedExtensions = new List<string>();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            cmbServer.SelectedIndex = 0;

            foreach (CheckBox chk in grbExtensions.Controls)
            {
                chk.CheckStateChanged += ChkDomain_CheckStateChanged;

                if (chk.Checked)
                    ChkDomain_CheckStateChanged(chk, e);
            }

        }
        private void ChkDomain_CheckStateChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox)sender;
            var ext = chk.Name.Substring(3).ToLower();

            if (chk.Checked)
            {
                var col = new DataGridViewCheckBoxColumn(false)
                {
                    Name = ext,
                    HeaderText = "." + ext,
                    ReadOnly = true,
                    ThreeState = true
                };

                dgvResult.Columns.Add(col);

                _selectedExtensions.Add(ext);
            }
            else
            {
                _selectedExtensions.Remove(ext);

                if (dgvResult.Columns.Contains(ext))
                    dgvResult.Columns.Remove(ext);
            }
        }
        private async void btnLookUp_Click(object sender, EventArgs e)
        {
            try
            {
                InvokeIfRequire(() => dgvResult.Rows.Clear());
                UiActivation(false);
                _cts = new CancellationTokenSource();
                string[] names = txtHostName.Text.Replace(" ", "\r\n").Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);


                if (names.Length == 0 || _selectedExtensions.Count == 0) return;

                InvokeIfRequire(() => progResult.Maximum = names.Length * _selectedExtensions.Count);
                InvokeIfRequire(() => progResult.Value = 0);

                foreach (var url in names)
                {
                    if (_cts.IsCancellationRequested) break;

                    var extensionReserving = new Dictionary<string, WhoisInfo>();

                    foreach (var extension in _selectedExtensions)
                    {
                        string server = cmbServer.Text;

                        if (extension.Equals("ir", StringComparison.OrdinalIgnoreCase))
                        {
                            server = "whois.nic.ir";
                        }

                        var res = await WhoisHelper.WhoiseCheckState(url, extension, server, false);

                        extensionReserving.Add(extension, res);

                        InvokeIfRequire(() => progResult.Value++);
                    }

                    Add(url, extensionReserving);
                }
            }
            finally
            {
                UiActivation(true);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts.Cancel();
        }


        private void Add(string domain, Dictionary<string, WhoisInfo> extensions)
        {
            var row = dgvResult.Rows.Add();
            dgvResult.Rows[row].Cells["colDomain"].Value = domain;

            foreach (var e in extensions.Keys)
            {
                var cell = ((DataGridViewCheckBoxCell)dgvResult.Rows[row].Cells[e]);
                cell.Value = extensions[e].ReserveState;

                if (extensions[e].ReserveState == CheckState.Unchecked)
                {
                    cell.Style.BackColor = System.Drawing.Color.Coral;
                    cell.ErrorText = extensions[e].ErrorLogArgs.Message;
                }
                else if (extensions[e].ReserveState == CheckState.Checked)
                    cell.Style.BackColor = System.Drawing.Color.LightGreen;
                else if (extensions[e].ReserveState == CheckState.Indeterminate)
                    cell.Style.BackColor = System.Drawing.Color.LightPink;
            }
        }
        private void UiActivation(bool active)
        {
            InvokeIfRequire(() => cmbServer.Enabled = active);
            InvokeIfRequire(() => grbExtensions.Enabled = active);
            InvokeIfRequire(() => btnLookUp.Enabled = active);
            InvokeIfRequire(() => btnCancel.Enabled = !active);
        }
        public void InvokeIfRequire(Action act)
        {
            if (InvokeRequired)
            {
                Invoke(act);
            }
            else
            {
                act();
            }
        }

    }
}
