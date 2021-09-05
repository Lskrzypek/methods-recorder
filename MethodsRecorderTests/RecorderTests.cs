using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodsRecorder;
using MethodsRecorderTests.ExampleData.Persons;
using MethodsRecorderTests.ExampleData.Accounts;
using System;
using System.IO;
using MethodsRecorder.RecordingPredicates;
using System.Linq;

namespace MethodsRecorderTests
{
    [TestClass]
    public class RecorderTests
    {
        public TestContext TestContext { get; set; }

        private readonly string resultsFolder = "TestResults";
        private readonly string fileExtension = ".txt";

        [TestInitialize]
        public void Initialize()
        {
            if(!Directory.Exists(resultsFolder))
            {
                Directory.CreateDirectory(resultsFolder);
            }
        }

        [TestMethod]
        public void Example_Recorder_simple()
        {
            var fileName = DeleteOldTestFile();
            var personsDao = new PersonsDao(new PersonsReader());

            using (var recorder = new Recorder(fileName))
            {
                var recordedPersonsDao = recorder.CreateRecordedObject<IPersonsDao>(personsDao).Object;
                recordedPersonsDao.GetCount();
            }

            AssertFileHasLines(fileName, 1);
        }

        [TestMethod]
        public void Example_Recorder_simple_async()
        {
            var fileName = DeleteOldTestFile();
            var personsDao = new PersonsDao(new PersonsReader());

            using (var recorder = new Recorder(fileName, true))
            {
                var recordedPersonsDao = recorder.CreateRecordedObject<IPersonsDao>(personsDao).Object;
                recordedPersonsDao.GetCount();
            }

            AssertFileHasLines(fileName, 1);
        }

        [TestMethod]
        public void Example_Recorder_many_methods()
        {
            var fileName = DeleteOldTestFile();
            var personsDao = new PersonsDao(new PersonsReader());

            using (var recorder = new Recorder(fileName))
            {
                var recordedPersonsDao = recorder.CreateRecordedObject<IPersonsDao>(personsDao).Object;
                recordedPersonsDao.GetOne("Jan", "Kowalski");
                recordedPersonsDao.GetAllPersons();
                recordedPersonsDao.GetCount();
                recordedPersonsDao.GetOne("Marek", "Nowak");
            }

            AssertFileHasLines(fileName, 4);
        }

        [TestMethod]
        public void Example_Recorder_second_time_the_same_method_with_different_body()
        {
            var fileName = DeleteOldTestFile();
            var accountsValuesDao = new AccountValuesDao(new AccountValuesReader(), new CurrentTime());
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
            AccountValue AccountValueA, AccountValueB;

            using (var recorder = new Recorder(fileName, true))
            {
                var recordedaccountsValuesDao = recorder.CreateRecordedObject<IAccountValuesDao>(accountsValuesDao).Object;

                AccountValueA = recordedaccountsValuesDao.GetCurrent(account2.AccountNumber);
                recordedaccountsValuesDao.Get(account1, new DateTime(2021, 7, 1));
                AccountValueB = recordedaccountsValuesDao.GetCurrent(account2.AccountNumber);
            }

            AssertFileHasLines(fileName, 3);
            Assert.AreNotEqual(AccountValueA.Value, AccountValueB.Value);
        }

        [TestMethod]
        public void Example_Recorder_two_classes()
        {
            var fileName = DeleteOldTestFile();
            var accountsValuesDao = new AccountValuesDao(new AccountValuesReader(), new CurrentTime());
            var personsDao = new PersonsDao(new PersonsReader());

            using (var recorder = new Recorder(fileName))
            {
                var recordedPersonsDao = recorder.CreateRecordedObject<IPersonsDao>(personsDao).Object;
                var recordedaccountsValuesDao = recorder.CreateRecordedObject<IAccountValuesDao>(accountsValuesDao).Object;

                recordedPersonsDao.GetOne("Jan", "Kowalski");
                recordedaccountsValuesDao.GetValue("1-1234-5678-9012", new DateTime(2021, 7, 1, 12, 0, 0));
                recordedPersonsDao.GetOne("Karolina", "Kaczmarek");
                recordedaccountsValuesDao.GetValue("2-0000-1111-2222", new DateTime(2021, 7, 1, 12, 0, 0));
            }

            AssertFileHasLines(fileName, 4);
        }

        [TestMethod]
        public void Example_Recorder_constraints_methods()
        {
            var fileName = DeleteOldTestFile();
            var personsDao = new PersonsDao(new PersonsReader());

            using (var recorder = new Recorder(fileName))
            {
                var recordedPersonsDao = recorder
                    .CreateRecordedObject<IPersonsDao>(personsDao)
                    .Record(x => x.Methods("GetOne"), RecordElements.Arguments)
                    .Object;

                recordedPersonsDao.GetOne("Jan", "Kowalski");
                recordedPersonsDao.GetAllPersons();
                recordedPersonsDao.GetCount();
                recordedPersonsDao.GetOne("Marek", "Nowak");
            }

            AssertFileHasLines(fileName, 2);
        }

        [TestMethod]
        public void Example_Recorder_constraints_allMethods()
        {
            var fileName = DeleteOldTestFile();
            var personsDao = new PersonsDao(new PersonsReader());

            using (var recorder = new Recorder(fileName))
            {
                var recordedPersonsDao = recorder
                    .CreateRecordedObject<IPersonsDao>(personsDao)
                    .Record(x => x.AllMethods())
                    .Object;

                recordedPersonsDao.GetOne("Jan", "Kowalski");
                recordedPersonsDao.GetAllPersons();
                recordedPersonsDao.GetCount();
                recordedPersonsDao.GetOne("Marek", "Nowak");
            }

            AssertFileHasLines(fileName, 4);
        }

        [TestMethod]
        public void Example_Recorder_constraints_methods_func()
        {
            var fileName = DeleteOldTestFile();
            var personsDao = new PersonsDao(new PersonsReader());

            using (var recorder = new Recorder(fileName))
            {
                var recordedPersonsDao = recorder
                    .CreateRecordedObject<IPersonsDao>(personsDao)
                    .Record(x => x.Methods(m => 
                        m.Accessibility == MethodAccessibility.Public && 
                        m.ReturnType == typeof(int)))
                    .Object;

                recordedPersonsDao.GetOne("Jan", "Kowalski");
                recordedPersonsDao.GetAllPersons();
                recordedPersonsDao.GetCount();
                recordedPersonsDao.GetOne("Marek", "Nowak");
            }

            AssertFileHasLines(fileName, 1);
        }

        [TestMethod]
        public void Example_Recorder_constraints_methods_method_withParameters()
        {
            var fileName = DeleteOldTestFile();
            var personsDao = new PersonsDao(new PersonsReader());

            Person person, person2;

            using (var recorder = new Recorder(fileName))
            {
                var recordedPersonsDao = recorder
                    .CreateRecordedObject<IPersonsDao>(personsDao)
                    .Record(x => x.Methods("GetOne").WithParameters("firstName", "lastName"))
                    .Object;

                person = recordedPersonsDao.GetOne("Jan", "Kowalski");
                recordedPersonsDao.GetAllPersons();
                recordedPersonsDao.GetCount();
                person2 = recordedPersonsDao.GetOne(person);
            }

            Assert.AreEqual(person.LastName, person2.LastName);
            AssertFileHasLines(fileName, 1);
        }

        [TestMethod]
        public void Example_Recorder_constraints_methods_method_withParametersValues()
        {
            var fileName = DeleteOldTestFile();
            var personsDao = new PersonsDao(new PersonsReader());

            using (var recorder = new Recorder(fileName))
            {
                var recordedPersonsDao = recorder
                    .CreateRecordedObject<IPersonsDao>(personsDao)
                    .Record(x => 
                        x.Methods("GetOne")
                            .WithParameterValue("firstName", p => p.ToString() == "Jan")
                            .WithParameterValue("lastName", p => p.ToString() == "Kowalski"))
                    .Object;

                recordedPersonsDao.GetOne("Jan", "Kowalski");
                recordedPersonsDao.GetOne("Marek", "Nowak");
                recordedPersonsDao.GetOne("Jan", "Kowalski");
                recordedPersonsDao.GetOne("Micha³", "Zmaczyñski");
            }

            AssertFileHasLines(fileName, 2);
        }

        private string DeleteOldTestFile()
        {
            var fileName = GetFileNameFromTestName();
            if(File.Exists(fileName))
                File.Delete(fileName);
            return fileName;
        }

        private string GetFileNameFromTestName()
        {
            return Path.Combine(resultsFolder, TestContext.TestName + fileExtension);
        }

        private static void AssertFileHasLines(string fileName, int expectedLinesCount)
        {
            var linesInFile = File.ReadLines(fileName).Count();
            Assert.AreEqual(linesInFile, expectedLinesCount);
        }
    }
}
