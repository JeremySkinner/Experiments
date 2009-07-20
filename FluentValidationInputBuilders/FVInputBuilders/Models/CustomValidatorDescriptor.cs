using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Validators;
using InputBuilder;

namespace FVInputBuilders.Models {
	public interface ICustomValidatorDescriptor {
		string GetExample(PropertyInfo prop);
		string GetLabel(PropertyInfo prop);
		string GetPartialName(PropertyInfo prop);
		bool GetIsRequired(PropertyInfo prop);
	}


	public class CustomValidatorDescriptor<T> : ValidatorDescriptor<T>, ICustomValidatorDescriptor {
		public CustomValidatorDescriptor(IEnumerable<IValidationRule<T>> ruleBuilders) : base(ruleBuilders) {
		}


		public string GetExample(PropertyInfo prop) {
			return Rules.OfType<ISimplePropertyRule<T>>()
			       	.Where(x => x.Member == prop)
			       	.Select(x => x.Validator)
			       	.OfType<IExampleMetaData>()
			       	.Select(x => x.Example).FirstOrDefault() ?? string.Empty;
		}

		public string GetLabel(PropertyInfo prop) {
			return Rules.OfType<IPropertyRule<T>>()
					.Where(x => x.Member == prop)
					.Select(x => x.PropertyDescription).FirstOrDefault();
		}

		public string GetPartialName(PropertyInfo prop) {
			return Rules.OfType<ISimplePropertyRule<T>>()
					.Where(x => x.Member == prop)
					.Select(x => x.Validator)
					.OfType<IRenderUsingMetaData>()
					.Select(x => x.ViewName).FirstOrDefault();
		}

		public bool GetIsRequired(PropertyInfo prop) {
			return Rules.OfType<ISimplePropertyRule<T>>()
				.Where(x => x.Member == prop)
				.Select(x => x.Validator)
				.Where(x => x is INotNullValidator || x is INotEmptyValidator)
				.Any();
		}
	}
}