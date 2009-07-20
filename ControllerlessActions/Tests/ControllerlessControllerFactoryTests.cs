using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ControllerlessActions;
using Microsoft.Practices.ServiceLocation;
using Moq;
using NUnit.Framework;

namespace Tests {
	[TestFixture]
	public class ControllerlessControllerFactoryTests {
		private ControllerlessControllerFactory factory;
		private RequestContext context;

		[SetUp]
		public void Setup() {
			var locator = new Mock<IServiceLocator>();
			locator.Setup(x => x.GetInstance(typeof(object), "home.test.action")).Returns(new TestAction());
			locator.Setup(x => x.GetInstance<INamingConventions>()).Returns(new DefaultNamingConventions());

			factory = new ControllerlessControllerFactory(()=>locator.Object);
			context = TestHelper.RequestContext();
		}

		[Test]
		public void Instantiates_correct_action() {
			context.RouteData.Values.Add("action", "test");

			var action = factory.CreateController(context, "home");

			action.ShouldBe<ControllerAdaptor>().Action.ShouldBe<TestAction>();
		}

		[Test]
		public void Throws_404_when_Action_not_found() {
			context.RouteData.Values.Add("action", "foo");
			typeof(HttpException).ShouldBeThrownBy(() => factory.CreateController(context, "home"));
		}

		private class TestAction {
			public ActionResult Execute() {
				return new EmptyResult();
			}
		}
	}
}