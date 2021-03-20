using MSSQL_Programmability.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SQLProgrammability.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
		[HttpPost]
		public ActionResult Index(FunAndSP fun_and_sp)
		{
			ViewBag.Title = "Home Page";

			ViewBag.Name = "";
			if (!string.IsNullOrWhiteSpace(fun_and_sp.Name))
			{
				ViewBag.Name = fun_and_sp.Name;
			}
			ViewBag.EXECUTE = "";
			if (!string.IsNullOrWhiteSpace(fun_and_sp.Execute))
			{
				ViewBag.EXECUTE = fun_and_sp.Execute;
			}
			ViewBag.content = "";
			if (!string.IsNullOrWhiteSpace(fun_and_sp.Content))
			{
				ViewBag.content = fun_and_sp.Content;
			}
			ViewBag.remark = "";
			if (!string.IsNullOrWhiteSpace(fun_and_sp.Remark))
			{
				ViewBag.remark = fun_and_sp.Remark;
			}
			ViewBag.tags = "";
			if (!string.IsNullOrWhiteSpace(fun_and_sp.Tags))
			{
				ViewBag.tags = fun_and_sp.Tags;
			}

			ViewBag.AJAXparams = getAJAXparams(fun_and_sp);

			return View();
		}

        public ActionResult TableInfo()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [HttpPost]
        public ActionResult TableInfo(FunAndSP fun_and_sp)
        {
            ViewBag.Title = "Home Page";

            ViewBag.Name = "";
            if (!string.IsNullOrWhiteSpace(fun_and_sp.Name))
            {
                ViewBag.Name = fun_and_sp.Name;
            }
            ViewBag.EXECUTE = "";
            if (!string.IsNullOrWhiteSpace(fun_and_sp.Execute))
            {
                ViewBag.EXECUTE = fun_and_sp.Execute;
            }
            ViewBag.content = "";
            if (!string.IsNullOrWhiteSpace(fun_and_sp.Content))
            {
                ViewBag.content = fun_and_sp.Content;
            }
            ViewBag.remark = "";
            if (!string.IsNullOrWhiteSpace(fun_and_sp.Remark))
            {
                ViewBag.remark = fun_and_sp.Remark;
            }
            ViewBag.tags = "";
            if (!string.IsNullOrWhiteSpace(fun_and_sp.Tags))
            {
                ViewBag.tags = fun_and_sp.Tags;
            }

            ViewBag.AJAXparams = getAJAXparams(fun_and_sp);

            return View();
        }

        private string getAJAXparams(FunAndSP fun_and_sp)
		{
			List<string> aJAX_Params = new List<string>();

			if (!string.IsNullOrWhiteSpace(fun_and_sp.Name))
			{
				aJAX_Params.Add("Name=" + fun_and_sp.Name);
			}
			if (!string.IsNullOrWhiteSpace(fun_and_sp.Execute))
			{
				aJAX_Params.Add("Execute=" + fun_and_sp.Execute);
			}
			if (!string.IsNullOrWhiteSpace(fun_and_sp.Content))
			{
				aJAX_Params.Add("Content=" + fun_and_sp.Content);
			}
			if (!string.IsNullOrWhiteSpace(fun_and_sp.Remark))
			{
				aJAX_Params.Add("Remark=" + fun_and_sp.Remark);
			}
			if (!string.IsNullOrWhiteSpace(fun_and_sp.Tags))
			{
				aJAX_Params.Add("Tags=" + fun_and_sp.Tags);
			}

			if (aJAX_Params.Count > 0)
			{
				return "/search?" + string.Join("&", aJAX_Params);
			}
			else
			{
				return "";
			}
		}
	}
}
