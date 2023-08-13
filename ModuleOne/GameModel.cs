using System.Collections.Generic;

namespace WorkshopServer
{
    public class GameModel
    {
        public GameTypeEnum GameType { get; set; }
        public List<PlayerModel> Players { get; set; }
    }
}
