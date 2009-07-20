using System;
using System.Reflection;
using FluentValidation;
using InputBuilder;

namespace FVInputBuilders.Models {
	public class FluentValidationConventions {
		private IValidatorFactory validatorFactory;

		public FluentValidationConventions(IValidatorFactory factory) {
			this.validatorFactory = factory;
		}

		private ICustomValidatorDescriptor GetDescriptor(Type type) {
			var validator = validatorFactory.GetValidator(type);
			return (ICustomValidatorDescriptor) validator.CreateDescriptor();
		}

		public string ExampleConvention(PropertyInfo prop) {
			return GetDescriptor(prop.ReflectedType).GetExample(prop);
		}

		public string LabelConvention(PropertyInfo prop) {
			return GetDescriptor(prop.ReflectedType).GetLabel(prop) ?? DefaultConventions.LabelForProperty(prop);
		}

		public string PartialNameConvention(PropertyInfo prop) {
			return GetDescriptor(prop.ReflectedType).GetPartialName(prop) ?? DefaultConventions.PartialName(prop);
		}

		public bool RequiredConvention(PropertyInfo prop) {
			return GetDescriptor(prop.ReflectedType).GetIsRequired(prop);
		}
	}
}