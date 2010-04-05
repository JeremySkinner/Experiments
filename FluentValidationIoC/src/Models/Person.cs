namespace FluentValidationIoC.Models {
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Person {
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime DateOfBirth { get; set; }
	}

}