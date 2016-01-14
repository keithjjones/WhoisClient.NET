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
                .Is("T-Mobile (UK) Limited");
        }
    }
}
