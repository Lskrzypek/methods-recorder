using System.Collections.Generic;

namespace MethodsRecorderTests.ExampleData.Persons
{
    public interface IPersonsReader
    {
        IEnumerable<Person> ReadAllPersons();
    }
}