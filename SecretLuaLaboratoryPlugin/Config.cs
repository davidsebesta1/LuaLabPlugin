using System.ComponentModel;

namespace LuaLab
{
    public class Config
    {
        public bool IsEnabled { get; set; } = true;

        [Description("Allows any player with RA perms and ExecuteConsoleCommands permission to execute Lua in game using 'Lua' cmd")]
        public bool AllowExecLuaInGame { get; set; } = true;
    }
}
