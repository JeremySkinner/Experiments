namespace FluentValidationIoC.Models {
	using System;
	using FluentValidation;

	public class PersonValidator : AbstractValidator<Person> {
		public PersonValidator() {
			RuleFor(x => x.Name).NotNull();
			RuleFor(x => x.DateOfBirth).Must(BeOver18).WithMessage("Must be over 18 years old.");
		}

		bool BeOver18(DateTime dateOfBirth) {
			var now = DateTime.Today;
			int years = now.Year - dateOfBirth.Year;

			if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) {
				--years;
			}

			return years >= 18;
		}
	}
}