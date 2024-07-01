using CommandSystem;
using PluginAPI.Events;
using System;
using System.Linq;

namespace LuaLab.Commands
{
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class ReloadLuaPluginCommand : ICommand
    {
        public bool SanitizeResponse => true;

        public string Command => "reloadLua";

        public string[] Aliases => ["luaReload"];

        public string Description => "Reloads specified lua plugin";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 1)
            {
                PlayerChangeRoleEvent
                bool res = Plugin.Instance.LuaPluginManager.ReloadLuaPlugin(arguments.ElementAt(0));
                response = res ? "Reloaded" : "Couldnt reload plugin, error has occured";
                return res;
            }

            response = "Provide plugin name";
            return false;
        }
    }
}
