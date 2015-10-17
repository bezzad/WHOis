using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;

namespace WHOis
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource _cts;
        private CancellationTokenSource _ctsOnDoubleClick;

        readonly List<string> _selectedExtensions;
        public int PrintColumnWidth = 30;

        public MainForm()
        {
            InitializeComponent();

            _selectedExtensions = new List<string>();

            this.Text = $"WHOis  Onlin Domain Database   ver {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
        }


        private async void btnLookUp_Click(object sender, EventArgs e)
        {
            try
            {
                InvokeIfRequire(() => dgvResult.Rows.Clear());
                _cts = new CancellationTokenSource();
                btnPreCompile.PerformClick();
                string[] names = GetNamesByPreCompile(txtHostName.Text);
                UiActivation(false);


                if (names.Length == 0 || _selectedExtensions.Count == 0) return;

                InvokeIfRequire(() => progResult.Maximum = names.Length * _selectedExtensions.Count);
                InvokeIfRequire(() => progResult.Value = 0);
                InvokeIfRequire(() => lblProcessPercent.Text = $"0 / {names.Length * _selectedExtensions.Count} domain");

                await Task.Run(() =>
                {
                    foreach (var url in names)
                    {
                        if (_cts.IsCancellationRequested) return;

                        InvokeIfRequire(() => dgvResult.Rows.Add(url));
                    }
                });

                await WhoisParallel();
            }
            finally
            {
                UiActivation(true);
            }
        }

        private async Task WhoisParallel()
        {
            string server = cmbServer.Text;
            List<Task> tasks = new List<Task>();

            foreach (var extension in _selectedExtensions)
            {
                if (_cts.IsCancellationRequested) return;

                tasks.Add(Task.Run(async () =>
                {
                    var whoisHost = server;

                    if (extension.Equals("ir", StringComparison.OrdinalIgnoreCase))
                    {
                        whoisHost = "whois.nic.ir";
                    }

                    foreach (DataGridViewRow row in dgvResult.Rows)
                    {
                        if (_cts.IsCancellationRequested) return;

                        var whois = new WhoisHelper();
                        var res = await whois.WhoiseCheckState(row.Cells[0].Value.ToString(), extension, whoisHost, false, _cts.Token);
                        SetCellStyle(row.Cells[extension], res);
                        InvokeIfRequire(() => progResult.Value++);
                        InvokeIfRequire(() => lblProcessPercent.Text = $"{progResult.Value} / {dgvResult.Rows.Count * _selectedExtensions.Count} domain");
                    }
                }));
            }

            await Task.Run(() => Task.WaitAll(tasks.ToArray()));
        }

        private async void dgvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (_ctsOnDoubleClick == null || _ctsOnDoubleClick.IsCancellationRequested)
                _ctsOnDoubleClick = new CancellationTokenSource();

            DataGridViewCell cell = dgvResult.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.Style.BackColor = System.Drawing.Color.OrangeRed;

            if (!string.IsNullOrEmpty(cell.ErrorText))
            {
                string server = cmbServer.Text;

                if (cell.OwningColumn.HeaderText.Equals(".ir", StringComparison.OrdinalIgnoreCase))
                {
                    server = "whois.nic.ir";
                }

                var whois = new WhoisHelper();
                var res = await whois.WhoiseCheckState(cell.OwningRow.Cells[0].Value.ToString(),
                    cell.OwningColumn.HeaderText.Substring(1), server, false, _ctsOnDoubleClick.Token);

                SetCellStyle(cell, res);
            }
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
                    ThreeState = true,
                    SortMode = DataGridViewColumnSortMode.Automatic
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
        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
            _ctsOnDoubleClick?.Cancel();
        }
        private void btnPreCompile_Click(object sender, EventArgs e)
        {
            var names = GetNamesByPreCompile(txtHostName.Text);
            txtHostName.Text = string.Join("\r\n", names);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            var dlgSave = new SaveFileDialog
            {
                FileName = "WhoisResult.txt",
                CheckPathExists = true,
                DefaultExt = "Text Files *.Text|",
                Title = "Save WHOis Result Browser"
            };
            if (dlgSave.ShowDialog(this) == DialogResult.OK)
            {
                string[] result = GetDisplayResult();
                File.WriteAllLines(dlgSave.FileName, result);
                Process.Start(dlgSave.FileName);
            }
        }

        private void SetCellStyle(DataGridViewCell cell, WhoisInfo data)
        {
            InvokeIfRequire(() => cell.Value = data.ReserveState);

            if (data.ReserveState == CheckState.Unchecked)
            {
                InvokeIfRequire(() => cell.Style.BackColor = System.Drawing.Color.Coral);
                InvokeIfRequire(() => cell.ErrorText = data?.ErrorLogArgs.Message ?? " ");
                InvokeIfRequire(() => cell.ErrorText += "\r\n Double Click to rewhois");
            }
            else if (data.ReserveState == CheckState.Checked)
            {
                InvokeIfRequire(() => cell.Style.BackColor = System.Drawing.Color.LightGreen);
                InvokeIfRequire(() => cell.ErrorText = null);
            }
            else if (data.ReserveState == CheckState.Indeterminate)
            {
                InvokeIfRequire(() => cell.Style.BackColor = System.Drawing.Color.LightPink);
                InvokeIfRequire(() => cell.ErrorText = null);
            }
        }
        private void UiActivation(bool active)
        {
            InvokeIfRequire(() => cmbServer.Enabled = active);
            InvokeIfRequire(() => grbExtensions.Enabled = active);
            InvokeIfRequire(() => btnLookUp.Enabled = active);
            InvokeIfRequire(() => btnPreCompile.Enabled = active);
            InvokeIfRequire(() => btnCancel.Enabled = !active);
            InvokeIfRequire(() => txtHostName.ReadOnly = !active);
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
        private string[] GetNamesByPreCompile(string text)
        {
            var names = text.Split(new[]
            {
                "\r\n",
                ",",
                ":",
                "'",
                "\"",
                ";",
                ".",
                "&",
                "~",
                "!",
                "@",
                "#",
                "$",
                "%",
                "^",
                "*",
                "(",
                ")",
                "-",
                "+",
                "_",
                "+",
                "=",
                @"\",
                "|",
                "{",
                "}",
                "`",
                "?",
                "/",
                "<",
                ">",
                "[",
                "]",
                " "
            }, StringSplitOptions.RemoveEmptyEntries);

            List<string> preCompiledNames = new List<string>();
            foreach (var name in names)
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) continue;

                preCompiledNames.Add(name);
            }

            return preCompiledNames.ToArray();
        }
        private string[] GetDisplayResult()
        {
            var rows = new List<string>();
            var buffer = "";
            var line = "";

            foreach (DataGridViewColumn col in dgvResult.Columns)
            {
                buffer += AddFlowChar($"|  {col.HeaderText} ", PrintColumnWidth, ' ') + "\t";
            }
            rows.Add(buffer); // add header

            // create header line acourding header char lenght like: =============
            line = AddFlowChar("|", (dgvResult.Columns.Count - 1) * PrintColumnWidth, '=');
            rows.Add(line); // add header line

            line = line.Replace("=", "--"); // other lines like: ---------------------

            foreach (DataGridViewRow row in dgvResult.Rows)
            {
                buffer = "";

                foreach (DataGridViewColumn col in dgvResult.Columns)
                {
                    var val = row.Cells[col.Name].Value.ToString();

                    if (val == CheckState.Indeterminate.ToString())
                        val = "Reserved";
                    else if (val == CheckState.Checked.ToString())
                        val = "Free";
                    else if (val == CheckState.Unchecked.ToString())
                        val = "Error";

                    buffer += AddFlowChar($"|  {val}", PrintColumnWidth, ' ') + "\t"; ;
                }

                rows.Add(buffer);
                rows.Add(line);
            }

            return rows.ToArray();
        }
        private string AddFlowChar(string item, int fitLength, char flow)
        {
            for (fitLength = fitLength - item.Length; fitLength > 0; fitLength--)
            {
                item += flow.ToString();
            }

            return item;
        }

    }
}
