using System.ComponentModel;

namespace LuaLab
{
    public class Config
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Allows any player with RA perms to execute Lua in game using 'Lua' cmd")]
        public bool AllowExecLuaInGame { get; set; } = true;

        [Description("User Ids in format id@steam for players whose are allowed to execute Lua in game via Remote Admin Console")]
        public string[] AllowedUserIds { get; set; } = [];
    }
}