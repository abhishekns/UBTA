# UBTA
Usecase Based Test Automation

This platform allows for graphical and advanced test design for C# classes and APIs. Create a hierarchy based test system that allows for small use-cases to be a part of a bigger usecase for test and automation purposes.

This platform also provides a unique grammer to create and document tests. This can also prove to be basis for writing tests in plain English.


    // below is a regular test code in a slightly more layman readable.
    string t = "a business logic";
    assert.That (t) .IsNotNull ();
    assert.That (null) .IsNull ();
    assert.That (t) .Is (typeof(System.String));
    assert.That (2) .IsEqualTo (2);
    assert.That (2) .IsNotEqualTo (3);
    assert.That (5) .IsGreaterThan(4);
    assert.That (2) .IsLessThan (5);
    assert.That(new object()).And(new object()).AreNotNull();
    assert.That(null).And(null).AreNull();

    string t1 = null;
    string t2 = null;
    string t3 = null;

below describes a way to generate test records that are readable as well as can generate equivalent test records that are layman readable.

    string report = Record(
    new record(() => { That(t).And(t1).And(t2).And(t3).AreNull(); }),
    new record(() => { That(t).IsNotNull(); }));
    
    // print to console or file or display...
    
    System.Console.WriteLine(report);

-----------------------------------------------

Compiling:

ubta.sln can be opened in Visual Studio 2019 or above.

just build the solution.

Installation:

The 'Deployment' folder contains the software. Change the Deployment folder environment variable based on your choice.

set environment variable UBTA_HOME to your deployment directory. 

Running:
* run ubta.SchemaGenTest in $UBTA_HOME\bin\<release>\ directory to generate new schemas, if required.
* run ubta.Reflection.Editable.ClassGeneratorTest.exe in $UBTA_HOME\bin\<release>\ to generate *Activity.dll that will be used by UseCaseDesigner by default. 
* run the ubta.UsecaseDesigner.exe in $UBTA_HOME\bin\<release>\ directory.
    This will open the usecase designer with sample use-case.
* to create your own schema for classes you want to test. 
    Run ubta.SchemaGenTest with the assembly that you want to test. If you run it without any arguments it will just create schema for SampleLib.dll and relevant dlls and exit.


