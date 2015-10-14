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
        TcpClient tcpWhois;
        NetworkStream nsWhois;
        BufferedStream bfWhois;
        StreamWriter strmSend;
        StreamReader strmRecive;

        List<string> selectedExtensions;

        public MainForm()
        {
            InitializeComponent();

            selectedExtensions = new List<string>();
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
            string ext = chk.Name.Substring(3).ToLower();

            if (chk.Checked)
            {
                DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn(false);
                col.Name = ext;
                col.HeaderText = "." + ext;
                col.ReadOnly = true;
                col.ThreeState = true;

                dgvResult.Columns.Add(col);

                selectedExtensions.Add(ext);
            }
            else
            {
                selectedExtensions.Remove(ext);

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

            string[] names = txtHostName.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            if (names.Length == 0 || selectedExtensions.Count == 0) return;

            InvokeIfRequire(() => progResult.Maximum = names.Length * selectedExtensions.Count);
            InvokeIfRequire(() => progResult.Value = 0);

            foreach (string name in names)
            {
                var url = name.Replace(" ", "");

                Dictionary<string, bool> extensionReserving = new Dictionary<string, bool>();

                foreach (var extension in selectedExtensions)
                {
                    string server = cmbServer.SelectedItem.ToString();

                    if (extension.Equals("ir", StringComparison.OrdinalIgnoreCase))
                    {
                        server = "whois.nic.ir";
                    }

                    var res = await Whoise(url, extension, server);

                    extensionReserving.Add(extension, res);

                    InvokeIfRequire(() => progResult.Value++);
                }

                Add(url, extensionReserving);
            }

            UiActivation(true);
        }


        private void Add(string domain, Dictionary<string, bool> extensions)
        {
            int row = dgvResult.Rows.Add();
            dgvResult.Rows[row].Cells["colDomain"].Value = domain;

            foreach (var e in extensions.Keys)
            {
                var cell = ((DataGridViewCheckBoxCell)dgvResult.Rows[row].Cells[e]);
                cell.Value = extensions[e] ? CheckState.Checked : CheckState.Indeterminate;
                cell.Style.BackColor = extensions[e] ? System.Drawing.Color.LightGreen : System.Drawing.Color.LightPink;
            }
        }

        /// <summary>
        /// WHOis a domain to know is reserved or not 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="postfix"></param>
        /// <param name="server"></param>
        /// <returns>False if reserved and True if free</returns>
        private async Task<bool> Whoise(string name, string postfix, string server)
        {
            return await Task.Run(() =>
             {
                 string result = "";
                 try
                 {
                     //CONNECT TO TCP CLIENT OF WHOIS
                     tcpWhois = new TcpClient(server, 43);

                     //SETUP THE NETWORK STREAM
                     nsWhois = tcpWhois.GetStream();

                     //GET THE DATA IN THE BUFFER FROM THE NETWORK STREAM
                     bfWhois = new BufferedStream(nsWhois);

                     strmSend = new StreamWriter(bfWhois);

                     strmSend.WriteLine(name + "." + postfix);

                     strmSend.Flush();

                     try
                     {
                         strmRecive = new StreamReader(bfWhois);
                         string response;

                         while ((response = strmRecive.ReadLine()) != null)
                         {
                             result += response + "\r\n";

                             if (result.Contains("No match for ") || result.Contains("no entries found"))
                                 break;
                         }
                     }
                     catch (Exception exp)
                     {
                         Log(string.Format("WHOis Server Error: {0}", exp.Message));
                     }
                 }
                 catch (Exception exp)
                 {
                     Log(string.Format("No Internet Connection or Any other Fault. Error: {0}", exp.Message));
                 }

                 //SEND THE WHO_IS SERVER ABOUT THE HOSTNAME
                 finally
                 {
                     try
                     {
                         tcpWhois.Close();
                     }
                     catch (Exception exp)
                     {
                         Log(string.Format("Connection Closing Error: {0}", exp.Message));
                     }
                 }

                 return result.Contains("No match for ") || result.Contains("no entries found");
             });
        }

        private void Log(string msg)
        {
            InvokeIfRequire(() => txtLogger.Text += string.Format("{0}{1} {0}", Environment.NewLine, msg));
        }

        public void InvokeIfRequire(Action act)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(act);
            }
            else
            {
                act();
            }
        }
    }
}
