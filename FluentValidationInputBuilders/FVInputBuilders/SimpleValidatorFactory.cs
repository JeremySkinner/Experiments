using System;
using System.Collections.Generic;
using FluentValidation;
using FVInputBuilders.Models;

namespace FVInputBuilders
{
	public class SampleValidatorFactory : IValidatorFactory
	{
		static Dictionary<Type, IValidator> validators = new Dictionary<Type, IValidator>() {
			{ typeof(SampleModel), new SampleModelValidator() }
		};

		public IValidator<T> GetValidator<T>()
		{
			return (IValidator<T>) GetValidator(typeof(T));
		}

		public IValidator GetValidator(Type type)
		{
			IValidator validator;
			if(validators.TryGetValue(type, out validator)) {
				return validator;
			}

			return null;
		}
	}
}