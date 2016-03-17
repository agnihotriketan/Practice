using ConferenceTrackHandler;
using AppInterfaces;
using OutputWriter;
using System;
using static System.Console;
using ConferenceManager.AppConstants;
using InputParserServiceLib;
using System.IO;

namespace ConferenceTrackManager
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                WriteLine(AppConstants.outputDecorator+AppConstants.welcomeMessage + AppConstants.outputDecorator + "\n" + AppConstants.InputFilePathMessage + "\n" + AppConstants.changeInputFilePathMessage);

                string inputPath = ReadLine();
                if (!string.IsNullOrWhiteSpace(inputPath) && inputPath.Trim().ToLower().StartsWith("y"))
                {
                    WriteLine(AppConstants.getInputFilePathMessage);
                    inputPath = ReadLine();
                    //if (File.Exists(inputPath))                        return;  //Validate file exists or not
                }
                else
                    inputPath =   AppConstants.InputFilePath;

                if (string.IsNullOrWhiteSpace(inputPath))
                    throw new Exception(AppConstants.argumentExceptionMessage);

                using (IConferenceTrackManager conferenceTrackManager = new ConferenceTrackHandler.ConferenceTrackManager(new ConferenceTrackGenerator(), new ParserInputService(), new OutputWriterService()))
                {
                    conferenceTrackManager.GetConferenceTrack(inputPath);
                }

                WriteLine(AppConstants.appExit);
                ReadLine();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
