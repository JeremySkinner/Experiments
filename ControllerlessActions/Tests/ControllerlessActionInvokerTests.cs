using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using ControllerlessActions;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;

namespace Tests {
	[TestFixture]
	public class ControllerlessActionInvokerTests {
		private TestAction action;
		private RequestContext context;
		private ControllerAdaptor adaptor;
		private FakeServiceLocator locator;

		[SetUp]
		public void Setup() {
			action = new TestAction();
			context = TestHelper.RequestContext();
			locator = new FakeServiceLocator();
			locator.Add<IActionMethodSelector>(new DefaultActionMethodSelector());

			adaptor = new ControllerAdaptor(action, locator) { TempDataProvider = new NullTempDataProvider()};
		}

		[Test]
		public void Invokes_action() {
			context.RouteData.Values.Add("controller", "Index");
			context.RouteData.Values.Add("action", "Test");

			Execute();

			action.WasExecuted.ShouldBeTrue();
		}

		[Test]
		public void Fills_properties() {
			context.RouteData.Values.Add("controller", "Index");
			context.RouteData.Values.Add("action", "Test");
			context.RouteData.Values.Add("id", "1");

			Execute();

			action.Id.ShouldEqual(1);
		}

		[Test]
		public void Fixes_viewresult() {
			context.RouteData.Values.Add("controller", "Index");
			context.RouteData.Values.Add("action", "Test");

			Execute();

			action.ResultReturned.ViewData.ShouldBeTheSameAs(adaptor.ViewData);
			action.ResultReturned.TempData.ShouldBeTheSameAs(adaptor.TempData);
		}

		[Test]
		public void When_action_returns_object_should_be_decorated_with_modelresult() {
			context.RouteData.Values.Add("controller", "Index");
			context.RouteData.Values.Add("action", "Test");

			var filter = new FilterInterceptsModel();
			var filters = new FilterCollection();
			filters.Apply<FilterInterceptsModel>().Always();
			locator.Add(filter);
			locator.Add(filters);
            
			adaptor = new ControllerAdaptor(new TestActionReturnsModel(), locator) { TempDataProvider = new NullTempDataProvider() };

			Execute();

			filter.Model.ShouldNotBeNull();
		}

		private void Execute() {
			((IController)adaptor).Execute(context);
		}

		private class TestAction {
			public bool WasExecuted = false;
			public ViewResult ResultReturned;

			public ActionResult Execute(int? id) {
				WasExecuted = true;
				ResultReturned = new NullViewResult();
				Id = id;
				return ResultReturned;
			}

			public int? Id { get; set; }
		}

		private class FilterInterceptsModel : IActionFilter {

			public SomeModel Model;

			public void OnActionExecuting(ActionExecutingContext filterContext) {
			}

			public void OnActionExecuted(ActionExecutedContext filterContext) {
				Model = (SomeModel) ((ModelResult)filterContext.Result).Model;
			}
		}

		private class TestActionReturnsModel {
			public object Execute() {
				return new SomeModel();
			}
		}

		private class SomeModel {
			
		}
	}

	public class NullViewResult : ViewResult {
		public override void ExecuteResult(ControllerContext context) {
		}
	}

	public class NullTempDataProvider : ITempDataProvider {
		public IDictionary<string, object> LoadTempData(ControllerContext controllerContext) {
			return new Dictionary<string, object>();
		}

		public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values) {
		}
	}
}