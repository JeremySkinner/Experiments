using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FluentValidationIoC.Controllers {
	using FluentValidationIoC.Models;

	[HandleError]
	public class HomeController : Controller {

		PersonRepository repositoy;

		public HomeController(PersonRepository repositoy) {
			this.repositoy = repositoy;
		}

		public ActionResult Index() {
			return View(repositoy.FindAll());
		}

		public ActionResult Edit(int id) {
			var person = repositoy.FindById(id);
			return View(person);
		}

		[HttpPost]
		public ActionResult Edit(int id, FormCollection form) {
			var person = repositoy.FindById(id);

			TryUpdateModel(person, form);

			if(ModelState.IsValid) {
				return View("Success");	
			}

			return View(person);
		}

		public ActionResult About() {
			return View();
		}
	}
}
