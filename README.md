Problem: 
Conference Track Management

Language, Technologie & Tools Used:
C#6.0, .NET Framework 4.5.2 Visual studio 2015.

Input: 
I am giving input to application from "~/AppInputs/input.txt".
I have declared a common filepath in AppConstants.cs with member name as "InputFilePath".
I am asking user to provide his own file's path as well.

Application Code Description:

"Program.cs"- Application Entry point.

"AppConstants.cs"- All app level constant strings are declared / intialised in this static class to make them accessible throughout application.

"ExceptionFilePath"- variable in AppConstants.cs is the file path where I am locking all application Exceptions.

"AppInterfaces.cs"- All application interfaces are declared in this file.

"OutputWriter.cs"- Class who s resonsible to write all output to console & writing exception's to exception file w.r.t single responsibility principle implementation.

"ParserInputService .cs"- It is a class to format all tracks inputs provided through input file.

"ConferenceTrackGenerator.cs"- Class responsible to create conference tracks.


Unit Test Cases are implemented by using visual studio's MS test liabrary.
