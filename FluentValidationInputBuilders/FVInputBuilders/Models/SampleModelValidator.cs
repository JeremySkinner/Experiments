using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace FVInputBuilders.Models {
	public class SampleModelValidator : AbstractValidator<SampleModel> {
		public SampleModelValidator() {
			RuleFor(x => x.Name).NotEmpty();

			RuleFor(x => x.EnumAsRadioButton)
				.NotNull()
				.RenderUsing("RadioButtons")
				.WithName("Number of Types");

			RuleFor(x => x.IntegerRangeValue)
				.GreaterThanOrEqualTo(3)
				.LessThanOrEqualTo(15);

			RuleFor(x => x.TimeStamp).Example("mm/dd/yyyy hh:mm PM");

			RuleFor(x => x.Html).DataType(DataType.MultilineText);
		}

		public override IValidatorDescriptor<SampleModel> CreateDescriptor() {
			return new CustomValidatorDescriptor<SampleModel>(this);
		}
	}
}