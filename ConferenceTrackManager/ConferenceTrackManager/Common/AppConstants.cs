namespace ConferenceManager.AppConstants
{
    public static class AppConstants
    { 
        public static string welcomeMessage { get; } = "\t Welcome Conference Track Manager";
        public static string InputFilePathMessage { get; } = "Input file path is 'D:/MyGit/Practice/ConferenceTrackManager/Input/input.txt' ";
        public static string InputFilePath { get; } = "D:/MyGit/Practice/ConferenceTrackManager/ConferenceTrackManager/AppInputs/input.txt";
        public static string ExceptionFilePath { get; } = "D:/MyGit/Practice/ConferenceTrackManager/ConferenceTrackManager/AppLogsExceptions/AppLogsExceptions.txt";
        public static string argumentExceptionMessage { get; } = "no argument found";
        public static string getInputFilePathMessage { get; } = "Please enter full input file path:";
        public static string changeInputFilePathMessage { get; } = "Do you want to change track input file path ? ";
        public static string networkingEvent { get; } = "Networking Event";
        public static string lunchEvent { get; } = "Lunch";
        public static string outputDecorator { get; } = "\n * * * * * * * * * * * * * * * * * * * * * * * * \n";
        public static string appExit { get; } = "Your conference tracks are ready. \n Press any key to exit";

    }
}