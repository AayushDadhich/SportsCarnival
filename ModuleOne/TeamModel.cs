using System.Collections.Generic;

namespace WorkshopServer
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GameTypeEnum GameType { get; set; }
        public List<PlayerModel> Players { get; set; }
    }

    public class TeamList
    {
        public List<TeamModel> Teams { get; set; }
        public int Total { get; set; }
        public List<PlayerModel> AdditionalPlayers;

    }
}
