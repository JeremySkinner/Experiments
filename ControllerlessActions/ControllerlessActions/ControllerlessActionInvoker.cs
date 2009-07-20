using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace ControllerlessActions {
	public class ControllerlessActionInvoker : ControllerActionInvoker {
		private IServiceLocator locator;
		private readonly Object action;

		public ControllerlessActionInvoker(IServiceLocator serviceLocator, Object action) {
			this.locator = serviceLocator;
			this.action = action;
		}

		protected override ControllerDescriptor GetControllerDescriptor(ControllerContext controllerContext) {
            var actionType = action.GetType();
			var descriptor = new ControllerlessDescriptor(actionType, action, locator.GetInstance<IActionMethodSelector>());
			return descriptor;
		}

		protected override void InvokeActionResult(ControllerContext controllerContext, ActionResult actionResult) {
			FixupViewResult(actionResult as ViewResultBase, controllerContext);
			base.InvokeActionResult(controllerContext, actionResult);
		}

		private void FixupViewResult(ViewResultBase result, ControllerContext context) {
			if(result == null) return;
			result.ViewData = context.Controller.ViewData;
			result.TempData = context.Controller.TempData;
		}

		protected override ActionResult CreateActionResult(ControllerContext controllerContext, ActionDescriptor actionDescriptor, object actionReturnValue) {
			if(actionReturnValue != null && !(actionReturnValue is ActionResult)) {
				actionReturnValue = new ModelResult(actionReturnValue);
			}

			return base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
		}

		protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor) {
			var filters = locator.GetInstance<FilterCollection>();
			var query = from f in filters
						where f.AppliesTo((ControllerlessDescriptor)actionDescriptor.ControllerDescriptor)
						select locator.GetInstance(f.FilterType);

			var matchingFilters = query.ToList();

			var filterInfo = new FilterInfo();
			MergeFilters(matchingFilters, filterInfo.ActionFilters);
			MergeFilters(matchingFilters, filterInfo.AuthorizationFilters);
			MergeFilters(matchingFilters, filterInfo.ExceptionFilters);
			MergeFilters(matchingFilters, filterInfo.ResultFilters);
			return filterInfo;
		}

		private void MergeFilters<TFilter>(IEnumerable<object> source, IList<TFilter> destination) where TFilter : class {
			foreach (var filter in source) {
				var castFilter = filter as TFilter;
				if (castFilter != null) {
					destination.Add(castFilter);
				}
			}
		}
	}

	public class ModelResult : ContentResult {
		public object Model { get; private set; }

		public ModelResult(object model) {
			Model=model;
		}

		public override void ExecuteResult(ControllerContext context) {
			Content = Convert.ToString(Model, CultureInfo.CurrentCulture);
			base.ExecuteResult(context);
		}
	}
}