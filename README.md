# UBTA
Usecase Based Test Automation

This platform allows for graphical and advanced test design for C# classes and APIs. Create a hierarchy based test system that allows for small use-cases to be a part of a bigger usecase for test and automation purposes.

This platform also provides a unique grammer to create and document tests. This can also prove to be basis for writing tests in plain English.

---------------------------------------------<br />
// below is a regular test code in a slightly more layman readable.<br />
string t = "a business logic";<br />
assert.That (t) .IsNotNull ();<br />
assert.That (null) .IsNull ();<br />
assert.That (t) .Is (typeof(System.String));<br />
assert.That (2) .IsEqualTo (2);<br />
assert.That (2) .IsNotEqualTo (3);<br />
assert.That (5) .IsGreaterThan(4);<br />
assert.That (2) .IsLessThan (5);<br />
assert.That(new object()).And(new object()).AreNotNull();<br />
assert.That(null).And(null).AreNull();<br /><br />

string t1 = null;<br />
string t2 = null;<br />
string t3 = null;<br />

// below describes a way to generate test records that are readable as well as can generate equivalent test records that are layman readable.<br />

string report = Record(<br />
new record(() => { That(t).And(t1).And(t2).And(t3).AreNull(); }),<br />
new record(()=> {  That(t).IsNotNull(); }));<br />
<br />
// print to console or file or display...<br />
<br />
System.Console.WriteLine(report);<br /><br />

-----------------------------------------------<br />

Compiling:

ubta.sln can be opened in Visual Studio 2015 or above.

just build the solution.

Installation:

The 'Deployment' folder contains the software. Change the Deployment folder environment variable based on your choice.

set environment variable UBTA_HOME to your deployment directory. 

Running:

a) run the ubta.UsecaseDesigner.exe in $UBTA_HOME\bin\<release>\ directory.
    This will open the usecase designer with sample use-case.
b) to create your own schema for classes you want to test. 
    Run ubta.SchemaGenTest with the assembly that you want to test. If you run it without any arguments it will just create schema for SampleLib.dll and relevant dlls and exit.
...

