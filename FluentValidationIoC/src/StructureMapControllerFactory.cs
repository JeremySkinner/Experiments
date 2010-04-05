namespace FluentValidationIoC {
	using System.Web;
	using System.Web.Mvc;
	using StructureMap;

	public class StructureMapControllerFactory : DefaultControllerFactory {
		protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType) {
			if(controllerType == null) {
				throw new HttpException(404, "Controller not found");
			}

			var controller = ObjectFactory.TryGetInstance(controllerType) as IController;

			if(controller == null) {
				throw new HttpException(404, "Controller not found");
			}

			return controller;
		}
	}
}