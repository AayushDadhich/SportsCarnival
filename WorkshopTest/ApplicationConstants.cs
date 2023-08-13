using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopTest
{
    public class ApplicationConstants
    {
        public const string CreateTeamActionType = "create-team";
        public const string LocalIPAddressKey = "localIPAddress";
        public const string WorkshopServerPortKey = "workshopServerPort";
        public const string NoUnevenPlayersExceptionWasThrown = "No uneven players exception was thrown";
        public const string UnevenPlayersException = "UnevenPlayersException";
        public const string NoInvalidGameTypeIdExceptionWasThrown = "No invalid game type Id exception was thrown";
        public const string NoInvalidPlayerIdExceptionWasThrown = "No invalid player Id exception was thrown";
        public const string InvalidGameTypeIdException = "InvalidGameTypeIdException";
        public const string InvalidPlayerIdException = "InvalidPlayerIdException";
        public const string CreateTeamSuccessExpectedFilePath = "D:\\Learn and Code\\LearnAndCode Workshop\\DBIntegration\\learn-code_workshop\\ModuleOne\\MockDataFiles\\Expected Team Creation Response 2.json";
        public const string CreaTeamFailureUnevenPlayersInputFilePath = "D:\\Learn and Code\\LearnAndCode Workshop\\DBIntegration\\learn-code_workshop\\ModuleOne\\MockDataFiles\\Team Creation Request 1.json";
        public const string CreaTeamFailureInvalidPlayerIdInputFilePath = "D:\\Learn and Code\\LearnAndCode Workshop\\DBIntegration\\learn-code_workshop\\ModuleOne\\MockDataFiles\\Team Creation Request Invalid PlayerId.json";
        public const string CreaTeamFailureUnevenPlayersOutputFilePath = "D:\\Learn and Code\\LearnAndCode Workshop\\DBIntegration\\learn-code_workshop\\ModuleOne\\MockDataFiles\\Team Creation Response 1.json";
        public const string CreaTeamFailureInvalidPlayerIdOutputFilePath = "D:\\Learn and Code\\LearnAndCode Workshop\\DBIntegration\\learn-code_workshop\\ModuleOne\\MockDataFiles\\Team Creation Response Invalid PlayerId.json";
        public const string CreateTeamSuccessInputFilePath = "D:\\Learn and Code\\LearnAndCode Workshop\\DBIntegration\\learn-code_workshop\\ModuleOne\\MockDataFiles\\Team Creation Request 2.json";
        public const string CreateTeamSuccessOutputFilePath = "D:\\Learn and Code\\LearnAndCode Workshop\\DBIntegration\\learn-code_workshop\\ModuleOne\\MockDataFiles\\Team Creation Response 2.json";

    }
}
