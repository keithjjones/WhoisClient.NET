using System;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whois.NET;
using Whois.NET.Internal;

namespace WhoisClient_NET.Test
{
    [TestClass]
    public class IPAddressToWhoisServerListTest
    {
        [TestMethod]
        public void DefaultTest()
        {
            IPAddressToWhoisServerList.Default
                .Last()
                .WhoisServer.Is("whois.arin.net");
        }

        [TestMethod]
        public void FindWhoisServerTest()
        {
            IPAddressToWhoisServerList.Default
                .FindWhoisServer(IPAddress.Parse("125.0.0.1"))
                .Is("whois.apnic.net");
        }

        [TestMethod]
        public void FindWhoisServerTest_Hit_LastDefault()
        {
            IPAddressToWhoisServerList.Default
                .FindWhoisServer(IPAddress.Parse("127.0.0.1"))
                .Is("whois.arin.net");
        }
    }
}
