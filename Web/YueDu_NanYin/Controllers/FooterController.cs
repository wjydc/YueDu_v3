using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YueDu.Controllers
{
	public class FooterController : Controller
	{
		//
		// GET: /Footer/
		public PartialViewResult Index()
		{
			ViewData.Model = "这是布局的底部";
			return PartialView();
		}
	}
}