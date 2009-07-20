using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;

namespace ControllerlessActions {
	public class ControllerlessControllerFactory : IControllerFactory {
		private Func<IServiceLocator> serviceLocator;
		
        public ControllerlessControllerFactory(Func<IServiceLocator> serviceLocator) {
			this.serviceLocator = serviceLocator;
		}

		private INamingConventions NamingConventions {
			get { return serviceLocator().GetInstance<INamingConventions>(); }
		}

		public IController CreateController(RequestContext requestContext, string controllerName) {
			string actionName = requestContext.RouteData.GetRequiredString("action");
			string key = NamingConventions.BuildKeyFromControllerAndAction(controllerName, actionName);

			object actionInstance;

			try {
				actionInstance =  serviceLocator().GetInstance(typeof(object), key);
			}
			catch (Exception ex) {
				throw new HttpException(404, "Controller not found", ex);
			}

			if (actionInstance == null) {
				throw new HttpException(404, "Controller not found");
			}

			return new ControllerAdaptor(actionInstance, serviceLocator());
		}


		public void ReleaseController(IController controller) {
		}


		public static string NameFromType(Type actionType) {
			return null;
		}
	}
}