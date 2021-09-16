# methods-recorder
It is a  small library to recording methods calls including arguments and result values. It create files with all informations and now you can create mock objects with data.

## Example
### Recording
This is an example recording code. PersonsDao is an example of a class that we want to record. In this example, we will record a call to the GetAllPersons method.

```
using (var recorder = new Recorder(resultsFolder))
{
    IPersonsDao personsDao = new PersonsDao(new PersonsReader());
    IPersonsDao personsDaoRecording = recorder.CreateRecordedObject<IPersonsDao>(personsDao).Object;

    recorder.StartRecording();
    var person = personsDaoRecording.GetOne("Jan", "Kowalski");
    recorder.StopRecording();
}
```



First, we need to create a Recorder object. In the constructor you indicate the folder for saving files. Then we create a recorded object based on orginal object. When any method in this object is called, the result and arguments values of that method are written to the file. Every time when we call StartRecording and StopRecording a new file is created. This is what the generated file looks like:

```
{
  "OrderNumber": 0,
  "ClasName": "MethodsRecorderTests.ExampleData.Persons.IPersonsDao",
  "MethodName": "GetOne",
  "ReturnValue": {
    "FirstName": "Jan",
    "LastName": "Kowalski",
    "Age": 25
  },
  "Arguments": [ "Jan", "Kowalski" ]
}
```
