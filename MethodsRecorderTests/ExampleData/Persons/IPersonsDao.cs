using System.Collections.Generic;

namespace MethodsRecorderTests.ExampleData.Persons
{
    internal interface IPersonsDao
    {
        IEnumerable<Person> GetAdults();
        IEnumerable<Person> GetAllPersons();
        Person GetOne(string firstName, string LastName);
    }
}