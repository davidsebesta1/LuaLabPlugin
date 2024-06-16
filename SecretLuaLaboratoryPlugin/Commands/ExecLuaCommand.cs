using CommandSystem;
using RemoteAdmin;
using System;

namespace LuaLab.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ExecLuaCommand : ICommand
    {
        public bool SanitizeResponse => true;

        public string Command => "lua";

        public string[] Aliases => Array.Empty<string>();

        public string Description => "Executes given lua";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender.CheckPermission(PlayerPermissions.ServerConsoleCommands))
            {
                string code = string.Join(" ", arguments);

                ReferenceHub hub = ((PlayerCommandSender)sender).ReferenceHub;

                Plugin.Instance.LuaScriptManager.ExecuteLuaInGame(hub, code, LuaOutputType.PlayerConsole);

                response = "Exec script";
                return true;
            }

            response = "You do not have enough permission to run this command";
            return false;
        }
    }
}
