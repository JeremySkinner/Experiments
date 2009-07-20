using System;
using System.Linq;
using System.Web.Mvc;
using ControllerlessActions;
using NUnit.Framework;
using Tests.Controllers.Home;

namespace Tests {
	[TestFixture]
	public class ControllerActionLocatorTests {
		private ControllerActionLocator locator;

		[SetUp]
		public void Setup() {
			locator = new ControllerActionLocator(new DefaultNamingConventions());
		}

		[Test]
		public void Finds_actions_in_assembly() {
			var actions = locator.FindActionsFrom(new[] { typeof(Index), typeof(About) }).Where(x => x.Namespace == "Tests.Controllers.Home").ToArray();
			actions.Length.ShouldEqual(2);
			actions[0].Type.ShouldEqual(typeof(Index));
			actions[0].Name.ShouldEqual("home.index.action");
			actions[1].Type.ShouldEqual(typeof(About));
			actions[1].Name.ShouldEqual("home.about.action");
		}
        
		[Test]
		public void Builds_key_from_type() {
			string expected = "home.index.action";
			string actual = locator.NamingConventions.BuildKeyFromType(typeof(Index));
			actual.ShouldEqual(expected);
		}
	}
}

namespace Tests.Controllers.Home {
	public class Index  {
		public  ActionResult Execute() {
			return null;
		}
	}

	public class About  {
		public ActionResult Execute() {
			return null;
		}
	}
}