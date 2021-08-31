using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodsRecorder;
using MethodsRecorderTests.ExampleData.Persons;
using MethodsRecorderTests.ExampleData.Accounts;
using System.Linq;
using System;
using MethodsRecorder.Readers;

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

        [TestMethod]
        public void Example_Player_Method_double_called_with_other_body()
        {
            var accountNum = "1-1234-5678-9012";
            var player = this.CreateAccountValuesPlayer();

            var accountValuesPlayerDao = player.CreatePlayingObject<IAccountValuesDao>().Object;
            var value1 = accountValuesPlayerDao.GetCurrent(accountNum);
            var value2 = accountValuesPlayerDao.GetCurrent(accountNum);

            var expectedDao = this.CreateAccountValuesDao();
            var expectedValue1 = expectedDao.GetCurrent(accountNum);
            var expectedValue2 = expectedDao.GetCurrent(accountNum);
            Assert.AreEqual(expectedValue1.Value, value1.Value);
            Assert.AreEqual(expectedValue2.Value, value2.Value);
        }

        [TestMethod]
        public void Example_Player_Two_classes()
        {
            var player = this.CreateTwoClassPlayer();

            var personsPlayerDao = player.CreatePlayingObject<IPersonsDao>().Object;
            var accountValuesPlayerDao = player.CreatePlayingObject<IAccountValuesDao>().Object;
            var person1 = personsPlayerDao.GetOne("Jan", "Kowalski");
            var accountValues1 = accountValuesPlayerDao.GetValue("1-1234-5678-9012", new DateTime(2021, 7, 1, 12, 0, 0));
            var person2 = personsPlayerDao.GetOne("Karolina", "Kaczmarek");
            var accountValues2 = accountValuesPlayerDao.GetValue("2-0000-1111-2222", new DateTime(2021, 7, 1, 12, 0, 0));

            Assert.AreEqual("Kowalski", person1.LastName);
            Assert.AreEqual(1000, accountValues1);
            Assert.AreEqual("Kaczmarek", person2.LastName);
            Assert.AreEqual(50000, accountValues2);
        }

        private PersonsDao CreatePersonsDao() => new PersonsDao(new PersonsReader());
        private AccountValuesDao CreateAccountValuesDao() => new AccountValuesDao(new AccountValuesReader(), new CurrentTime());

        private Player CreatePersonsPlayer() => new Player(new FileReader("TestFiles/persons.txt"));
        private Player CreateAccountValuesPlayer() => new Player(new FileReader("TestFiles/accountValues.txt"));
        private Player CreateTwoClassPlayer() => new Player(new FileReader("TestFiles/twoClasses.txt"));


    }
}
