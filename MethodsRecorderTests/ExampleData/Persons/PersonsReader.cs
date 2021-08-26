using System.Collections.Generic;

namespace MethodsRecorderTests.ExampleData.Persons
{
    public class PersonsReader : IPersonsReader
    {
        private readonly IEnumerable<Person> Persons = new[]
        {
            new Person() { FirstName = "Jan", LastName = "Kowalski", Age = 25 },
            new Person() { FirstName = "Marek", LastName = "Nowak", Age = 30 },
            new Person() { FirstName = "Karolina", LastName = "Kaczmarek", Age = 19 },
            new Person() { FirstName = "Michał", LastName = "Zmaczyński", Age = 14 },
        };

        public IEnumerable<Person> ReadAllPersons()
        {
            return Persons;
        }
    }
}