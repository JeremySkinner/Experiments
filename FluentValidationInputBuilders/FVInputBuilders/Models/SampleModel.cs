using System;
using System.ComponentModel.DataAnnotations;

namespace FVInputBuilders.Models
{
	public class SampleModel {
		public Guid Guid { get; set; }

//		[Required]
		public string Name { get; set; }

//		[Required]
//		[Label("Number of Types")]
//		[PartialView("RadioButtons")]
		public NumberOfTypeEnum EnumAsRadioButton { get; set; }

		public NumberOfTypeEnum Enum { get; set; }

//		[Range(3, 15)]
		public int IntegerRangeValue { get; set; }

//		[Example("mm/dd/yyyy hh:mm PM")]
		public DateTime TimeStamp { get; set; }

//		[DataType(DataType.MultilineText)]
		public string Html { get; set; }

		public bool IsNeeded { get; set; }
	}

	public enum NumberOfTypeEnum
	{
		One,
		Two,
		Three,
		Four
	}
}