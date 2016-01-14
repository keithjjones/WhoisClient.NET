using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Whois.NET.Internal
{
    public class DomainToWhoisServerList : IEnumerable<DomainToWhoisSvrMap>
    {
        private static DomainToWhoisServerList _Default;

        private static object _sync = new object();

        public static DomainToWhoisServerList Default { get {
            if (_Default == null)
            {
                lock (_sync)
                {
                    if (_Default == null)
                    {
                        _Default = new DomainToWhoisServerList
                        {
                            {"uk.com", "whois.uk.com"},
                            {"eu.org", "whois.eu.org"},
                            {"edu.cn", "whois.edu.cn"},
                            {"ac.uk", "whois.ja.net"},
                            {"gov.uk", "whois.ja.net"},
                            {"gb.com", "whois.nomination.net"},
                            {"gb.net", "whois.nomination.net"},

                            {"com", "whois.internic.net"},
                            {"jp", "whois.jp"},
                            {"net", "whois.internic.net"},
                            {"org", "whois.internic.net"},
                            {"edu", "whois.internic.net"},
                            {"ac", "whois.nic.ac"},
                            {"al", "whois.ripe.net"},
                            {"am", "whois.amnic.net"},
                            {"am", "whois.amnic.net"},
                            {"as", "whois.nic.as"},
                            {"at", "whois.nic.at"},
                            {"au", "whois.aunic.net"},
                            {"az", "whois.ripe.net"},
                            {"ba", "whois.ripe.net"},
                            {"be", "whois.dns.be"},
                            {"bg", "whois.ripe.net"},
                            {"bm", "rwhois.ibl.bm:4321"},
                            {"br", "registro.fapesp.br"},
                            {"by", "whois.ripe.net"},
                            {"ca", "whois.cdnnet.ca"},
                            {"cc", "whois.nic.cc"},
                            {"ch", "whois.nic.ch"},
                            {"cl", "whois.nic.cl"},
                            {"cn", "whois.cnnic.cn"},
                            {"cx", "whois.nic.cx"},
                            {"cy", "whois.ripe.net"},
                            {"cz", "whois.ripe.net"},
                            {"de", "whois.ripe.net"},
                            {"dk", "whois.ripe.net"},
                            {"dz", "whois.ripe.net"},
                            {"ee", "whois.ripe.net"},
                            {"eg", "whois.ripe.net"},
                            {"es", "whois.ripe.net"},
                            {"fi", "whois.ripe.net"},
                            {"fo", "whois.ripe.net"},
                            {"fr", "whois.nic.fr"},
                            {"gov", "whois.nic.gov"},
                            {"gr", "whois.ripe.net"},
                            {"gs", "whois.adamsnames.tc"},
                            {"hk", "whois.hknic.net.hk"},
                            {"hm", "whois.nic.hm"},
                            {"hr", "whois.ripe.net"},
                            {"hu", "whois.ripe.net"},
                            {"ie", "whois.ucd.ie"},
                            {"il", "whois.ripe.net"},
                            {"in", "whois.ncst.ernet.in"},
                            {"int", "whois.isi.edu"},
                            {"is", "whois.isnet.is"},
                            {"it", "whois.nic.it"},
                            {"kr", "whois.krnic.net"},
                            {"kz", "whois.domain.kz"},
                            {"li", "whois.nic.li"},
                            {"lk", "whois.nic.lk"},
                            {"lt", "whois.ripe.net"},
                            {"lu", "whois.ripe.net"},
                            {"lv", "whois.ripe.net"},
                            {"ma", "whois.ripe.net"},
                            {"md", "whois.ripe.net"},
                            {"mil", "whois.nic.mil"},
                            {"mk", "whois.ripe.net"},
                            {"mm", "whois.nic.mm"},
                            {"ms", "whois.adamsnames.tc"},
                            {"mt", "whois.ripe.net"},
                            {"mx", "whois.nic.mx"},
                            {"my", "whois.mynic.net"},
                            {"nl", "whois.domain-registry.nl"},
                            {"no", "whois.norid.no"},
                            {"nu", "whois.nic.nu"},
                            {"pe", "whois.rcp.net.pe"},
                            {"pk", "whois.pknic.net.pk"},
                            {"pl", "whois.ripe.net"},
                            {"pt", "whois.dns.pt"},
                            {"ro", "whois.ripe.net"},
                            {"ru", "whois.ripn.net"},
                            {"se", "whois.nic-se.se"},
                            {"sg", "whois.nic.net.sg"},
                            {"sh", "whois.nic.sh"},
                            {"si", "whois.ripe.net"},
                            {"sk", "whois.ripe.net"},
                            {"sm", "whois.ripe.net"},
                            {"st", "whois.nic.st"},
                            {"su", "whois.ripe.net"},
                            {"tc", "whois.adamsnames.tc"},
                            {"tf", "whois.adamsnames.tc"},
                            {"th", "whois.thnic.net"},
                            {"tj", "whois.nic.tj"},
                            {"tm", "whois.nic.tm"},
                            {"tn", "whois.ripe.net"},
                            {"to", "whois.tonic.to"},
                            {"tr", "whois.metu.edu.tr"},
                            {"tw", "whois.twnic.net"},
                            {"ua", "whois.ripe.net"},
                            {"uk", "whois.nic.uk"},
                            {"us", "whois.isi.edu"},
                            {"va", "whois.ripe.net"},
                            {"vg", "whois.adamsnames.tc"},
                            {"yu", "whois.ripe.net"},
                            {"za", "whois.co.za"}
                        };
                    }
                }
            }
            return _Default;
        } }

        protected List<DomainToWhoisSvrMap> _List;

        public DomainToWhoisServerList()
        {
            this._List = new List<DomainToWhoisSvrMap>();
        }

        public void Add(string topDomain, string whoisServer)
        {
            _List.Add(new DomainToWhoisSvrMap { TopDomain = topDomain, WhoisServer = whoisServer });
        }

        public IEnumerator<DomainToWhoisSvrMap> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        public string FindWhoisServer(string domain)
        {
            var map = _List.FirstOrDefault(a =>
                Regex.IsMatch(domain, @"(^|\.)" + a.TopDomain.Replace(".", @"\.") + "$"));
            return map == null ? null : map.WhoisServer;
        }
    }
}
