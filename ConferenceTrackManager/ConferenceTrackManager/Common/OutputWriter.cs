using AppInterfaces;
using static System.Console;
using System.IO;
using ConferenceManager.AppConstants;

namespace OutputWriter
{
    public class OutputWriterService : IOutputWriterService
    {
        public void WriteMessage(string message)
        {
            WriteLine(message);
        }
        public void LogExceptions(string message)
        {
            File.AppendAllText(AppConstants.ExceptionFilePath, "\n" + message);
        }
    }
}