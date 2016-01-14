using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetTools;

namespace Whois.NET.Internal
{
    public class IPAddrToWhoisSvrMap
    {
        public IPAddressRange IPAddressRange { get; set; }

        public string WhoisServer { get; set; }
    }
}
