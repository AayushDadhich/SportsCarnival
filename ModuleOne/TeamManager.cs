using ModuleOne;
using System;
using System.Collections.Generic;

namespace WorkshopServer
{
    public class TeamManager: ITeamManager
    {
        private ITeamRepository _teamRepository;
        public TeamManager(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public TeamList CreateTeam(GameModel game)
        {
                TeamList teamList = new TeamList();
                GameTeamSizeEnum gameSize = GameTeamSizeEnum.Other;
                switch (game.GameType)
                {
                    case GameTypeEnum.Badminton:
                        gameSize = GameTeamSizeEnum.Badminton;
                        break;
                    case GameTypeEnum.Cricket:
                        gameSize = GameTeamSizeEnum.Cricket;
                        break;
                    case GameTypeEnum.Chess:
                        gameSize = GameTeamSizeEnum.Chess;
                        break;
                    default:
                        Console.WriteLine(ApplicationConstants.InvalidGameType);
                        break;
                }
                if (ValidPlayersCount(game, gameSize))
                {
                    //Adding players in DB
                    AddPlayers(game.Players);
                    teamList = CreateTeamByTeamSize(game, gameSize);
                }
                else
                {
                    throw new UnevenPlayersException(ApplicationConstants.UnevenWarningTeamSize + game.GameType);
                }

                //Adding Teams
                AddTeams(teamList, game.GameType);
                return teamList;
        }

        public bool ValidPlayersCount(GameModel game, GameTeamSizeEnum teamSize)
        {
            return (game.Players.Count==0 || game.Players.Count % (int)teamSize != 0) ? false : true;
        }

        public TeamList CreateTeamByTeamSize(GameModel game, GameTeamSizeEnum teamSize)
        {
            int playerCount = 0;
            TeamList teamList = new TeamList();
            TeamModel team = new TeamModel();
            teamList.Teams = new List<TeamModel>();
            teamList.AdditionalPlayers = new List<PlayerModel>();
            team.Players = new List<PlayerModel>();
            teamList.Total = game.Players.Count / (int)teamSize;
            game.Players.ForEach(player =>
            {
                if (playerCount % (int)teamSize == 0 && playerCount != 0)
                {
                    team = SetTeamDetailsByGameDetails(team, game, teamSize, playerCount);
                    teamList.Teams.Add(team);
                    team = new TeamModel();
                    team.Players = new List<PlayerModel>();
                }
                team.Players.Add(new PlayerModel()
                {
                    PlayerId = player.PlayerId,
                    Name = player.Name
                });
                playerCount++;
            });
            team = SetTeamDetailsByGameDetails(team, game, teamSize, playerCount);
            teamList.Teams.Add(team);
            return teamList;
        }

        public TeamModel SetTeamDetailsByGameDetails(TeamModel team, GameModel game, GameTeamSizeEnum teamSize, int playerCount)
        {
            team.Id = playerCount / (int) teamSize;
            team.GameType = game.GameType;
            team.Name = ApplicationConstants.TeamNamePrefix + (char)('A'+ ((playerCount / (int)teamSize)-1));
            return team;
        }

        public void AddPlayers(List<PlayerModel> players)
        {
            try
            {
                var playerRecords = new List<Player>();
                players.ForEach(player =>
                {
                    if (player.PlayerId < 0)
                    {
                        throw new InvalidPlayerIdException(ApplicationConstants.InvalidPlayerIdExceptionError);
                    }
                    playerRecords.Add(new Player()
                    {
                        playerId = player.PlayerId,
                        playerName = player.Name
                    });
                });
                _teamRepository.AddPlayers(playerRecords);
            }
            catch(InvalidPlayerIdException invalidPlayerIdException)
            {
                throw invalidPlayerIdException;
            }
        }

        public void AddTeams(TeamList teamDetails, GameTypeEnum gameType)
        { 
            var teamRecords = new List<Team>();
            var teamPlayerMapping = new List<Team_Player>();
            int lastTeam_PlayerId = _teamRepository.GetLastTeam_PlayerId();
            teamDetails.Teams.ForEach(teamDetail =>
            {
                teamRecords.Add(new Team()
                {
                    teamId = teamDetail.Id,
                    name =   teamDetail.Name,
                    gameId = (int)gameType,
                    eventId = (int)EventTypeEnum.SportsCarnival
                });
                teamDetail.Players.ForEach(player => 
                {
                    teamPlayerMapping.Add(new Team_Player()
                    {
                        id = ++lastTeam_PlayerId,
                        playerId = player.PlayerId,
                        teamId = teamDetail.Id
                    });
                });
            });
            _teamRepository.AddTeams(teamRecords);
            _teamRepository.AddTeam_PlayersMapping(teamPlayerMapping);
        }

        public TeamList GetTeamsAndPlayersByGameId(int gameTypeId)
        {
            List<TeamModel> teams = new List<TeamModel>();
            List<Team> teamsForGameId = _teamRepository.GetTeamsByGameId(gameTypeId);
            teamsForGameId.ForEach(team =>
            {
                List<PlayerModel> playersModel = new List<PlayerModel>();
                List<Player> players = _teamRepository.GetPlayersByTeamId(team.teamId);
                players.ForEach(player => 
                {
                    playersModel.Add(new PlayerModel()
                    {
                        PlayerId = player.playerId,
                        Name = player.playerName
                    });
                });
                teams.Add(new TeamModel()
                {
                    Id = team.teamId,
                    Name = team.name,
                    GameType = (GameTypeEnum)Enum.Parse(typeof(GameTypeEnum), gameTypeId.ToString()),
                    Players = playersModel
                });
            });
            return new TeamList()
            {
                AdditionalPlayers = new List<PlayerModel>(),
                Teams = teams,
                Total = teams.Count
            };
        }

        public List<GameType> GetAllGameTypes()
        {
           return _teamRepository.GetAllGameTypes();
        }

        public void CleanDatabase()
        {
            _teamRepository.CleanDatabase();
        }
    }
}
