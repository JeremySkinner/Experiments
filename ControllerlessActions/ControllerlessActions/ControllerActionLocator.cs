using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ControllerlessActions {

	public interface IControllerActionLocator {
		IEnumerable<LocatedControllerAction> Where(Func<Type, bool> predicate);
	}

	public class ControllerActionLocator : IControllerActionLocator {
		private INamingConventions namingConventions;
		private IEnumerable<Type> types;

		public INamingConventions NamingConventions {
			get { return namingConventions; }
		}

		public ControllerActionLocator(INamingConventions namingConventions) {
			this.namingConventions = namingConventions;
		}

		public IControllerActionLocator FindActionsFromAssemblyContaining<T>() {
			return FindActionsFromAssembly(typeof(T).Assembly);
		}

		public IControllerActionLocator FindActionsFromAssembly(Assembly assembly) {
			return FindActionsFrom(assembly.GetExportedTypes());
		}

		public IControllerActionLocator FindActionsFrom(IEnumerable<Type> types) {
			this.types = types;
			return this;
		}

		IEnumerable<LocatedControllerAction> IControllerActionLocator.Where(Func<Type, bool> predicate) {
			var actionTypes = from type in types
							  where type.IsPublic
							  where !type.IsAbstract
							  where !type.IsInterface
							  where predicate(type)
							  select new LocatedControllerAction(type, namingConventions.BuildKeyFromType(type));

			return actionTypes;
		}
	}

	public class DefaultNamingConventions : INamingConventions {
		public string BuildKeyFromType(Type type) {
			int indexOfControllers = type.FullName.IndexOf("Controllers.");
			return type.FullName.Substring(indexOfControllers + 12).ToLowerInvariant() + ".action";
		}

		public string BuildKeyFromControllerAndAction(string controllerName, string actionName) {
			return controllerName.ToLowerInvariant() + "." + actionName.ToLowerInvariant() + ".action";
		}
	}

	public interface INamingConventions {
		string BuildKeyFromType(Type type);
		string BuildKeyFromControllerAndAction(string controllerName, string actionName);
	}

	public class LocatedControllerAction {

		public Type Type { get; private set; }
		public string Name { get; private set; }


		public LocatedControllerAction(Type type, string name) {
			Type = type;
			Name = name;
		}


	}
}