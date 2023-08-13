using log4net;
using ModuleOne;
using System;
using System.Collections.Generic;

namespace WorkshopServer
{
    public class TeamController
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(RequestHandler));
        private ITeamManager _teamManager;
        public TeamController(ITeamManager teamManager)
        {
            _teamManager = teamManager;
        }

        public TeamList CreateTeam(GameModel game)
        {
            try
            {
                TeamList teams = _teamManager.CreateTeam(game);
                return teams;
            }
            catch (UnevenPlayersException unevenPlayersException)
            {
                _logger.Error(unevenPlayersException.Message);
                throw unevenPlayersException;
            }
            catch(InvalidPlayerIdException invlaidPlayerIdException)
            {
                _logger.Error(invlaidPlayerIdException.Message);
                throw invlaidPlayerIdException;
            }
            catch (Exception generalException)
            {
                _logger.Error(generalException.Message);
                throw generalException;
            }

        }

        public TeamList GetTeamsAndPlayersByGameId(int gameTypeId)
        {
            try
            {
                var validGameTypes = Enum.GetValues(typeof(GameTypeEnum));
                if(gameTypeId < 1 || gameTypeId > validGameTypes.Length)
                {
                    throw new InvalidGameTypeIdException(ApplicationConstants.InvalidGameType);
                }
                return _teamManager.GetTeamsAndPlayersByGameId(gameTypeId);
            }
            catch(InvalidGameTypeIdException invalidGameTypeIdException)
            {
                _logger.Error(invalidGameTypeIdException.Message);
                throw invalidGameTypeIdException;
            }
        }

        public void CleanDatabase()
        {
            _logger.Error(ApplicationConstants.CleaningDatabaseLog);
            _teamManager.CleanDatabase();
        }
    }
}
