using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FluentValidation;
using FVInputBuilders.Models;
using InputBuilder;

namespace FVInputBuilders
{
	public class MvcApplication : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new {controller = "Home", action = "Index", id = ""} // Parameter defaults
				);
		}

		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);
			InputBuilder.InputBuilder.BootStrap();
			SetupFluentValidationConventions();
		}

		private void SetupFluentValidationConventions() {
			var conventions = new FluentValidationConventions(new SampleValidatorFactory());

			ModelPropertyFactory.ExampleForPropertyConvention = conventions.ExampleConvention;
			ModelPropertyFactory.LabelForPropertyConvention = conventions.LabelConvention;
			ModelPropertyFactory.PartialNameConvention = conventions.PartialNameConvention; 
			ModelPropertyFactory.PropertyIsRequiredConvention = conventions.RequiredConvention; 
		}

	}
}