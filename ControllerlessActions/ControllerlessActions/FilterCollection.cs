using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace ControllerlessActions {
	//Why do filters not share a common IFilter interface?
	public class FilterCollection : IEnumerable<FilterConfiguration> {
		List<FilterConfiguration> filters = new List<FilterConfiguration>();

		public IFilterConfiguration Apply<T>() {
			var config = new FilterConfiguration(typeof(T));
			filters.Add(config);
			return config;
		}

		public IEnumerator<FilterConfiguration> GetEnumerator() {
			return filters.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}

	public class FilterConfiguration : IFilterConfiguration {
		private Func<ControllerlessDescriptor, bool> predicate = x=>true;

		public FilterConfiguration(Type filterType) {
			FilterType = filterType;
		}

		public Type FilterType { get; private set; }

		public void When(Func<ControllerlessDescriptor, bool> predicate) {
			this.predicate = predicate;
		}

		public bool AppliesTo(ControllerlessDescriptor descriptor) {
			return predicate(descriptor);
		}
	}

	public interface IFilterConfiguration {
		void When(Func<ControllerlessDescriptor, bool> predicate);
	}

	public static class FilterCollectionExtensions {
		public static void Always(this IFilterConfiguration cfg) {
			cfg.When(x => true);
		}

		public static void ForTypesAssignableTo<T>(this IFilterConfiguration cfg) {
			cfg.When(x => x.ActionInstance is T);
		}
	}
}