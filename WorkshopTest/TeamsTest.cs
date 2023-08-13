using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WorkshopServer;
using CommunicationProtocol;
using System.Configuration;
using ClientServer;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.IO;
using ModuleOne;
using System.Net;

namespace WorkshopTest
{
    [TestClass]
    public class TeamsTest
    {
        TeamController _teamController = new TeamController(new TeamManager(new TeamRepository()));
        static FileHandler _fileHandler = new FileHandler();
        static string localIPAddress = ConfigurationManager.AppSettings.Get(ApplicationConstants.LocalIPAddressKey);
        static int workshopServerPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get(ApplicationConstants.WorkshopServerPortKey));

        [TestMethod]
        public void CreateTeamSuccessTest()
        {
            _teamController.CleanDatabase();
            var iscRequest = GenerateTestISCSuccessRequest();
            GameModel gameDetails = RequestHandler.DeserializeBytesToGameModel(iscRequest.getRequestData());
            TeamList teamList = _teamController.CreateTeam(gameDetails);
            RequestHandler.WriteCreatedTeamsToOutputFilePath(iscRequest.GetOutputFilePath(), teamList);
            string createdTeamsJSON = _fileHandler.ReadCreatedTeamsDetailsToJSON(iscRequest.GetOutputFilePath());
            string expectedCreatedTeamsJSON = _fileHandler.ReadCreatedTeamsDetailsToJSON(ApplicationConstants.CreateTeamSuccessExpectedFilePath);
            TeamList createdTeams = JsonConvert.DeserializeObject<TeamList>(createdTeamsJSON);
            TeamList expectedTeams = JsonConvert.DeserializeObject<TeamList>(expectedCreatedTeamsJSON);
            Assert.AreEqual(createdTeams.AdditionalPlayers.Count, expectedTeams.AdditionalPlayers.Count);
            Assert.AreEqual(createdTeams.Teams.Count, expectedTeams.Teams.Count);
            Assert.AreEqual(createdTeams.Total, expectedTeams.Total);
        }

        [TestMethod]
        public void CreateTeamFailureUnevenPlayersTest()
        {
            try
            {
                _teamController.CleanDatabase();
                var iscRequest = GenerateTestISCFailureUnevenPlayersRequest();
                GameModel gameDetails = RequestHandler.DeserializeBytesToGameModel(iscRequest.getRequestData());
                TeamList createdTeams = _teamController.CreateTeam(gameDetails);
                Assert.Fail(ApplicationConstants.NoUnevenPlayersExceptionWasThrown);
            }
            catch (Exception generalException)
            {
                Assert.AreEqual(generalException.GetType().Name, ApplicationConstants.UnevenPlayersException);
            }
        }

        [TestMethod]
        public void CreateTeamFailureInvalidPlayerIdTest()
        {
            try
            {
                _teamController.CleanDatabase();
                var iscRequest = GenerateTestISCFailureInvalidPlayerIdRequest();
                GameModel gameDetails = RequestHandler.DeserializeBytesToGameModel(iscRequest.getRequestData());
                TeamList createdTeams = _teamController.CreateTeam(gameDetails);
                Assert.Fail(ApplicationConstants.NoUnevenPlayersExceptionWasThrown);
            }
            catch (Exception generalException)
            {
                Assert.AreEqual(generalException.GetType().Name, ApplicationConstants.InvalidPlayerIdException);
            }
        }

        [TestMethod]
        public void GetTeamSuccessTest()
        {
            _teamController.CleanDatabase();
            var iscRequest = GenerateTestISCSuccessRequest();
            GameModel gameDetails = RequestHandler.DeserializeBytesToGameModel(iscRequest.getRequestData());
            _teamController.CreateTeam(gameDetails);
            TeamList teamList = _teamController.GetTeamsAndPlayersByGameId(2);
            string expectedCreatedTeamsJSON = _fileHandler.ReadCreatedTeamsDetailsToJSON(ApplicationConstants.CreateTeamSuccessExpectedFilePath);
            TeamList expectedTeams = JsonConvert.DeserializeObject<TeamList>(expectedCreatedTeamsJSON);
            Assert.AreEqual(teamList.AdditionalPlayers.Count, expectedTeams.AdditionalPlayers.Count);
            Assert.AreEqual(teamList.Teams.Count, expectedTeams.Teams.Count);
            Assert.AreEqual(teamList.Total, expectedTeams.Total);
        }

        [TestMethod]
        public void GetTeamFailureInvalidGameTypeIdTest()
        {
            try
            {
                _teamController.CleanDatabase();
                var iscRequest = GenerateTestISCSuccessRequest();
                GameModel gameDetails = RequestHandler.DeserializeBytesToGameModel(iscRequest.getRequestData());
                TeamList createdTeams = _teamController.CreateTeam(gameDetails);
                TeamList teamList = _teamController.GetTeamsAndPlayersByGameId(9);
                Assert.Fail(ApplicationConstants.NoInvalidGameTypeIdExceptionWasThrown);
            }
            catch(Exception generalException)
            {
                Assert.AreEqual(generalException.GetType().Name, ApplicationConstants.InvalidGameTypeIdException);
            }
        }

        ISCRequest GenerateTestISCFailureUnevenPlayersRequest()
        {
            string getGameDetailsFilePath = ApplicationConstants.CreaTeamFailureUnevenPlayersInputFilePath;
            string gameDetailsJSON = _fileHandler.ReadGameDetailsFromJSON(getGameDetailsFilePath);
            ISCRequest createTeamsISCRequest = new ISCRequest();
            byte[] gameDetailsJSONBytes = JSONSerializer.SerializeObjectToBytes(gameDetailsJSON);
            createTeamsISCRequest.SetIPInformation(localIPAddress, workshopServerPort, localIPAddress, workshopServerPort);
            createTeamsISCRequest.SetDataSize(gameDetailsJSONBytes.Length);
            createTeamsISCRequest.SetData(gameDetailsJSONBytes);
            createTeamsISCRequest.SetActionType(ApplicationConstants.CreateTeamActionType);
            createTeamsISCRequest.SetOutputFilePath(ApplicationConstants.CreaTeamFailureUnevenPlayersOutputFilePath);
            return createTeamsISCRequest;
        }

        ISCRequest GenerateTestISCFailureInvalidPlayerIdRequest()
        {
            string getGameDetailsFilePath = ApplicationConstants.CreaTeamFailureInvalidPlayerIdInputFilePath;
            string gameDetailsJSON = _fileHandler.ReadGameDetailsFromJSON(getGameDetailsFilePath);
            ISCRequest createTeamsISCRequest = new ISCRequest();
            byte[] gameDetailsJSONBytes = JSONSerializer.SerializeObjectToBytes(gameDetailsJSON);
            createTeamsISCRequest.SetIPInformation(localIPAddress, workshopServerPort, localIPAddress, workshopServerPort);
            createTeamsISCRequest.SetDataSize(gameDetailsJSONBytes.Length);
            createTeamsISCRequest.SetData(gameDetailsJSONBytes);
            createTeamsISCRequest.SetActionType(ApplicationConstants.CreateTeamActionType);
            createTeamsISCRequest.SetOutputFilePath(ApplicationConstants.CreaTeamFailureInvalidPlayerIdOutputFilePath);
            return createTeamsISCRequest;
        }

        ISCRequest GenerateTestISCSuccessRequest()
        {
            string getGameDetailsFilePath = ApplicationConstants.CreateTeamSuccessInputFilePath;
            string gameDetailsJSON = _fileHandler.ReadGameDetailsFromJSON(getGameDetailsFilePath);
            ISCRequest createTeamsISCRequest = new ISCRequest();
            byte[] gameDetailsJSONBytes = JSONSerializer.SerializeObjectToBytes(gameDetailsJSON);
            createTeamsISCRequest.SetIPInformation(localIPAddress, workshopServerPort, localIPAddress, workshopServerPort);
            createTeamsISCRequest.SetDataSize(gameDetailsJSONBytes.Length);
            createTeamsISCRequest.SetData(gameDetailsJSONBytes);
            createTeamsISCRequest.SetActionType(ApplicationConstants.CreateTeamActionType);
            createTeamsISCRequest.SetOutputFilePath(ApplicationConstants.CreateTeamSuccessOutputFilePath);
            return createTeamsISCRequest;
        }
    }
}
