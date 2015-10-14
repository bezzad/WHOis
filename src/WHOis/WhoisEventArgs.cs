using System;

namespace WHOis
{
    public class WhoisEventArgs : EventArgs
    {
        public string Message { get; protected set; }

        public string DomainName { get; protected set; }

        public string DomainExtension { get; protected set; }

        public string ServerUrl { get; protected set; }

        public bool ReWhoisRequired { get; protected set; }


        public WhoisEventArgs(string domain, string postfix, string server, bool reWhois, string logFormat, params object[] args)
        {
            Message = string.Format(logFormat, args);
            DomainName = domain;
            DomainExtension = postfix;
            ServerUrl = server;
            ReWhoisRequired = reWhois;
        }
    }
}
