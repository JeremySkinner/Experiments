using System;
using System.Web.Mvc;
using ControllerlessActions;
using Web.Models;

namespace Web.Controllers.Home {
	public class Index {

		public object Get(int? id) {
			return new HomeViewModel { Id = id };
		}

		public object Post(string name) {
			return new ContentResult { Content = "Hello there, " + name };
		}
	}
}