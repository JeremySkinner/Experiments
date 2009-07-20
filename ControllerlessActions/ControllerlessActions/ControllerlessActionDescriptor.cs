using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace ControllerlessActions {
	public class ControllerlessActionDescriptor : ActionDescriptor {
		private readonly ControllerlessDescriptor parent;
		readonly ParameterDescriptor[] parameterDescriptors;
		private readonly MethodInfo method;


		public ControllerlessActionDescriptor(ControllerlessDescriptor parent, MethodInfo method) {
			this.parent = parent;
			this.method = method;

			parameterDescriptors = method.GetParameters()
				.Select(x => new ReflectedParameterDescriptor(x, this)).ToArray();
		}

		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters) {

			ParameterInfo[] parameterInfos = this.method.GetParameters();
			var rawParameterValues = from parameterInfo in parameterInfos
									 select ExtractParameterFromDictionary(parameterInfo, parameters);
			object[] parametersArray = rawParameterValues.ToArray();

			var action = ((ControllerAdaptor)controllerContext.Controller).Action;

			return method.Invoke(action, parametersArray);
		}

		//Copied from the MVC framework source

		private object ExtractParameterFromDictionary(ParameterInfo parameterInfo, IDictionary<string, object> parameters) {
			object value;

			if (!parameters.TryGetValue(parameterInfo.Name, out value)) {
				// the key should always be present, even if the parameter value is null
				string message = String.Format(CultureInfo.CurrentUICulture, "Could not find a parameter named '{0}' with the type '{1}' on action '{2}'",
					parameterInfo.Name, parameterInfo.ParameterType, parent.Type.Name);

				throw new ArgumentException(message, "parameters");
			}

			if (value == null && !TypeAllowsNullValue(parameterInfo.ParameterType)) {
				// tried to pass a null value for a non-nullable parameter type
				string message = String.Format(CultureInfo.CurrentUICulture, "Value for parameter '{0}' of type '{1}' cannot be null on action '{2}'",
					parameterInfo.Name, parameterInfo.ParameterType, parent.Type.Name);
				throw new ArgumentException(message, "parameters");
			}

			if (value != null && !parameterInfo.ParameterType.IsInstanceOfType(value)) {
				// value was supplied but is not of the proper type
				string message = String.Format(CultureInfo.CurrentUICulture, "Parameter '{0}' has the wrong type",
					parameterInfo.Name);
				throw new ArgumentException(message, "parameters");
			}

			return value;
		}

		private bool TypeAllowsNullValue(Type type) {
			// reference types allow null values
			if (!type.IsValueType) {
				return true;
			}

			// nullable value types allow null values
			// code lifted from System.Nullable.GetUnderlyingType()
			if (type.IsGenericType && !type.IsGenericTypeDefinition && (type.GetGenericTypeDefinition() == typeof(Nullable<>))) {
				return true;
			}

			// no other types allow null values
			return false;
		}

		public override ParameterDescriptor[] GetParameters() {
			return parameterDescriptors;
		}

        public override string ActionName {
			get { return method.Name; }
		}

		public override ControllerDescriptor ControllerDescriptor {
			get { return parent; }
		}

		public override FilterInfo GetFilters() {
			return null; //this is handled by the ControllerlessActionInvoker
		}

	}
}