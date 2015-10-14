using System;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WHOis
{
    public partial class MainForm : Form
    {
        readonly List<string> _selectedExtensions;

        public MainForm()
        {
            InitializeComponent();

            _selectedExtensions = new List<string>();
            WhoisHelper.WhoisLog += WhoisHelper_WhoisLog;
        }

        private async void WhoisHelper_WhoisLog(object sender, WhoisEventArgs e)
        {
            Log(e.Message);
            //await Task.Run(() => );
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

        private void UiActivation(bool active)
        {
            InvokeIfRequire(() => cmbServer.Enabled = active);
            InvokeIfRequire(() => grbExtensions.Enabled = active);
        }

        private async void btnLookUp_Click(object sender, EventArgs e)
        {
            InvokeIfRequire(() => dgvResult.Rows.Clear());
            UiActivation(false);

            string[] names = txtHostName.Text.Replace(" ", "\r\n").Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (names.Length == 0 || _selectedExtensions.Count == 0) return;

            InvokeIfRequire(() => progResult.Maximum = names.Length * _selectedExtensions.Count);
            InvokeIfRequire(() => progResult.Value = 0);

            foreach (var url in names)
            {
                var extensionReserving = new Dictionary<string, CheckState>();

                foreach (var extension in _selectedExtensions)
                {
                    string server = cmbServer.SelectedItem.ToString();

                    if (extension.Equals("ir", StringComparison.OrdinalIgnoreCase))
                    {
                        server = "whois.nic.ir";
                    }

                    var res = await WhoisHelper.WhoiseCheckState(url, extension, server);

                    extensionReserving.Add(extension, res);

                    InvokeIfRequire(() => progResult.Value++);
                }

                Add(url, extensionReserving);
            }

            UiActivation(true);
        }


        private void Add(string domain, Dictionary<string, CheckState> extensions)
        {
            var row = dgvResult.Rows.Add();
            dgvResult.Rows[row].Cells["colDomain"].Value = domain;

            foreach (var e in extensions.Keys)
            {
                var cell = ((DataGridViewCheckBoxCell)dgvResult.Rows[row].Cells[e]);
                cell.Value = extensions[e];

                if (extensions[e] == CheckState.Unchecked)
                    cell.Style.BackColor = System.Drawing.Color.Coral;
                else if (extensions[e] == CheckState.Checked)
                    cell.Style.BackColor = System.Drawing.Color.LightGreen;
                else if (extensions[e] == CheckState.Indeterminate)
                    cell.Style.BackColor = System.Drawing.Color.LightPink;
            }
        }



        private void Log(string msg)
        {
            InvokeIfRequire(() => txtLogger.Text += string.Format("{0}{1} {0}", Environment.NewLine, msg));
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
