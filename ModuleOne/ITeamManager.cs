using System.Collections.Generic;

namespace WorkshopServer
{
    public interface ITeamManager
    {
        void CleanDatabase();
        TeamList CreateTeam(GameModel game);
        void AddPlayers(List<PlayerModel> players);
        void AddTeams(TeamList teamDetails, GameTypeEnum gameType);
        List<GameType> GetAllGameTypes();
        TeamList GetTeamsAndPlayersByGameId(int gameTypeId);
    }
}
