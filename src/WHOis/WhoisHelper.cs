using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WHOis
{
    public static class WhoisHelper
    {
        static TcpClient _tcpWhois;
        static NetworkStream _nsWhois;
        static BufferedStream _bfWhois;
        static StreamWriter _strmSend;
        static StreamReader _strmRecive;

        public static event EventHandler<WhoisEventArgs> WhoisLog;
        private static void OnWhoisLog(WhoisEventArgs e)
        {
            WhoisLog?.Invoke(null, e);
        }


        /// <summary>
        /// WHOis a domain to know is reserved or not 
        /// </summary>
        /// <param name="name">domain name</param>
        /// <param name="postfix">domain extension</param>
        /// <param name="server">server url</param>
        /// <returns>False if reserved and True if free</returns>
        public static async Task<bool> Whoise(string name, string postfix, string server)
        {
            return await Task.Run(() =>
            {
                string result = "";
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
                            result += response + "\r\n";

                            if (result.Contains("No match for ") || result.Contains("no entries found"))
                                break;
                        }
                    }
                    catch (Exception exp)
                    {
                        OnWhoisLog(new WhoisEventArgs(name, postfix, server, true, "WHOis Server Error: {0}", exp.Message));
                    }
                }
                catch (Exception exp)
                {
                    OnWhoisLog(new WhoisEventArgs(name, postfix, server, true,
                        "No Internet Connection or Any other Fault.{1} Error: {0}", exp.Message, Environment.NewLine));
                }

                //SEND THE WHO_IS SERVER ABOUT THE HOSTNAME
                finally
                {
                    try
                    {
                        _tcpWhois.Close();
                    }
                    catch (Exception exp)
                    {
                        OnWhoisLog(new WhoisEventArgs(name, postfix, server, false, "Connection Closing Error: {0}",
                            exp.Message));
                    }
                }

                return result.Contains("No match for") || result.Contains("no entries found");
            });
        }

        /// <summary>
        /// WHOis a domain to know is reserved or not 
        /// </summary>
        /// <param name="name">domain name</param>
        /// <param name="postfix">domain extension</param>
        /// <param name="server">server url</param>
        /// <returns>False if reserved and True if free</returns>
        public static async Task<CheckState> WhoiseCheckState(string name, string postfix, string server)
        {
            return await Task.Run(() =>
            {
                string result = "";
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
                            result += response + "\r\n";

                            if (result.Contains("No match for ") || result.Contains("no entries found"))
                                break;
                        }
                    }
                    catch (Exception exp)
                    {
                        OnWhoisLog(new WhoisEventArgs(name, postfix, server, true, "WHOis Server Error: {0}", exp.Message));
                        return CheckState.Unchecked;
                    }
                }
                catch (Exception exp)
                {
                    OnWhoisLog(new WhoisEventArgs(name, postfix, server, true,
                        "No Internet Connection or Any other Fault.{1} Error: {0}", exp.Message, Environment.NewLine));

                    return CheckState.Unchecked;
                }

                //SEND THE WHO_IS SERVER ABOUT THE HOSTNAME
                finally
                {
                    try
                    {
                        _tcpWhois.Close();
                    }
                    catch (Exception exp)
                    {
                        OnWhoisLog(new WhoisEventArgs(name, postfix, server, false, "Connection Closing Error: {0}",
                            exp.Message));
                    }
                }

                return result.Contains("No match for") || result.Contains("no entries found")
                    ? CheckState.Checked
                    : CheckState.Indeterminate;
            });
        }

        /// <summary>
        /// Whois a domain to fetch reserver info
        /// </summary>
        /// <param name="name">domain name</param>
        /// <param name="postfix">domain extension</param>
        /// <param name="server">server url</param>
        /// <returns>reserver information</returns>
        public static async Task<string> WhoiseInfo(string name, string postfix, string server)
        {
            return await Task.Run(() =>
            {
                string result = "";
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
                            result += response + "\r\n";
                        }
                    }
                    catch (Exception exp)
                    {
                        OnWhoisLog(new WhoisEventArgs(name, postfix, server, true, "WHOis Server Error: {0}", exp.Message));
                    }
                }
                catch (Exception exp)
                {
                    OnWhoisLog(new WhoisEventArgs(name, postfix, server, true,
                        "No Internet Connection or Any other Fault.{1} Error: {0}", exp.Message, Environment.NewLine));
                }

                //SEND THE WHO_IS SERVER ABOUT THE HOSTNAME
                finally
                {
                    try
                    {
                        _tcpWhois.Close();
                    }
                    catch (Exception exp)
                    {
                        OnWhoisLog(new WhoisEventArgs(name, postfix, server, false, "Connection Closing Error: {0}",
                            exp.Message));
                    }
                }

                return result;
            });
        }

    }
}
