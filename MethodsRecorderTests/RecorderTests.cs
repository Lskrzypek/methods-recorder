using Microsoft.VisualStudio.TestTools.UnitTesting;
using MethodsRecorder;
using MethodsRecorderTests.ExampleData.Persons;

namespace MethodsRecorderTests
{
    [TestClass]
    public class RecorderTests
    {
        [TestMethod]
        public void Example()
        {
            var recorder = new Recorder("test.txt");

            var personsDao = new PersonsDao(new PersonsReader());
            var recordedPersonsDao = recorder
                .CreateRecordedObject<IPersonsDao>(personsDao)
                .Object;
            
            recordedPersonsDao.GetOne("Jan", "Kowalski");
            recordedPersonsDao.GetAllPersons();
        }
    }
}
