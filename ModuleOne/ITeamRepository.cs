using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopServer
{
    public interface ITeamRepository
    {
        void CleanDatabase();
        void AddPlayers(List<Player> players);
        void AddTeams(List<Team> teams);
        void AddTeam_PlayersMapping(List<Team_Player> teamPlayerMappings);
        int GetLastTeam_PlayerId();
        List<GameType> GetAllGameTypes();
        List<Team> GetTeamsByGameId(int gameTypeId);
        List<Player> GetPlayersByTeamId(int teamId);
    }
}
