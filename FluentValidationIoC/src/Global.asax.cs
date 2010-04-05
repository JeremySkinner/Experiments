using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FluentValidationIoC {
	using FluentValidation.Mvc;
	using StructureMap;

	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);


			//Configure structuremap
			ObjectFactory.Configure(cfg => cfg.AddRegistry(new MyRegistry()));
			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

			//Configure FV to use StructureMap
			var factory = new StructureMapValidatorFactory();

			//Tell MVC to use FV for validation
			ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(factory));        
			DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
		}
	}
}