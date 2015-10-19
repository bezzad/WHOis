using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WHOis
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource _cts;
        private CancellationTokenSource _ctsOnDoubleClick;
        readonly List<string> _selectedExtensions;
        bool closing = false;
        object _lockObj = new object();

        public MainForm()
        {
            InitializeComponent();

            _selectedExtensions = new List<string>();

            this.Text = $"WHOis  Online Domain Database   version {Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
        }


        private async void btnLookUp_Click(object sender, EventArgs e)
        {
            try
            {
                InvokeIfRequire(() => dgvResult.Rows.Clear());
                _cts = new CancellationTokenSource();
                btnPreCompile.PerformClick();
                string[] _names = txtHostName.Text.GetNamesByPreCompile();
                InvokeIfRequire(() => lblNamesCounter.Text = _names.Length.ToString());
                UiActivation(false);


                if (_names.Length == 0 || _selectedExtensions.Count == 0) return;

                InvokeIfRequire(() => progResult.Maximum = _names.Length * _selectedExtensions.Count);
                IncreaseProgress(true);

                await Task.Run(() =>
                {
                    foreach (var url in _names)
                    {
                        if (_cts.IsCancellationRequested) return;

                        InvokeIfRequire(() => dgvResult.Rows.Add(url));
                    }
                });

                var server = cmbServer.Text;
                var tasks = new List<Task>();
                var lstRows = new List<DataGridViewRow>();

                foreach (DataGridViewRow row in dgvResult.Rows)
                {
                    lstRows.Add(row);
                }

                WhoisHelper.Progress += WhoisHelper_Progress;
                await WhoisHelper.WhoisParallel(lstRows, _selectedExtensions, server, _cts);
            }
            finally
            {
                InvokeIfRequire(() => progResult.Value = progResult.Maximum);
                MessageBox.Show("Whois Completed");
                UiActivation(true);
            }
        }
        private void WhoisHelper_Progress(object sender, WhoisInfo e)
        {
            SetCellStyle(e);
            IncreaseProgress();
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
        private async void dgvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (_ctsOnDoubleClick == null || _ctsOnDoubleClick.IsCancellationRequested)
                _ctsOnDoubleClick = new CancellationTokenSource();

            DataGridViewCell cell = dgvResult.Rows[e.RowIndex].Cells[e.ColumnIndex];
            await UpdateCell(cell);
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
            var names = txtHostName.Text.GetNamesByPreCompile();
            InvokeIfRequire(() => lblNamesCounter.Text = names.Length.ToString());
            InvokeIfRequire(() => txtHostName.Text = string.Join("\r\n", names));
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            var dlgSave = new SaveFileDialog
            {
                FileName = "WhoisResult.csv",
                CheckPathExists = true,
                DefaultExt = "CSV Files *.csv|",
                Title = "Save WHOis Result Browser"
            };
            if (dlgSave.ShowDialog(this) == DialogResult.OK)
            {
                string[] result = dgvResult.PrintGridViewToCsv();
                File.WriteAllLines(dlgSave.FileName, result);
                Process.Start(dlgSave.FileName);
            }
        }
        private async void btnReTryErrorCells_Click(object sender, EventArgs e)
        {
            try
            {
                UiActivation(false);

                if (_ctsOnDoubleClick == null || _ctsOnDoubleClick.IsCancellationRequested)
                    _ctsOnDoubleClick = new CancellationTokenSource();

                foreach (DataGridViewRow row in dgvResult.Rows)
                {
                    for (int col = 1; col < dgvResult.Columns.Count; col++)
                    {
                        var cell = row.Cells[col];

                        if (cell.Value is CheckState && (CheckState)cell.Value == CheckState.Unchecked && !string.IsNullOrEmpty(cell.ErrorText))
                            await UpdateCell(cell);
                    }
                }
            }
            finally
            {
                UiActivation(true);
            }

        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            closing = true;
        }


        private void IncreaseProgress(bool setZero = false)
        {
            lock (_lockObj)
            {
                if (setZero)
                    InvokeIfRequire(() => progResult.Value = 0);
                else if (progResult.Value < progResult.Maximum)
                    InvokeIfRequire(() => progResult.Value++);
            }
            InvokeIfRequire(() => lblProcessPercent.Text = $"{progResult.Value} / {progResult.Maximum} domain");
        }
        private async Task UpdateCell(DataGridViewCell cell)
        {
            cell.Style.BackColor = System.Drawing.Color.OrangeRed;

            if (!string.IsNullOrEmpty(cell.ErrorText))
            {
                string server = cmbServer.Text;

                var res = await Task.Run(() => WhoisHelper.WhoisExtension(cell.OwningRow, cell.OwningColumn.HeaderText.Substring(1), server));

                SetCellStyle(res);
            }
        }
        private void SetCellStyle(WhoisInfo data)
        {
            InvokeIfRequire(() => data.Cell.Value = data.ReserveState);

            if (data.ReserveState == CheckState.Unchecked)
            {
                InvokeIfRequire(() => data.Cell.Style.BackColor = System.Drawing.Color.Coral);
                InvokeIfRequire(() => data.Cell.ErrorText = data?.ErrorLogArgs.Message ?? " ");
                InvokeIfRequire(() => data.Cell.ErrorText += "\r\n Double Click to rewhois");
            }
            else if (data.ReserveState == CheckState.Checked)
            {
                InvokeIfRequire(() => data.Cell.Style.BackColor = System.Drawing.Color.LightGreen);
                InvokeIfRequire(() => data.Cell.ErrorText = null);
            }
            else if (data.ReserveState == CheckState.Indeterminate)
            {
                InvokeIfRequire(() => data.Cell.Style.BackColor = System.Drawing.Color.LightPink);
                InvokeIfRequire(() => data.Cell.ErrorText = null);
            }
        }
        private void UiActivation(bool active)
        {
            InvokeIfRequire(() => cmbServer.Enabled = active);
            InvokeIfRequire(() => grbExtensions.Enabled = active);
            InvokeIfRequire(() => btnLookUp.Enabled = active);
            InvokeIfRequire(() => btnReTryErrorCells.Enabled = active);
            InvokeIfRequire(() => btnPreCompile.Enabled = active);
            InvokeIfRequire(() => btnCancel.Enabled = !active);
            InvokeIfRequire(() => txtHostName.ReadOnly = !active);
        }
        private void InvokeIfRequire(Action act)
        {
            try
            {
                if (InvokeRequired && !closing)
                {
                    Invoke(act);
                }
                else if (!closing)
                {
                    act();
                }
            }
            catch (Exception) { }
        }


    }
}
