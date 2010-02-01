namespace LinqToSqlWithMvc
{
	using System;
	using System.Data.Linq;
	using System.Web.Mvc;
	using LinqToSqlWithMvc.Models;
	using StructureMap;

	public class EagerlyLoadAttribute : FilterAttribute, IAuthorizationFilter {
		private Type[] types;

		public EagerlyLoadAttribute(params Type[] types) {
			this.types = types;
		}

		public void OnAuthorization(AuthorizationContext filterContext) {
			var loadOptions = new DataLoadOptions();
			var context = ObjectFactory.GetInstance<BlogDataContext>();

			foreach (var type in types) {
				if (!typeof(IEagerLoadingSpecification).IsAssignableFrom(type)) {
					throw new InvalidOperationException(string.Format("Type {0} does not implement IEagerLoadingSpecification", type));
				}

				var spec = (IEagerLoadingSpecification)Activator.CreateInstance(type);
				spec.Build(loadOptions);
			}

			context.LoadOptions = loadOptions;
		}
	}
}