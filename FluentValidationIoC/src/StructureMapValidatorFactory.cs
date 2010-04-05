namespace FluentValidationIoC {
	using System;
	using FluentValidation;
	using StructureMap;

	public class StructureMapValidatorFactory : ValidatorFactoryBase {
		public override IValidator CreateInstance(Type validatorType) {
			return ObjectFactory.TryGetInstance(validatorType) as IValidator;
		}
	}
}