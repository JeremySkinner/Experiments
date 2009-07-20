using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Tests {
	public class FakeServiceLocator : ServiceLocatorImplBase {
		Dictionary<Type, object> instances = new Dictionary<Type, object>();

		public void Add<T>(T instance) {
			instances[typeof(T)] = instance;
		}
		

		protected override object DoGetInstance(Type serviceType, string key) {

			if(instances.ContainsKey(serviceType)) {
				return instances[serviceType];
			}

			return Activator.CreateInstance(serviceType);
		}

		protected override IEnumerable<object> DoGetAllInstances(Type serviceType) {
			return Enumerable.Empty<object>();
		}
	}
}