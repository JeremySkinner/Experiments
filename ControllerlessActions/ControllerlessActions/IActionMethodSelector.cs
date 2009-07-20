using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace ControllerlessActions {
	public interface IActionMethodSelector {
		ActionDescriptor SelectMethod(RequestContext context, ActionDescriptor[] descriptors);
		ActionDescriptor[] GetMethods(ControllerlessDescriptor descriptor);
	}

	public class DefaultActionMethodSelector : IActionMethodSelector {
		public ActionDescriptor SelectMethod(RequestContext context, ActionDescriptor[] descriptors) {
			string methodName = FormatVerb(context.HttpContext.Request.HttpMethod) ?? "Execute";
			return descriptors.SingleOrDefault(x => x.ActionName == methodName) ??
			       descriptors.SingleOrDefault(x => x.ActionName == "Execute");
		}

		public ActionDescriptor[] GetMethods(ControllerlessDescriptor descriptor) {
			return descriptor.Type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod)
				.Select(x => new ControllerlessActionDescriptor(descriptor, x)).ToArray();
		}

		private string FormatVerb(string method) {
			if (string.IsNullOrEmpty(method) || method.Length < 2) return null;

			return method[0].ToString().ToUpperInvariant() + method.Substring(1).ToLowerInvariant();
		}
	}
}