using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodsRecorder;
using MethodsRecorderTests.ExampleData.Persons;
using System.Linq;

namespace MethodsRecorderTests
{
    [TestClass]
    public class RecorderTests
    {
        [TestMethod]
        public void Example_Recorder()
        {
            var recorder = new Recorder("test.txt");

            var personsDao = new PersonsDao(new PersonsReader());
            var recordedPersonsDao = recorder
                .CreateRecordedObject<IPersonsDao>(personsDao)
                .Object;
            
            recordedPersonsDao.GetOne("Jan", "Kowalski");
            recordedPersonsDao.GetAllPersons();
            recordedPersonsDao.GetCount();
            recordedPersonsDao.GetOne("Marian", "Nowak");
        }

        [TestMethod]
        public void Example_Player_Method_without_parameter_and_simple_return_type()
        {
            var player = new Player("test.txt");

            var personsPlayerDao = player.CreatePlayingObject<IPersonsDao>().Object;
            var count = personsPlayerDao.GetCount();

            var expectedCount = new PersonsDao(new PersonsReader()).GetCount();
            Assert.AreEqual(expectedCount, count);
        }

        [TestMethod]
        public void Example_Player_Method_without_parameter_and_complex_return_type()
        {
            var player = new Player("test.txt");

            var personsPlayerDao = player.CreatePlayingObject<IPersonsDao>().Object;
            var persons = personsPlayerDao.GetAllPersons();

            var expectedPersons = new PersonsDao(new PersonsReader()).GetAllPersons();
            Assert.AreEqual(expectedPersons.Count(), persons.Count());
        }

        [TestMethod]
        public void Example_Player_Method_with_parameters_and_complex_return_type()
        {
            var player = new Player("test.txt");

            var personsPlayerDao = player.CreatePlayingObject<IPersonsDao>().Object;
            var person = personsPlayerDao.GetOne("Jan", "Kowalski");

            var expectedPerson = new PersonsDao(new PersonsReader()).GetOne("Jan", "Kowalski");
            Assert.AreEqual(expectedPerson.LastName, person.LastName);
        }
    }
}
