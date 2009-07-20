using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ControllerlessActions;
using Moq;
using NUnit.Framework;

namespace Tests {
	[TestFixture]
	public class DefaultActionMethodSelectorTester {
		private DefaultActionMethodSelector selector;
		private RequestContext context;
		private TestActionDescriptor[] descriptors;

		[SetUp]
		public void Setup() {
			selector = new DefaultActionMethodSelector();
			context = TestHelper.RequestContext();
			((IMocked<HttpRequestBase>)context.HttpContext.Request).Mock.Setup(x => x.HttpMethod).Returns("GET");
			descriptors = new[] { new TestActionDescriptor(typeof(ActionWithPostAndExecute).GetMethod("Post")), new TestActionDescriptor(typeof(ActionWithPostAndExecute).GetMethod("Execute")) };
		}

		[Test]
		public void Selects_execute() {
			var method = selector.SelectMethod(context,descriptors);
			method.ShouldEqual(descriptors[1]);
		}

		[Test]
		public void Selects_verb() {
			((IMocked<HttpRequestBase>)context.HttpContext.Request).Mock.Setup(x => x.HttpMethod).Returns("POST");
			var method = selector.SelectMethod(context, descriptors);
			method.ShouldEqual(descriptors[0]);
		}

		private class ActionWithPostAndExecute {
			public object Post() { return null; }
			public object Execute() { return null; }
		}

		private class TestActionDescriptor : ActionDescriptor {
			private readonly MethodInfo method;


			public TestActionDescriptor(MethodInfo method) {
				this.method = method;
			}

			public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters) {
				return null;
			}

			public override ParameterDescriptor[] GetParameters() {
				return new ParameterDescriptor[0];
			}

			public override string ActionName {
				get { return method.Name; }
			}

			public override ControllerDescriptor ControllerDescriptor {
				get { return null; }
			}
		}

	}
}