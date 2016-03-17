using ConferenceHandlerLib;
using AppInterfaces;
using OutputWriter;
using System;
using static System.Console;
using ConferenceManager.AppConstants;
using InputParserServiceLib;

namespace ConferenceTrackManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                WriteLine(AppConstants.welcomeMessage + "\n" + AppConstants.InputFilePathMessage + "\n" + AppConstants.changeInputFilePathMessage);

                var inputPath = ReadLine();
                if (!string.IsNullOrWhiteSpace(inputPath) && inputPath.Trim().ToLower().StartsWith("y"))
                {
                    WriteLine(AppConstants.getInputFilePathMessage);
                    inputPath = ReadLine();
                }
                else
                    inputPath = AppConstants.InputFilePath;

                if (string.IsNullOrWhiteSpace(inputPath))
                    throw new Exception(AppConstants.argumentExceptionMessage);

                using (IConferenceTrackManager conferenceTrackManager = new ConferenceHandlerLib.ConferenceTrackManager(new ConferenceTrackGenerator(), new ParserInputService(), new OutputWriterService()))
                {
                    conferenceTrackManager.GetConferenceTrack(inputPath);
                }

                ReadLine();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
