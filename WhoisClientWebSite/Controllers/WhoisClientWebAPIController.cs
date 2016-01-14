using System;
using System.Linq;
using System.Text;
using System.Web.Http;
using Whois.NET;

namespace WhoisClientWebSite.Controllers
{
    public class WhoisClientWebAPIController : ApiController
    {
        // GET /api/query/
        public WhoisResponse Query(string query, string server, int port = 43, string encoding = "ASCII")
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("required 'query' parameter.", "query");
            return WhoisClient.Query(query, server, port, Encoding.GetEncoding(encoding));
        }

        // GET /api/rawquery/
        public string RawQuery(string query, string server, int port = 43, string encoding = "ASCII")
        {
            if (string.IsNullOrWhiteSpace(query)) throw new ArgumentException("required 'query' parameter.", "query");
            if (string.IsNullOrWhiteSpace(server)) throw new ArgumentException("required 'server' parameter.", "server");
            return WhoisClient.RawQuery(query, server, port, Encoding.GetEncoding(encoding));
        }
    }
}
