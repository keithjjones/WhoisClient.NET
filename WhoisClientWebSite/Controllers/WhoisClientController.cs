using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Whois.NET;
using WhoisClientWebSite.Models;

namespace WhoisClientWebSite.Controllers
{
    public class WhoisClientController : Controller
    {
        public ActionResult Index(WhoisQueryViewModel model)
        {
            // Select default encoding if not specified, and persist last selected encoding.
            if (model.Encoding == null) 
            {
                var lastSelectedEncoding = (Request.Cookies["last-selected-encoding"] ?? new HttpCookie("", null)).Value;
                var userlang = Request.UserLanguages.FirstOrDefault() ?? "";
                model.Encoding = lastSelectedEncoding ?? (userlang.StartsWith("ja") ? "iso-2022-jp" : "us-ascii");
            }
            Response.Cookies.Set(new HttpCookie("last-selected-encoding", model.Encoding) { Expires = DateTime.Now.AddYears(100) });

            if (Request["Query"] == null)
            {
                ModelState.Clear();
                return View(model);
            }

            if (ModelState.IsValid == false) return View(model);

            model.Response = WhoisClient.Query(
                model.Query,
                model.Server,
                encoding: Encoding.GetEncoding(model.Encoding));
            
            return View(model);
        }

        public ActionResult ResponseOfQuery(WhoisQueryViewModel model)
        {
            var res = WhoisClient.Query(
                model.Query,
                model.Server,
                encoding: Encoding.GetEncoding(model.Encoding));
            return PartialView("_ResponseOfQuery", res);
        }
    }
}
