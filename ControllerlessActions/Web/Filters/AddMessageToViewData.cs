using System;
using System.Web.Mvc;

namespace Web.Filters {
	public class AddMessageToViewData : IActionFilter {
		public void OnActionExecuting(ActionExecutingContext filterContext) {
			filterContext.Controller.ViewData["Message"] = "Hello world";
		}

		public void OnActionExecuted(ActionExecutedContext filterContext) {
		}
	}
}