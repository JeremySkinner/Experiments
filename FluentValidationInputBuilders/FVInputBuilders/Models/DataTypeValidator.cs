using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;
using FluentValidation.Validators;

namespace FVInputBuilders.Models {
	public interface IDataTypeMetaData {
		DataType DataType { get; }
	}

	public class DataTypeValidator<T, TProperty> : IPropertyValidator<T, TProperty>, IDataTypeMetaData
	{
		private DataType datatype;

		public DataTypeValidator(DataType datatype)
		{
			this.datatype = datatype;
		}

		public PropertyValidatorResult Validate(PropertyValidatorContext<T, TProperty> context)
		{
			return PropertyValidatorResult.Success();
		}

		public DataType DataType
		{
			get { return datatype; }
		}
	}
}