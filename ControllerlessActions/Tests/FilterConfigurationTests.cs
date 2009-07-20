using System;
using System.Web.Mvc;
using System.Web.Routing;
using ControllerlessActions;
using Microsoft.Practices.ServiceLocation;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests {
	[TestFixture]
	public class FilterConfigurationTests {
		private FilterCollection filters;
		private FakeServiceLocator locator;
		private RequestContext context;
		private TestAction action;
		private ControllerAdaptor adaptor;

		[SetUp]
		public void Setup() {
			filters = new FilterCollection();
			
			locator = new FakeServiceLocator();
			locator.Add(filters);
			locator.Add<IActionMethodSelector>(new DefaultActionMethodSelector());

			context = TestHelper.RequestContext();
			context.RouteData.Values.Add("action","test");
			
			action = new TestAction();
			adaptor = new ControllerAdaptor(action, locator) { TempDataProvider = new NullTempDataProvider() };
		}

		[Test]
		public void Apply_single_filter() {
            filters.Apply<TestActionFilter>().When(x => true);
            
			Execute();

			action.FilterInvoked.ShouldBeTrue();
		}

		[Test]
		public void Should_not_apply_filter() {
			filters.Apply<TestActionFilter>().When(x => false);
			Execute();

			action.FilterInvoked.ShouldBeFalse();
		}
		private void Execute() {
			((IController)adaptor).Execute(context);
		}

		private class TestAction {
			public List<object> InvokedFilters = new List<object>(); 

			public bool FilterInvoked = false;

			public ActionResult Execute() {
				return null;
			}
		}

        private class TestActionFilter : IActionFilter {
			public void OnActionExecuting(ActionExecutingContext filterContext) {
				((TestAction)((IControllerAdaptor)filterContext.Controller).Action).FilterInvoked = true;
				((TestAction)((IControllerAdaptor)filterContext.Controller).Action).InvokedFilters.Add(this);
			}

			public void OnActionExecuted(ActionExecutedContext filterContext) {
			}
        }

		private class TestActionFilter2 : IActionFilter  {
			public void OnActionExecuting(ActionExecutingContext filterContext) {
				((TestAction)((IControllerAdaptor)filterContext.Controller).Action).InvokedFilters.Add(this);
			}

			public void OnActionExecuted(ActionExecutedContext filterContext) {
			}
		}
	}
}