# methods-recorder
It is a  small library to recording methods calls including arguments and result values. It create files with all informations and generate mock objects with data. This can be helpful in creating unit tests or in debugging when we do not have access to the production database.

## Example
### Recording
This is an example recording code. PersonsDao is an example of a class that reads some data from a database. In this example, we will record a call to the GetOne method.

```
using (var recorder = new Recorder(resultsFolder))
{
    IPersonsDao personsDao = new PersonsDao();
    IPersonsDao personsDaoRecording = recorder.CreateRecordedObject<IPersonsDao>(personsDao).Object;

    recorder.StartRecording();
    var person = personsDaoRecording.GetOne("Jan", "Kowalski");
    recorder.StopRecording();
}
```



First, we need to create a Recorder object. In the constructor we select the folder for saving files. Then we create a recorded object based on orginal object. When any method in this object is called, the result and arguments values of that method are written to the file. Every time when we call StartRecording and StopRecording a new file is created. This is what the generated file looks like (MethodsRecorder_2021_09_16_16_41_49.txt):

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

###Playing
OK. We have a history. Now we can run the method GetOne("Jan", "Kowalski") even if we don't have access to the database at this moment. This can be helpful in creating unit tests or in debugging when we do not have access to the production database. All the necessary information is contained in the file.

```
var player = new Player("MethodsRecorder_2021_09_16_16_41_49.txt");
IPersonsDao personsDaoPlayer = player.CreatePlayingObject<IPersonsDao>().Object;
var person = personsDaoPlayer.GetOne("Jan", "Kowalski");
```

We need to create a Player object and select the file with the recorded methods. Then we create mocked object that implement our interface. In this time we don't use the PersonsDao implementation.
