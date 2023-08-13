using System.ComponentModel;

namespace WorkshopServer
{
    public enum GameTypeEnum
    {
        [Description("Chess")]
        Chess = 1,

        [Description("Badminton")]
        Badminton = 2,

        [Description("Cricket")]
        Cricket = 3
    }
}
