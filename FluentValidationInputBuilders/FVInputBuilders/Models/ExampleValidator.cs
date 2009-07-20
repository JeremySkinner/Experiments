using FluentValidation.Results;
using FluentValidation.Validators;

namespace FVInputBuilders.Models {
	public interface IExampleMetaData {
		string Example { get; }
	}

	public class ExampleValidator<T, TProperty> : IPropertyValidator<T, TProperty>, IExampleMetaData {
		private string example;

		public ExampleValidator(string example) {
			this.example = example;
		}

		public PropertyValidatorResult Validate(PropertyValidatorContext<T, TProperty> context) {
			return PropertyValidatorResult.Success();
		}

		public string Example {
			get { return example; }
		}
	}
}