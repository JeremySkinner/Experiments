namespace LinqToSqlWithMvc
{
	using System.Linq;
	using System.Web;
	using System.Web.Routing;

	public class RoutePreParser : RouteBase {
		public override RouteData GetRouteData(HttpContextBase httpContext) {
			return null;
		}

		public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
			var query = from pair in values
						where pair.Value != null
						let routable = pair.Value as IUrlRoutable
						where routable != null
						select new { pair.Key, Value = routable.GetRouteParameter() };

			foreach (var pair in query.ToList()) {
				values[pair.Key] = pair.Value;
			}

			return null;
		}
	}
}