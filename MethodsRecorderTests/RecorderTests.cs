using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodsRecorder;
using MethodsRecorderTests.ExampleData.Persons;
using MethodsRecorderTests.ExampleData.Accounts;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using MethodsRecorder.Writters;
using MethodsRecorder.RecordingPredicates;

namespace MethodsRecorderTests
{
    [TestClass]
    public class RecorderTests
    {
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
            using var recorder = new Recorder(Path.Combine(resultsFolder, GetCurrentMethod() + fileExtension));

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
        public void Example_Recorder_simple_async()
        {
            using var recorder = new Recorder(Path.Combine(resultsFolder, GetCurrentMethod() + fileExtension));

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
            using var recorder = new Recorder(Path.Combine(resultsFolder, GetCurrentMethod() + fileExtension));

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

        [TestMethod]
        public void Example_Recorder_two_classes()
        {
            using var recorder = new Recorder(Path.Combine(resultsFolder, GetCurrentMethod() + fileExtension));

            var personsDao = new PersonsDao(new PersonsReader());
            var recordedPersonsDao = recorder
                .CreateRecordedObject<IPersonsDao>(personsDao)
                .Object;
            var accountsValuesDao = new AccountValuesDao(new AccountValuesReader(), new CurrentTime());
            var recordedaccountsValuesDao = recorder
                .CreateRecordedObject<IAccountValuesDao>(accountsValuesDao)
                .Object;

            recordedPersonsDao.GetOne("Jan", "Kowalski");
            var a = recordedaccountsValuesDao.GetValue("1-1234-5678-9012", new DateTime(2021, 7, 1, 12, 0, 0));
            recordedPersonsDao.GetOne("Karolina", "Kaczmarek");
            recordedaccountsValuesDao.GetValue("2-0000-1111-2222", new DateTime(2021, 7, 1, 12, 0, 0));
        }

        [TestMethod]
        public void Example_Recorder_constraint()
        {
            using var recorder = new Recorder(Path.Combine(resultsFolder, GetCurrentMethod() + fileExtension));

            var personsDao = new PersonsDao(new PersonsReader());
            var recordedPersonsDao = recorder
                .CreateRecordedObject<IPersonsDao>(personsDao)
                .Record(x => x.Methods("GetOne"), RecordElements.Arguments)
                //.Record(x => x.Methods("GetOne", "GetAllPersons"), RecordElements.All)
                //.Record(x => x.Methods(m => m.Accessibility == MethodAccessibility.Public && m.ReturnType == typeof(int)), RecordElements.ReturnValue)
                //.Record(x => x.Methods("GetOne").WithParameters("parmA"))
                //.Record(x => x.Methods("GetOne").WithParameterValue("ParameterName", val => val.ToString() == "test"))
                .Object;

            recordedPersonsDao.GetOne("Jan", "Kowalski");
            recordedPersonsDao.GetAllPersons();
            recordedPersonsDao.GetCount();
            recordedPersonsDao.GetOne("Marek", "Nowak");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
    }
}
