using FluentValidation.Results;
using FluentValidation.Validators;

namespace FVInputBuilders.Models {
	public interface IRenderUsingMetaData {
		string ViewName { get; }
	}

	public class RenderUsingValidator<T, TProperty> : IPropertyValidator<T, TProperty>, IRenderUsingMetaData {
		private string viewname;

		public RenderUsingValidator(string viewname) {
			this.viewname = viewname;
		}

		public PropertyValidatorResult Validate(PropertyValidatorContext<T, TProperty> context) {
			return PropertyValidatorResult.Success();
		}

		public string ViewName {
			get { return viewname; }
		}
	}
}