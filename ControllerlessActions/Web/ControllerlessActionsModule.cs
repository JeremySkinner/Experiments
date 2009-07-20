using System;
using System.Web.Mvc;
using Autofac.Builder;
using CommonServiceLocator.AutofacAdapter;
using ControllerlessActions;
using Microsoft.Practices.ServiceLocation;
using Web.Controllers.Home;
using Web.Filters;

namespace Web {
	public class ControllerlessActionsModule : Module {
		INamingConventions	namingConventions = new DefaultNamingConventions();

		protected override void Load(ContainerBuilder builder) {
			builder.Register(c => new AutofacServiceLocator(c)).As<IServiceLocator>().ContainerScoped();
			builder.Register(namingConventions).ExternallyOwned();
			builder.Register(c => new DefaultActionMethodSelector()).As<IActionMethodSelector>();

			var actionTypes = new ControllerActionLocator(namingConventions)
				.FindActionsFromAssemblyContaining<Index>()
				.Where(x => x.Namespace.StartsWith("Web.Controllers"));

			foreach(var action in actionTypes) {
				builder.Register(action.Type).FactoryScoped().Named(action.Name);
			}

			ConfigureFilters(builder);
		}

		private void ConfigureFilters(ContainerBuilder builder) {
			var filters = new FilterCollection();
			builder.Register(filters).ExternallyOwned();
			builder.RegisterTypesAssignableTo<IActionFilter>().FactoryScoped();
			builder.RegisterTypesAssignableTo<IAuthorizationFilter>().FactoryScoped();
			

			filters.Apply<DecorateModelWithViewResult>().Always();
			filters.Apply<AddMessageToViewData>().When(action => action.ActionInstance is Controllers.Home.Index);
			//alternatively:
			//filters.Apply<AddMessageToViewData>().ForTypesAssignableTo<Controllers.Home.Index>();
		}
	}
}