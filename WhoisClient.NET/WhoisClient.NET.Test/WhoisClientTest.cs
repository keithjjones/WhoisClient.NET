using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Whois.NET;

namespace WhoisClient_NET.Test
{
    [TestClass]
    public class WhoisClientTest
    {
        [TestMethod]
        public void RipeReferralTest()
        {
            WhoisClient
                .Query("31.116.94.96")
                .OrganizationName
                .Is("EE Limited");
        }

        [TestMethod]
        public void WhoisClientIPTest()
        {
            WhoisResponse response = WhoisClient.Query("4.4.4.4");
            Assert.AreEqual("Level 3 Communications, Inc. LVLT-STATIC-4-4-16 (NET-4-4-0-0-1)", response.OrganizationName);
            Assert.AreEqual("4.4.0.0-4.4.255.255", response.AddressRange.ToString());
        }

    }
}
