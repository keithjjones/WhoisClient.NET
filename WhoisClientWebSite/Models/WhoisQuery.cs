using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Whois.NET;

namespace WhoisClientWebSite.Models
{
    public class WhoisQueryViewModel
    {
        [Required]
        public string Query { get; set; }

        public string Server { get; set; }

        public string Encoding { get; set; }

        public WhoisResponse Response { get; set; }

        public WhoisQueryViewModel()
        {
        }
    }
}