using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Builder;
using Autofac.Integration.Web;
using ControllerlessActions;
using Microsoft.Practices.ServiceLocation;

namespace Web {
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication, IContainerProviderAccessor {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default",                                              // Route name
				"{controller}/{action}/{id}",                           // URL with parameters
				new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
			);

		}

		protected void Application_Start() {
			RegisterRoutes(RouteTable.Routes);
			InitialiseContainer();

			ControllerBuilder.Current.SetControllerFactory(new ControllerlessControllerFactory(() => provider.RequestContainer.Resolve<IServiceLocator>()));

			ConfigureActionFilters();
		}

		private void ConfigureActionFilters() {
		}

		private void InitialiseContainer() {
			var builder = new ContainerBuilder();
			builder.RegisterModule(new ControllerlessActionsModule());
			provider = new ContainerProvider(builder.Build());

			ServiceLocator.SetLocatorProvider(() => provider.RequestContainer.Resolve<IServiceLocator>());
        }

		private static IContainerProvider provider;

		public IContainerProvider ContainerProvider {
			get { return provider; }
		}
	}
}