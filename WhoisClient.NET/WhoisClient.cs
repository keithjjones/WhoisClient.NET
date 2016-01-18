using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NetTools;

namespace Whois.NET
{
    public class WhoisClient
    {
        /// <summary>
        /// Send WHOIS query to WHOIS server, requery to referral servers recursive, and return the response from WHOIS server.
        /// </summary>
        /// <param name="query">domain name (ex."nic.ad.jp")or IP address (ex."192.41.192.40") to be queried.</param>
        /// <param name="server">FQDN of whois server (ex."whois.arin.net"). This parameter is optional (default value is null) to determine server automatically.</param>
        /// <param name="port">TCP port number to connect whois server. This parameter is optional, and default value is 43.</param>
        /// <param name="encoding">Encoding method to decode the result of query. This parameter is optional (default value is null) to using ASCII encoding.</param>
        /// <returns>The strong typed result of query which responded from WHOIS server.</returns>
        public static WhoisResponse Query(string query, string server = null, int port = 43, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.ASCII;

            if (string.IsNullOrEmpty(server))
            {
                var ipAddress = default(IPAddress);
                IPAddress.TryParse(query, out ipAddress);
                server = "whois.iana.org";
            }

            return QueryRecursive(query, new List<string> { server }, port, encoding);
        }

        private static WhoisResponse QueryRecursive(string query, List<string> servers, int port, Encoding encoding)
        {
            var server = servers.Last();

            string rawResponse = "";

            // Remove the "domain" command from other servers
            if (server == "whois.internic.net" || server == "whois.verisign-grs.com")
            {
                rawResponse = RawQuery("domain " + query, server, port, encoding);
            }
            else
            {
                rawResponse = RawQuery(query, server, port, encoding);
            }

            // "ReferralServer: whois://whois.apnic.net"
            // "remarks:        at whois.nic.ad.jp. To obtain an English output"
            var m2 = Regex.Match(rawResponse,
                @"(^ReferralServer:\W+whois://(?<refsvr>[^:\r\n]+)(:(?<port>\d+))?)|" +
                @"(^\s*Whois Server:\s*(?<refsvr>[^:\r\n]+)(:(?<port>\d+))?)|" +
                @"(^\s*refer:\s*(?<refsvr>[^:\r\n]+)(:(?<port>\d+))?)|" +
                @"(^remarks:\W+.*(?<refsvr>whois\.[0-9a-z\-\.]+\.[a-z]{2})(:(?<port>\d+))?)",
                RegexOptions.Multiline);
            if (m2.Success)
            {
                servers.Add(m2.Groups["refsvr"].Value);
                if (m2.Groups["port"].Success) port = int.Parse(m2.Groups["port"].Value);
                return QueryRecursive(query, servers, port, encoding);
            }
            else
                return new WhoisResponse(servers.ToArray(), rawResponse);
        }

        /// <summary>
        /// Send simple WHOIS query to WHOIS server, and return the response from WHOIS server.
        /// (No requery to referral servers, and No parse the result of query.)
        /// </summary>
        /// <param name="query">domain name (ex."nic.ad.jp")or IP address (ex."192.41.192.40") to be queried.</param>
        /// <param name="server">FQDN of whois server (ex."whois.arin.net").</param>
        /// <param name="port">TCP port number to connect whois server. This parameter is optional, and default value is 43.</param>
        /// <param name="encoding">Encoding method to decode the result of query. This parameter is optional (default value is null) to using ASCII encoding.</param>
        /// <returns>The raw data decoded by encoding parameter from WHOIS server responded.</returns>
        public static string RawQuery(string query, string server, int port = 43, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.ASCII;
            var tcpClient = new TcpClient(server, port);
            try
            {
                using (var s = tcpClient.GetStream())
                {
                    var queryBytes = Encoding.ASCII.GetBytes(query + "\r\n");
                    s.Write(queryBytes, 0, queryBytes.Length);

                    const int buffSize = 8192;
                    var readBuff = new byte[buffSize];
                    var res = new StringBuilder();
                    var cbRead = default(int);
                    do
                    {
                        cbRead = s.Read(readBuff, 0, readBuff.Length);
                        res.Append(encoding.GetString(readBuff, 0, cbRead));
                    } while (cbRead > 0);

                    return res.ToString();
                }
            }
            finally
            {
                tcpClient.Close();
            }
        }
    }
}
