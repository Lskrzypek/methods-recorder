using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodsRecorder;
using MethodsRecorderTests.ExampleData.Persons;
using System.Linq;

namespace MethodsRecorderTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Example_Player_Method_without_parameter_and_simple_return_type()
        {
            var player = this.CreatePersonsPlayer();

            var personsPlayerDao = player.CreatePlayingObject<IPersonsDao>().Object;
            var count = personsPlayerDao.GetCount();

            var expectedCount = this.CreatePersonsDao().GetCount();
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void Example_Player_Method_without_parameter_and_complex_return_type()
        {
            var player = this.CreatePersonsPlayer();

            var personsPlayerDao = player.CreatePlayingObject<IPersonsDao>().Object;
            var persons = personsPlayerDao.GetAllPersons();

            var expectedPersons = this.CreatePersonsDao().GetAllPersons();
            Assert.AreEqual(expectedPersons.Count(), persons.Count());
        }

        [TestMethod]
        public void Example_Player_Method_with_parameters_and_complex_return_type()
        {
            var player = this.CreatePersonsPlayer();

            var personsPlayerDao = player.CreatePlayingObject<IPersonsDao>().Object;
            var person = personsPlayerDao.GetOne("Jan", "Kowalski");

            var expectedPerson = this.CreatePersonsDao().GetOne("Jan", "Kowalski");
            Assert.AreEqual(expectedPerson.LastName, person.LastName);
        }

        private PersonsDao CreatePersonsDao()
        {
            return new PersonsDao(new PersonsReader());
        }

        private Player CreatePersonsPlayer()
        {
            return new Player("TestFiles/persons.txt");
        }
    }
}
