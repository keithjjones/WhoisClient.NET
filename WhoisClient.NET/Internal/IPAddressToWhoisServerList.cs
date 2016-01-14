using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NetTools;
using System.Net;

namespace Whois.NET.Internal
{
    public class IPAddressToWhoisServerList : IEnumerable<IPAddrToWhoisSvrMap>
    {
        private static object _sync = new object();

        private static IPAddressToWhoisServerList _Default;

        public static IPAddressToWhoisServerList Default
        {
            get {
                if (_Default == null)
                {
                    lock (_sync)
                    {
                        if (_Default == null)
                        {
                            _Default = new IPAddressToWhoisServerList { 
                            {"58.0.0.0-61.255.255.255",	    "whois.apnic.net"},
                            {"62.0.0.0-90.255.255.255",	    "whois.ripe.net"},
                            {"124.0.0.0-126.255.255.255",   "whois.apnic.net"}, 
                            {"189.0.0.0/8",			        "whois.lacnic.net"},
                            {"190.0.0.0/8",			        "whois.lacnic.net"},
                            {"193.0.0.0-195.255.255.255",	"whois.ripe.net"},
                            {"200.0.0.0/8",			        "whois.lacnic.net"},
                            {"201.0.0.0/8",			        "whois.lacnic.net"}, 
                            {"202.0.0.0/8",			        "whois.apnic.net"},
                            {"203.0.0.0/8",			        "whois.apnic.net"},
                            {"210.0.0.0/8",			        "whois.apnic.net"},
                            {"211.0.0.0/8",			        "whois.apnic.net"},
                            {"212.0.0.0-217.255.255.255",	"whois.ripe.net"},
                            {"213.0.0.0/8",			        "whois.ripe.net"},
                            {"217.0.0.0/8",			        "whois.ripe.net"},
                            {"218.0.0.0-222.255.255.255",	"whois.apnic.net"},
                            {"0.0.0.0/0",			        "whois.arin.net"} // Default is ARIN.
                            };
                        }
                    }
                }
                return _Default;
            }
        }

        protected List<IPAddrToWhoisSvrMap> _List;

        public IPAddressToWhoisServerList()
        {
            this._List = new List<IPAddrToWhoisSvrMap>();
        }

        public void Add(string ipRangeString, string whoisServerName)
        {
            _List.Add(new IPAddrToWhoisSvrMap
            {
                IPAddressRange = new IPAddressRange(ipRangeString),
                WhoisServer = whoisServerName
            });
        }

        public string FindWhoisServer(IPAddress ipAddress)
        {
            var map = this.FirstOrDefault(a => a.IPAddressRange.Contains(ipAddress));
            return map == null ? null : map.WhoisServer;
        }

        public IEnumerator<IPAddrToWhoisSvrMap> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _List.GetEnumerator();
        }
    }
}
