namespace FluentValidationIoC.Models {
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class PersonRepository {
		static List<Person> people = new List<Person>();

		static PersonRepository() {
			for(int i = 1; i<=10; i++) {
				var person = new Person() { Id = i, Name = "Person #" + i, DateOfBirth = new DateTime(1980, 1, 1).AddMonths(i) };
				people.Add(person);
			}
		}

		public IEnumerable<Person> FindAll() {
			return people;
		}

		public Person FindById(int id) {
			return people.SingleOrDefault(x => x.Id == id);
		}
	}
}