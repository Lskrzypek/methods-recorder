using System.Collections.Generic;

namespace MethodsRecorderTests.ExampleData.Persons
{
    internal interface IPersonsReader
    {
        IEnumerable<Person> ReadAllPersons();
    }
}