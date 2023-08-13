using System.ComponentModel;

namespace WorkshopServer
{
    public enum GameTeamSizeEnum
    {
        [Description("Cricket")]
        Cricket = 11,

        [Description("Badminton")]
        Badminton = 2,

        [Description("Chess")]
        Chess = 1,

        [Description("Other")]
        Other = 0
    }
}
