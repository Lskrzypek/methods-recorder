using MethodsRecorder;
using MethodsRecorderTests.ExampleData.Persons;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MethodsRecorderTests
{
    [TestClass]
    public class AllTests
    {
        private readonly string resultsFolder = "TestResultsAll";

        [TestInitialize]
        public void Initialize()
        {
            if(Directory.Exists(resultsFolder))
            {
                Directory.Delete(resultsFolder, true);
            }

            if (!Directory.Exists(resultsFolder))
            {
                Directory.CreateDirectory(resultsFolder);
            }
        }
          
        [TestMethod]
        public void Example_All_Simple()
        {
            // Recording
            IPersonsDao personsDao = new PersonsDao(new PersonsReader());
            IEnumerable<Person> personsResults;

            using (var recorder = new Recorder(resultsFolder))
            {
                IPersonsDao personsDaoRecording = recorder.CreateRecordedObject<IPersonsDao>(personsDao).Object;
                
                recorder.StartRecording();
                personsResults = personsDaoRecording.GetAllPersons();
                recorder.StopRecording();
            }


            // Playing
            var filePath = GetFirstFile();
            IEnumerable<Person> personsPlayerResults;

            var player = new Player(filePath);
            IPersonsDao personsDaoPlayer = player.CreatePlayingObject<IPersonsDao>().Object;
            personsPlayerResults = personsDaoPlayer.GetAllPersons();

            // Assert
            Assert.IsTrue(Enumerable.SequenceEqual(personsResults, personsPlayerResults));
        }

        private string GetFirstFile()
        {
            return Directory.GetFiles(resultsFolder).FirstOrDefault();
        }
    }
}
