using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using NetTools;

namespace Whois.NET
{
    [DataContract]
    public class WhoisResponse
    {
        /// <summary>
        /// FQDN of WHOIS servers which sent query. The last element is the WHOIS server name that latest queried.
        /// </summary>
        [DataMember]
        public string[] RespondedServers { get; protected set; }

        [DataMember]
        public string Raw { get; protected set; }

        [DataMember]
        public string OrganizationName { get; protected set; }

        [DataMember]
        public IPAddressRange AddressRange { get; protected set; }

        public WhoisResponse(string[] responsedServers, string rawWhoisResponse)
        {
            this.RespondedServers = responsedServers;
            this.Raw = rawWhoisResponse;

            // resolve Organization Name.
            var m1 = Regex.Match(this.Raw,
                @"(^f\.\W*\[組織名\]\W+(?<orgName>[^\r\n]+))|" +
                @"(^(OrgName|descr):\W+(?<orgName>[^\r\n]+))",
                RegexOptions.Multiline);
            if (m1.Success)
            {
                this.OrganizationName = m1.Groups["orgName"].Value;
            }

            // resolve Address Range.
            var m2 = Regex.Match(this.Raw, 
                @"(^a.\W*\[IPネットワークアドレス\]\W+(?<adr>[^\r\n]+))|"+
                @"(^(NetRange|CIDR|inetnum):\W+(?<adr>[^\r\n]+))",
                RegexOptions.Multiline);
            if (m2.Success)
            {
                this.AddressRange = new IPAddressRange(m2.Groups["adr"].Value);
            }
        }
    }
}
