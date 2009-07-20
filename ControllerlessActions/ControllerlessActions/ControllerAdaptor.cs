using System;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace ControllerlessActions {
	public class ControllerAdaptor : Controller, IControllerAdaptor {
		private readonly object action;
		private readonly IServiceLocator serviceLocator;

		public ControllerAdaptor(object action, IServiceLocator serviceLocator) {
			this.action = action;
			this.serviceLocator = serviceLocator;

			ActionInvoker = new ControllerlessActionInvoker(serviceLocator, action);
		}


		public object Action {
			get { return action; }
		}
	}

	public interface IControllerAdaptor : IController {
		object Action { get; }
	}
}