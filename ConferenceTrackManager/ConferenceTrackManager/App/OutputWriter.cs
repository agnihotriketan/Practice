using AppInterfaces;
using static System.Console;

namespace OutputWriter
{
    public class OutputWriterService : IOutputWriterService
    {
        public void WriteMessage(string message)
        {
            WriteLine(message);
        }
    }
}