﻿using CommandSystem;
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
            PlayerCommandSender cmdSender = sender as PlayerCommandSender;

            ReferenceHub hub = cmdSender?.ReferenceHub ?? ReferenceHub.HostHub;
            if (Plugin.Instance.Config.AllowExecLuaInGame && (hub == ReferenceHub.HostHub || (Plugin.Instance.Config.AllowedUserIds?.Contains(hub.authManager.UserId) ?? false)))
            {
                string code = string.Join(" ", arguments);
                Plugin.Instance.LuaScriptManager.ExecuteLuaInGame(hub, code, hub == ReferenceHub.HostHub ? LuaOutputType.ServerConsole : LuaOutputType.PlayerConsole);

                response = "Exec script";
                return true;
            }

            response = "You do not have enough permission to run this command";
            return false;
        }
    }
}
