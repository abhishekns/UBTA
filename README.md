# UBTA
use case based testing application

This application allows for graphical and advanced test design for C# classes and APIs. Create a hierarchy based test system that allows for small use-cases to be a part of a bigger usecase for test and automation purposes.

Compiling:

ubta.sln can be opened in Visual Studio 2015 or above.

just build the solution.

Installation:

The 'Deployment' folder contains the software. Change the Deployment folder environment variable based on your choice.

set environment variable UBTA_HOME to your deployment directory. Make sure to keep the ending '\'.

Running:

a) run the ubta.UsecaseDesigner.exe in $UBTA_HOME\bin\<release>\ directory.
    This will open the usecase designer with sample use-case.
b) to create your own schema for classes you want to test. 
    Run ubta.SchemaGenTest with the assembly that you want to test. If you run it without any arguments it will just create schema for SampleLib.dll and relevant dlls and exit.
...
