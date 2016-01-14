using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whois.NET;
using Whois.NET.Internal;

namespace WhoisClient_NET.Test
{
    [TestClass]
    public class DomainToWhoisServerListTest
    {
        [TestMethod]
        public void FindTest_TopDomainOnly()
        {
            DomainToWhoisServerList.Default.FindWhoisServer("jp").Is("whois.jp");
        }

        [TestMethod]
        public void FindTest_FQDN()
        {
            DomainToWhoisServerList.Default.FindWhoisServer("www.microsoft.co.jp").Is("whois.jp");
        }

        [TestMethod]
        public void FindTest_SubDomainOnly()
        {
            DomainToWhoisServerList.Default.FindWhoisServer("uk.com").Is("whois.uk.com");
        }

        [TestMethod]
        public void FindTest_SubDomainFQDN()
        {
            DomainToWhoisServerList.Default.FindWhoisServer("whois.eu.org").Is("whois.eu.org");
        }

        [TestMethod]
        public void FindTest_NotFound()
        {
            DomainToWhoisServerList.Default.FindWhoisServer("whois.xxx").IsNull();
        }
    }
}
