# methods-recorder
It is a  small library to recording methods calls including arguments and result values. It create files with all informations and now you can create mock objects with data.

<H1>Example</H1>
<H2>Recording</H2>
This is an example recording code. PersonsDao is an example of a class that we want to record. In this example, we will record a call to the GetAllPersons method.

<code>using (var recorder = new Recorder(resultsFolder))
{
    IPersonsDao personsDaoRecording = recorder.CreateRecordedObject<IPersonsDao>(personsDao).Object;

    recorder.StartRecording();
    IPersonsDao personsDao = new PersonsDao(new PersonsReader());
    var personResults = personsDaoRecording.GetAllPersons();
    recorder.StopRecording();
}</code>



First, you need to create a Recorder object. In the constructor you indicate the folder for saving files.
