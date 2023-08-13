namespace ClientServer
{
    public class ApplicationConstants
    {
        public const int Bytesize = 1024 * 1024;
        public const int ExitUserChoice = 4;
        public const int Portnumber = 8888;
        public const string HostName = "localhost";
        public const string SUCCESS = "success";
        public const string ERROR = "error";
        public const string SuccessMessageHeaderKey = "success-message";
        public const string ErrorMessageHeaderKey = "error-message";
        public const string MainMenueSrting = "1.Create Teams\n2.Get Teams\n3.Command Help\n4.Exit\n\nEnter your choice : ";
        public const string EnterCommandMessage = "Enter the command :";
        public const string EnterGameTypeCommand = "Enter game type : ";
        public const string GetGameDetailsFilePathMessage = "Enter the full path of the file conntaining the game details : ";
        public const string WriteCreatedTeamsPathMessage = "Enter the full path of the file in which you want to write created teams output : ";
        public const string TeamsCreatedSuccessfullyMessage = "Team details written successfully.\n";
        public const string SomethingWentWrongError = "Something went wrong.";
        public const string ConnectionErrorMessage = "Cannot connect to server!";
        public const string RequestSentMessage = "Request sent to server.";
        public const string EnterValidChoice = "Enter valid choice";
        public const string OutputFileNotFound = "Output file not found!";
        public const string CommandHelpMessage = "Command Format:\nCreate Team : isc -a create_teams -i [input-file-path] -o [output-file-path]\nGet Teams : isc -a get_teams";
    }
}
