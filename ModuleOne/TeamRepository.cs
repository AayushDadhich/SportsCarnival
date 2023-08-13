using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopServer
{
    public class TeamRepository: ITeamRepository
    {
        workshopEntities _dbContext = new workshopEntities();
        public void AddPlayers(List<Player> players)
        {
            _dbContext.Players.AddRange(players);
            _dbContext.SaveChanges();
        }

        public void AddTeams(List<Team> teams)
        {
            _dbContext.Teams.AddRange(teams);
            _dbContext.SaveChanges();
        }

        public void AddTeam_PlayersMapping(List<Team_Player> teamPlayerMappings)
        {
            _dbContext.Team_Player.AddRange(teamPlayerMappings);
            _dbContext.SaveChanges();
        }

        public int GetLastTeam_PlayerId()
        {
            var TeamPlayerRecords = _dbContext.Team_Player.OrderBy(x => x.id);
            return !TeamPlayerRecords.Any() ? 0 : TeamPlayerRecords.FirstOrDefault().id;
        }

        public List<Team> GetTeamsByGameId(int gameTypeId)
        {
            return _dbContext.Teams.Where(x => x.GameType.gameTypeId == gameTypeId).ToList();
        }

        public List<GameType> GetAllGameTypes()
        {
            return _dbContext.GameTypes.Select(x => x).ToList();
        }

        public List<Player> GetPlayersByTeamId(int teamId)
        {
            List<Player> players = new List<Player>();
            var teamPlayerMappings =  _dbContext.Team_Player.Where(teamPlayerRecord => teamPlayerRecord.teamId == teamId).ToList();
            teamPlayerMappings.ForEach(teamPlayerRecord => {
                players.Add(teamPlayerRecord.Player);
            });
            return players;
        }

        public void CleanDatabase()
        {
            var team_player = _dbContext.Team_Player.Select(teamPlayer => teamPlayer);
            if (team_player.Any())
            {
                _dbContext.Team_Player.RemoveRange(team_player);
                _dbContext.SaveChanges();
            }

            var players = _dbContext.Players.Select(player => player);
            if (players.Any())
            {
                _dbContext.Players.RemoveRange(players);
                _dbContext.SaveChanges();
            }

            var teams = _dbContext.Teams.Select(team => team);
            if (teams.Any())
            {
                _dbContext.Teams.RemoveRange(teams);
                _dbContext.SaveChanges();
            }
        }
    }
}
