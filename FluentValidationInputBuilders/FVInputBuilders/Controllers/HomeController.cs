using System.Web.Mvc;
using FVInputBuilders.Models;

namespace FVInputBuilders.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View(new SampleModel());
		}

		public ActionResult Save(SampleModel model)
		{
			return View("Index", model);
		}
	}
}