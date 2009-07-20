using System;
using System.Web.Mvc;
using ControllerlessActions;

namespace Web.Filters {
	public class DecorateModelWithViewResult : IActionFilter {
		public void OnActionExecuting(ActionExecutingContext filterContext) {
		}

		public void OnActionExecuted(ActionExecutedContext filterContext) {
			var result = filterContext.Result as ModelResult;
			if(result != null) {
				filterContext.Controller.ViewData.Model = result.Model;
				filterContext.Result = new ViewResult();
			}
		}
	}
}