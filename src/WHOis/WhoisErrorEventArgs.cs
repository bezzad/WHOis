using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHOis
{
    public class WhoisErrorEventArgs
    {
        public string Message { get; protected set; }

        public string DomainName { get; protected set; }

        public string DomainExtension { get; protected set; }

        public string ServerUrl { get; protected set; }

        public bool ReWhoisRequired { get; protected set; }


        public WhoisErrorEventArgs(string domain, string postfix, string server, bool reWhois, string logFormat, params object[] args)
        {
            Message = string.Format(logFormat, args);
            DomainName = domain;
            DomainExtension = postfix;
            ServerUrl = server;
            ReWhoisRequired = reWhois;
        }
    }
}
