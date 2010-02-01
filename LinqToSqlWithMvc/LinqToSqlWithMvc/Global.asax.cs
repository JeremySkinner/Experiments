namespace LinqToSqlWithMvc
{
	using System;
	using System.Web;
	using System.Web.Mvc;
	using System.Web.Routing;
	using LinqToSqlWithMvc.Models;
	using StructureMap;
	using StructureMap.Configuration.DSL;

	public class MvcApplication : HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//This must be before calls to MapRoute
			routes.Add(new RoutePreParser());

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new {controller = "Home", action = "Index", id = ""} // Parameter defaults
				);
		}

		protected void Application_Start()
		{
			RegisterRoutes(RouteTable.Routes);

			ObjectFactory.Configure(cfg => cfg.AddRegistry(new MyRegistry()));

			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

			//This is very bad - do not do this in production!
			AppDomain.CurrentDomain.SetData("SQLServerCompactEditionUnderWebHosting", true);
		}
	}

	public class MyRegistry : Registry
	{
		public MyRegistry()
		{
			For<BlogDataContext>()
				.HttpContextScoped()
				.Use(c =>
				     	{
							string pathToDatabase = HttpContext.Current.Server.MapPath("~/Blog.sdf");
							return new BlogDataContext(pathToDatabase);
				     	});

			Scan(scanner => scanner.AddAllTypesOf<Controller>());
		}
	}
}