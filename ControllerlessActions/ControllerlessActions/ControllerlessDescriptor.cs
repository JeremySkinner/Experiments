using System;
using System.Web.Mvc;

namespace ControllerlessActions {
	public class ControllerlessDescriptor : ControllerDescriptor {
		private Type actionType;
		private readonly object action;
		private readonly IActionMethodSelector methodSelector;
		private ActionDescriptor[] methodDescriptors;

		public ControllerlessDescriptor(Type actionType, Object action, IActionMethodSelector methodSelector) {
			this.actionType = actionType;
			this.action = action;
			this.methodSelector = methodSelector;

			methodDescriptors = methodSelector.GetMethods(this);
		}

		public override ActionDescriptor FindAction(ControllerContext controllerContext, string actionName) {
			return methodSelector.SelectMethod(controllerContext.RequestContext, methodDescriptors);
		}

		public override ActionDescriptor[] GetCanonicalActions() {
			return methodDescriptors;
		}

		public override Type ControllerType {
			get { return actionType; }
		}

		public Type Type {
			get { return actionType; }
		}

		public object ActionInstance {
			get { return action; }
		}
	}
}