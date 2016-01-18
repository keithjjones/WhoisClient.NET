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

        public WhoisResponse()
        {

        }

        public WhoisResponse(string[] responsedServers, string rawWhoisResponse)
        {
            this.RespondedServers = responsedServers;
            this.Raw = rawWhoisResponse;

            // resolve Organization Name.
            var m1 = Regex.Match(this.Raw,
                @"(^f\.\W*\[組織名\]\W+(?<orgName>[^\r\n]+))|" +
                @"(^\s*(OrgName|descr|Registrant Organization|owner):\W+(?<orgName>[^\r\n]+))",
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
                this.AddressRange = IPAddressRange.Parse(m2.Groups["adr"].Value);
            }

            // resolve ARIN Address Range.
            if (responsedServers != null && responsedServers.Last() == "whois.arin.net")
            {
                var m3 = Regex.Matches(this.Raw,
                    @"(?<org>^.*) (?<adr>\d+\.\d+\.\d+\.\d+ - \d+\.\d+\.\d+\.\d+)",
                    RegexOptions.Multiline);
                if (m3.Count > 0)
                {
                    var mymatch = m3[m3.Count - 1];
                    // Test to see if the information was already picked up from above
                    if (mymatch.Groups["org"].Value.Trim() != "NetRange:" &&
                        mymatch.Groups["org"].Value.Trim() != "CIDR:" &&
                        mymatch.Groups["org"].Value.Trim() != "inetnum:")
                    {
                        this.AddressRange = IPAddressRange.Parse(mymatch.Groups["adr"].Value);
                        this.OrganizationName = mymatch.Groups["org"].Value.Trim();
                    }
                }
            }

        }
    }
}
