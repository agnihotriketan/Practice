using AppInterfacesLib;
using System;

namespace OutputHandlerLib
{
    public class ConsoleOutputService : IOutputService
    {
        public void writeMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}