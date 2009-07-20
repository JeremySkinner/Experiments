using System;
using System.Web.Mvc;
using ControllerlessActions;

namespace Web.Controllers.Home {
	public class About {
		public ActionResult Execute() {
			return new ViewResult();
		}
	}
}