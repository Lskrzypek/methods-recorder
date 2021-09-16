using System;

namespace MethodsRecorderTests.ExampleData.Persons
{
    public class Person : IEquatable<Person>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public bool Equals(Person other)
        {
            return FirstName == other.FirstName &&
                LastName == other.LastName &&
                Age == other.Age;
        }
    }
}