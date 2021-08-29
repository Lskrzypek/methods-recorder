using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodsRecorder;
using MethodsRecorderTests.ExampleData.Persons;
using MethodsRecorderTests.ExampleData.Accounts;
using System;

namespace MethodsRecorderTests
{
    [TestClass]
    public class RecorderTests
    {
        [TestMethod]
        public void Example_Recorder_simple()
        {
            var recorder = new Recorder("test.txt");

            var personsDao = new PersonsDao(new PersonsReader());
            var recordedPersonsDao = recorder
                .CreateRecordedObject<IPersonsDao>(personsDao)
                .Object;
            
            recordedPersonsDao.GetOne("Jan", "Kowalski");
            recordedPersonsDao.GetAllPersons();
            recordedPersonsDao.GetCount();
            recordedPersonsDao.GetOne("Marek", "Nowak");
        }

        [TestMethod]
        public void Example_Recorder_second_time_the_same_method_with_different_body()
        {
            var recorder = new Recorder("test2.txt");

            var account1 = new Account()
            {
                AccountNumber = "2-0000-1111-2222",
                Company = "Test company"
            };
            var account2 = new Account()
            {
                AccountNumber = "1-1234-5678-9012",
                Company = "Other company"
            };

            var accountsValuesDao = new AccountValuesDao(new AccountValuesReader(), new CurrentTime());
            var recordedaccountsValuesDao = recorder
                .CreateRecordedObject<IAccountValuesDao>(accountsValuesDao)
                .Object;

            recordedaccountsValuesDao.GetCurrent(account2.AccountNumber);
            recordedaccountsValuesDao.Get(account1, new DateTime(2021, 7, 1));
            recordedaccountsValuesDao.GetCurrent(account2.AccountNumber);
        }
    }
}
