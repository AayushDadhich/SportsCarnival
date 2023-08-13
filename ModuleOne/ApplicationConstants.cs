namespace WorkshopServer
{
    public class ApplicationConstants
    {
        public const int bytesize = 1024 * 1024;
        public const string ERROR_ResponseModel_Type = "Error";
        public const string ERROR = "error";
        public const string SUCCESS = "success";
        public const string InvalidData_ResponseModel_Message = "Invalid data";
        public const string OutputFilePathHeaderKey = "outputFilePath";
        public const string CreateTeamsSuccessMessage = "Teams created successfully.";
        public const string SomethingWentWrongError = "Something went wrong.";
        public const string InvalidDataReceived = "Data not received correctly.";
        public const string ServerStartedMessage = "Started listening requests at: {0}:{1}";
        public const string CreateTeamsActionType = "create_team";
        public const string GetTeamsActionType = "get_teams";
        public const string UnevenWarningTeamSize = "Number of players are uneven for ";
        public const string InvalidGameType = "Invalid Game Type!";
        public const string TeamNamePrefix = "Team-";
        public const string GetGameDetailsFilePathMessage = "Enter the full path of the file conntaining the game details : ";
        public const string WriteCreatedTeamsPathMessage = "Enter the full path of the file in which you want to write created teams output : ";
        public const string TeamsCreatedSuccessfullyMessage = "Team details written successfully.\n";
        public const string ReterivedTeamsSuccessfully = "Reterieved Teams Successfully!";
        public const string InvalidActionTypeProvided = "Invalid action type provided!";
        public const string CleaningDatabaseLog = "Cleaning Database";
        public const string ResponseSentToClient = "Response sent to client.";
        public const string InvalidPlayerIdExceptionError = "Input data containes an invalid playerId!";
    }
}
