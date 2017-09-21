using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YueDu.Controllers
{
	public class HeaderController : Controller
	{
		//
		// GET: /Layout/
		public PartialViewResult Index()
		{
			ViewData.Model = "这是布局的头部";
			return PartialView();
		}
	}
}