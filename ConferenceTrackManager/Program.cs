using ConferenceHandlerLib;
using AppInterfacesLib;
using OutputHandlerLib;
using System;


namespace ConferenceTrackManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("Welcome Conference Track Manager");
                Console.WriteLine("Input file path is 'D:/MyGit/Practice/ConferenceTrackManager/Input/input.txt' ");
                Console.WriteLine("Do you want to change track input file path ? ");
                var inputPath = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(inputPath) && inputPath.Trim().ToLower().StartsWith("y"))
                {
                    Console.WriteLine("Please enter full input file path:");
                    inputPath = Console.ReadLine();
                }
                else
                    inputPath = "D:/MyGit/Practice/ConferenceTrackManager/Input/input.txt";

                if (string.IsNullOrWhiteSpace(inputPath))
                    throw new Exception("no argument found");

                IConferenceTrackManager conferenceTrackManager = new ConferenceTrackManagement(new ConferenceTrackGenerator(), new InputParserService(), new ConsoleOutputService());
                conferenceTrackManager.getConferenceTrack(inputPath);

                Console.ReadLine();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
