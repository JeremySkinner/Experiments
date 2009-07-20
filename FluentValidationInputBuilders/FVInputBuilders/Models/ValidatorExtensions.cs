using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace FVInputBuilders.Models {
	public static class ValidatorExtensions {
		public static IRuleBuilderOptions<T, TProperty> DataType<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder,
		                                                                       DataType dataType) {
			return ruleBuilder.SetValidator(new DataTypeValidator<T, TProperty>(dataType));
		}

		public static IRuleBuilderOptions<T, TProperty> Example<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder,
		                                                                      string example) {
			return ruleBuilder.SetValidator(new ExampleValidator<T, TProperty>(example));
		}


		public static IRuleBuilderOptions<T, TProperty> RenderUsing<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder,
		                                                                          string viewname) {
			return ruleBuilder.SetValidator(new RenderUsingValidator<T, TProperty>(viewname));
		}
	}
}