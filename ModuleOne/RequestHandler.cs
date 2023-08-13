using CommunicationProtocol;
using log4net;
using ModuleOne;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;

namespace WorkshopServer
{
    public class RequestHandler
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(RequestHandler));
        static ITeamManager _teamManager = new TeamManager(new TeamRepository());
        static TeamController _teamController = new TeamController(_teamManager);
        static Server _server = new Server();
        static NetworkStream stream;
        
        public RequestHandler(NetworkStream networkStream)
        {
            stream = networkStream;
        }

        public static void HandleRequest(ISCRequest request)
        {
            _logger.Info("Client action type : " + request.getActionType());
            try
            {
                switch (request.getActionType())
                {
                    case ApplicationConstants.CreateTeamsActionType:
                        CreateTeam(request.getRequestData(), request.getValue(ApplicationConstants.OutputFilePathHeaderKey));
                        break;
                    case ApplicationConstants.GetTeamsActionType:
                        GetTeamsByGameTypeId(Convert.ToInt32(request.getValue("gameTypeId")));
                        break;
                    default:
                        throw new InvalidActionTypeException(ApplicationConstants.InvalidActionTypeProvided);
                }
            }
            catch (InvalidActionTypeException invalidActionTypeException)
            {
                _logger.Error(invalidActionTypeException.Message);
                byte[] createTeamsResponseMessage = GenerateErrorResponse(invalidActionTypeException.Message);
                SendICSResponse(createTeamsResponseMessage);
            }
            
        }

        private static void CreateTeam(byte[] requestDataBytes, string outputFilePath)
        {
            try
            {
                _teamController.CleanDatabase();
                GameModel gameDetails = DeserializeBytesToGameModel(requestDataBytes);
                TeamList teamList = _teamController.CreateTeam(gameDetails);
                WriteCreatedTeamsToOutputFilePath(outputFilePath, teamList);
                byte[] createTeamsResponseMessage = GenerateCreateTeamSuccessResponse(ApplicationConstants.CreateTeamsSuccessMessage);
                SendICSResponse(createTeamsResponseMessage);
                _logger.Info(ApplicationConstants.ResponseSentToClient);
            }
            catch (UnevenPlayersException unevenPlayersException)
            {
                _logger.Error(unevenPlayersException.Message);
                WriteCreatedTeamErrorModelToOutputFilePath(outputFilePath, GenerateInvalidDataResponseModel());
                byte[] createTeamsResponseMessage = GenerateErrorResponse(unevenPlayersException.Message);
                SendICSResponse(createTeamsResponseMessage);
                _logger.Info(ApplicationConstants.ResponseSentToClient);
            }
            catch (InvalidPlayerIdException invalidPlayerIdException)
            {
                _logger.Error(invalidPlayerIdException.Message);
                byte[] createTeamsResponseMessage = GenerateErrorResponse(invalidPlayerIdException.Message);
                SendICSResponse(createTeamsResponseMessage);
                _logger.Info(ApplicationConstants.ResponseSentToClient);
            }
            catch (Exception generalException)
            {
                _logger.Error(generalException.Message);
                byte[] createTeamsResponseMessage = GenerateErrorResponse(ApplicationConstants.SomethingWentWrongError);
                SendICSResponse(createTeamsResponseMessage);
                _logger.Info(ApplicationConstants.ResponseSentToClient);
            }
        }

        public static void GetTeamsByGameTypeId(int gameTypeId)
        {
            try
            {
                TeamList teamDetails = _teamController.GetTeamsAndPlayersByGameId(gameTypeId);
                byte[] getTeamsResponseMessage = GenerateGetTeamSuccessResponse(teamDetails, ApplicationConstants.ReterivedTeamsSuccessfully);
                SendICSResponse(getTeamsResponseMessage);
                _logger.Info(ApplicationConstants.ResponseSentToClient);
            }
            catch(InvalidGameTypeIdException invalidGameTypeIdException)
            {
                _logger.Error(invalidGameTypeIdException.Message);
                byte[] createTeamsResponseMessage = GenerateErrorResponse(invalidGameTypeIdException.Message);
                SendICSResponse(createTeamsResponseMessage);
                _logger.Info(ApplicationConstants.ResponseSentToClient);
            }
        }

        private static CreateTeamErrorResponseModel GenerateInvalidDataResponseModel()
        {
            CreateTeamErrorResponseModel createdTeamsModel = new CreateTeamErrorResponseModel();
            createdTeamsModel.setType(ApplicationConstants.ERROR_ResponseModel_Type);
            createdTeamsModel.setMessage(ApplicationConstants.InvalidData_ResponseModel_Message);
            return createdTeamsModel;
        }

        private static void SendICSResponse(byte[] iscResponse)
        {
            stream.Write(iscResponse, 0, iscResponse.Length);
        }

        private static byte[] GenerateCreateTeamSuccessResponse(string successMessage)
        {
            ICSResponse iscResponse = new ICSResponse();
            iscResponse.setStatus(ApplicationConstants.SUCCESS);
            iscResponse.setSuccessMessage(successMessage);
            return JSONSerializer.SerializeObjectToBytes(iscResponse);
        }

        private static byte[] GenerateErrorResponse(string errorMessage)
        {
            ICSResponse iscResponse = new ICSResponse();
            iscResponse.setStatus(ApplicationConstants.ERROR);
            iscResponse.setErrorMessage(errorMessage);
            return JSONSerializer.SerializeObjectToBytes(iscResponse);
        }

        private static byte[] GenerateGetTeamSuccessResponse(TeamList teams, string successMessage)
        {
            ICSResponse iscResponse = new ICSResponse();
            iscResponse.setStatus(ApplicationConstants.SUCCESS);
            iscResponse.setSuccessMessage(successMessage);
            iscResponse.setData(JSONSerializer.SerializeObjectToBytes(teams));
            return JSONSerializer.SerializeObjectToBytes(iscResponse);
        }

        //Writes error in the output file
        public static void WriteCreatedTeamErrorModelToOutputFilePath(string outputFilePath, CreateTeamErrorResponseModel createdTeamsModel)
        {
            string teamListJSON = JsonConvert.SerializeObject(createdTeamsModel, Formatting.Indented);
            File.WriteAllText(outputFilePath, teamListJSON);
        }

        //Writes created teams in the output file
        public static void WriteCreatedTeamsToOutputFilePath(string outputFilePath, TeamList teamList)
        {
            string teamListJSON = JsonConvert.SerializeObject(teamList, Formatting.Indented);
            File.WriteAllText(outputFilePath, teamListJSON);
        }

        public static GameModel DeserializeBytesToGameModel(byte[] gameDetailsBytes)
        {
            string requestDataJSON = System.Text.Encoding.Unicode.GetString(gameDetailsBytes);
            requestDataJSON = requestDataJSON.Replace(" ", "");
            requestDataJSON = requestDataJSON.Replace("\n", "");
            requestDataJSON = JsonConvert.DeserializeObject(requestDataJSON).ToString();
            return JsonConvert.DeserializeObject<GameModel>(requestDataJSON);
        }
    }
}
