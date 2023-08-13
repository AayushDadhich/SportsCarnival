using System;
using System.Collections.Generic;
using CommunicationProtocol;
using Newtonsoft.Json;
using WorkshopServer;
using System.IO;

namespace ClientServer
{
    public class ConsoleHandler
    {
        static FileHandler _fileHandler = new FileHandler();
        static ClientServer _clientServer;


        public void StartApplication()
        {
            int userChoice = 0;
            _clientServer = new ClientServer();
            while (userChoice != ApplicationConstants.ExitUserChoice)
            {
                System.Console.WriteLine(ApplicationConstants.MainMenueSrting);
                userChoice = System.Convert.ToInt32(System.Console.ReadLine());
                System.Console.WriteLine();
                switch (userChoice)
                {
                    case 1:
                        ConsoleForCreateTeam();
                        break;
                    case 2:
                        ConsoleForGetTeams();
                        break;
                    case 3:
                        ConsoleForCommandHelp();
                        break;
                    case 4:
                        System.Environment.Exit(0);
                        break;
                    default:
                        System.Console.WriteLine(ApplicationConstants.EnterValidChoice);
                        System.Console.WriteLine();
                        break;
                }
            }
        }

        private void ConsoleForCommandHelp()
        {
            Console.WriteLine(ApplicationConstants.CommandHelpMessage);
            Console.WriteLine();
        }

        private void ConsoleForCreateTeam()
        {
            string getGameDetailsFilePath;
            string writeCreatedTeamsPath;
            string actionType;
            string command;
            System.Console.WriteLine(ApplicationConstants.EnterCommandMessage);
            command = System.Console.ReadLine();
            var commandAttributes = command.Split('\'');
            actionType = commandAttributes[0].Split(' ')[2];
            getGameDetailsFilePath = commandAttributes[1];
            writeCreatedTeamsPath = commandAttributes[3];
            string gameDetailsJSON = _fileHandler.ReadGameDetailsFromJSON(getGameDetailsFilePath);

            //Sending request to server
            ISCRequest createTeamsRequest = _clientServer.GenerateCreateTeamsISCRequest(actionType, gameDetailsJSON, writeCreatedTeamsPath);
            byte[] serializedCreateTeamsRequest = JSONSerializer.SerializeObjectToBytes(createTeamsRequest);
            _clientServer.SendISCRequest(serializedCreateTeamsRequest);
            Console.WriteLine();
            Console.WriteLine(ApplicationConstants.RequestSentMessage);
            Console.WriteLine();

            //Getting response from server
            ICSResponse response = _clientServer.GetCreatedTeamsResponse();
            HandleResponse(response);

            if (response.getStatus().Equals(ApplicationConstants.SUCCESS))
            {
                try
                {
                    DisplayCreatedTeams(writeCreatedTeamsPath);
                }
                catch(FileNotFoundException fileNotFoundException)
                {
                    Console.WriteLine(ApplicationConstants.OutputFileNotFound);
                }
            };
        }

        private void HandleResponse(ICSResponse response)
        {
            string responseMessage;
            switch (response.getStatus())
            {
                case ApplicationConstants.SUCCESS:
                    responseMessage = response.getValue(ApplicationConstants.SuccessMessageHeaderKey);
                    break;
                case ApplicationConstants.ERROR:
                    responseMessage = response.getValue(ApplicationConstants.ErrorMessageHeaderKey);
                    break;
                default:
                    responseMessage = ApplicationConstants.SomethingWentWrongError;
                    break;
            }
            System.Console.WriteLine();
            System.Console.WriteLine(responseMessage);
            System.Console.WriteLine();
            
        }

        public void DisplayCreatedTeams(string writeCreatedTeamsPath)
        {
            string createdTeamsJSON = _fileHandler.ReadCreatedTeamsDetailsToJSON(writeCreatedTeamsPath);
            System.Console.WriteLine("Created Teams :\n " + createdTeamsJSON);
            System.Console.WriteLine();
        }

        private void ConsoleForGetTeams()
        {
            System.Console.WriteLine(ApplicationConstants.EnterCommandMessage);
            string command = System.Console.ReadLine();
            var commandAttributes = command.Split('\'');
            var actionType = commandAttributes[0].Split(' ')[2];

            System.Console.WriteLine();
            System.Console.WriteLine("Enter the game type ID :");
            int gameTypeId = Convert.ToInt32(System.Console.ReadLine());

            //Sending request to server
            ISCRequest createTeamsRequest = _clientServer.GenerateGetTeamsISCRequest(actionType, gameTypeId);
            byte[] serializedCreateTeamsRequest = JSONSerializer.SerializeObjectToBytes(createTeamsRequest);
            _clientServer.SendISCRequest(serializedCreateTeamsRequest);

            //Getting response from server
            ICSResponse response = _clientServer.GetCreatedTeamsResponse();
            HandleResponse(response);
            System.Console.WriteLine(DeserializeBytesToTeamListJSONString(response.data));
            System.Console.WriteLine();
        }

        private string DeserializeBytesToTeamListJSONString(byte[] gameDetailsBytes)
        {
            return System.Text.Encoding.Unicode.GetString(gameDetailsBytes);
        }
    }
}
