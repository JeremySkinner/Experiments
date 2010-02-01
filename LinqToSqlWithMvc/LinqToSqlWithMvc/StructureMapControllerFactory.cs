namespace LinqToSqlWithMvc
{
	using System;
	using System.Web.Mvc;
	using System.Web.Routing;
	using StructureMap;

	public class StructureMapControllerFactory : DefaultControllerFactory {
		protected override IController GetControllerInstance(Type controllerType) {
			return (IController)ObjectFactory.GetInstance(controllerType);
		}
	}
}