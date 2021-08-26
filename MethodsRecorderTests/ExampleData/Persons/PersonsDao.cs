using System.Collections.Generic;
using System.Linq;

namespace MethodsRecorderTests.ExampleData.Persons
{
    internal class PersonsDao : IPersonsDao
    {
        private readonly IPersonsReader PersonsReader;

        public PersonsDao(IPersonsReader personsReader)
        {
            PersonsReader = personsReader;
        }

        public IEnumerable<Person> GetAllPersons()
        {
            return PersonsReader.ReadAllPersons();
        }

        public Person GetOne(string firstName, string LastName)
        {
            return PersonsReader.ReadAllPersons()
                .FirstOrDefault(x => x.FirstName == firstName && x.LastName == LastName);
        }

        public IEnumerable<Person> GetAdults()
        {
            return PersonsReader.ReadAllPersons()
                .Where(x => x.Age >= 18);
        }
    }
}