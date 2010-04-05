namespace FluentValidationIoC {
	using System.Web.Mvc;
	using FluentValidation;
	using FluentValidationIoC.Controllers;
	using FluentValidationIoC.Models;
	using StructureMap.Configuration.DSL;

	public class MyRegistry : Registry {
		public MyRegistry() {
			For<PersonRepository>().Use<PersonRepository>();
			For<HomeController>().Use<HomeController>();
			
			For<IValidator<Person>>()
				.Singleton()
				.Use<PersonValidator>();
		}
	}
}