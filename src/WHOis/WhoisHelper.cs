using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHOis
{
    public class WhoisHelper
    {
        TcpClient _tcpWhois;
        NetworkStream _nsWhois;
        BufferedStream _bfWhois;
        StreamWriter _strmSend;
        StreamReader _strmRecive;

        public event EventHandler<WhoisEventArgs> WhoisLog;
        private void OnWhoisLog(WhoisEventArgs e)
        {
            WhoisLog?.Invoke(null, e);
        }


        /// <summary>
        /// WHOis a domain to know is reserved or not 
        /// </summary>
        /// <param name="name">domain name</param>
        /// <param name="postfix">domain extension</param>
        /// <param name="server">server url</param>
        /// <param name="fullResponse">Set Whois info completely</param>
        /// <param name="ct">Cancellation Token</param>
        /// <returns>False if reserved and True if free</returns>
        public async Task<WhoisInfo> WhoiseCheckState(string name, string postfix, string server, bool fullResponse, System.Threading.CancellationToken ct)
        {
            return await Task.Run(() =>
            {
                var result = new WhoisInfo();                

                try
                {
                    //CONNECT TO TCP CLIENT OF WHOIS
                    _tcpWhois = new TcpClient(server, 43);

                    //SETUP THE NETWORK STREAM
                    _nsWhois = _tcpWhois.GetStream();

                    //GET THE DATA IN THE BUFFER FROM THE NETWORK STREAM
                    _bfWhois = new BufferedStream(_nsWhois);

                    _strmSend = new StreamWriter(_bfWhois);

                    _strmSend.WriteLine(name + "." + postfix);
                    _strmSend.Flush();


                    try
                    {
                        _strmRecive = new StreamReader(_bfWhois);
                        string response;

                        while ((response = _strmRecive.ReadLine()) != null)
                        {
                            result.Info += response + "\r\n";

                            if (!fullResponse &&
                                (result.Info.Contains("No match for ") || result.Info.Contains("no entries found")))
                                break;
                        }
                    }
                    catch (Exception exp)
                    {
                        result.ErrorLogArgs = new WhoisEventArgs(name, postfix, server, true, "WHOis Server Error: {0}",
                            exp.Message);

                        OnWhoisLog(result.ErrorLogArgs);
                        result.ReserveState = CheckState.Unchecked;
                        return result;
                    }
                }
                catch (Exception exp)
                {
                    result.ErrorLogArgs = new WhoisEventArgs(name, postfix, server, true,
                        "No Internet Connection or Any other Fault.{1} Error: {0}", exp.Message, Environment.NewLine);

                    OnWhoisLog(result.ErrorLogArgs);
                    result.ReserveState = CheckState.Unchecked;
                    return result;
                }

                //SEND THE WHO_IS SERVER ABOUT THE HOSTNAME
                finally
                {
                    try
                    {
                        _tcpWhois?.Close();
                    }
                    catch (Exception exp)
                    {
                        result.ErrorLogArgs = new WhoisEventArgs(name, postfix, server, false,
                            "Connection Closing Error: {0}",
                            exp.Message);

                        OnWhoisLog(result.ErrorLogArgs);
                    }
                }

                if (string.IsNullOrEmpty(result.Info))
                {
                    result.ReserveState = CheckState.Unchecked;
                    result.ErrorLogArgs = new WhoisEventArgs(name, postfix, server, true, "No Response: Unknown exception caused to couldn't response from server!");
                    OnWhoisLog(result.ErrorLogArgs);
                }
                else
                {
                    result.ReserveState = result.Info.Contains("No match for") ||
                                          result.Info.Contains("no entries found")
                        ? CheckState.Checked
                        : CheckState.Indeterminate;
                }

                return result;
            }, ct);
        }
    }
}
