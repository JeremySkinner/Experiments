using System;
using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;

namespace Tests {
	public static class TestHelper {
		public static RequestContext RequestContext() {
			var context = new RequestContext(HttpContext(), new RouteData());
			return context;
		}

		public static HttpContextBase HttpContext() {
			var httpContext = new Mock<HttpContextBase>();
			var request = new Mock<HttpRequestBase>();
			var response = new Mock<HttpResponseBase>();

			httpContext.Setup(x => x.Request).Returns(request.Object);
			httpContext.Setup(x => x.Response).Returns(response.Object);

			return httpContext.Object;
		}

		public static void ShouldBeTrue(this bool actual) {
			Assert.IsTrue(actual);
		}

		public static void ShouldBeFalse(this bool actual) {
			Assert.IsFalse(actual);
		}

		public static T ShouldBe<T>(this object actual) {
			Assert.IsInstanceOf<T>(actual);
			return (T)actual;
		}

		public static Exception ShouldBeThrownBy(this Type type, TestDelegate action) {
			return Assert.Throws(type, action);
		}

		public static void ShouldEqual(this object actual, object expected) {
			Assert.AreEqual(expected, actual);
		}

		public static void ShouldBeTheSameAs(this object actual, object expected) {
			Assert.AreSame(expected, actual);
		}

		public static void ShouldNotBeNull(this object actual) {
			Assert.IsNotNull(actual);
		}
	}
}