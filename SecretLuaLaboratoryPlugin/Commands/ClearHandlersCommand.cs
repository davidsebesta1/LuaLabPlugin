using CommandSystem;
using RemoteAdmin;
using System;
using System.Linq;

namespace LuaLab.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ClearHandlersCommand : ICommand
    {
        public bool SanitizeResponse => true;

        public string Command => "clrLuaEvents";

        public string[] Aliases => ["clearLuaEvents"];

        public string Description => "Unregisters all events from specified player´s environment";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            PlayerCommandSender cmdSender = sender as PlayerCommandSender;

            ReferenceHub hub = cmdSender?.ReferenceHub ?? ReferenceHub.HostHub;
            if (Plugin.Instance.Config.AllowExecLuaInGame && (hub == ReferenceHub.HostHub || (Plugin.Instance.Config.AllowedUserIds?.Contains(hub.authManager.UserId) ?? false)))
            {
                ReferenceHub target = arguments.Count > 0 ? ReferenceHub.AllHubs.FirstOrDefault(n => n.authManager.UserId == arguments.ElementAt(0)) : hub;

                if (target == null)
                {
                    response = "Unable to find specified player";
                    return false;
                }

                response = "Cleared all registered events";
                Plugin.Instance.LuaScriptManager.ClearEventHandlersForPlayer(target);
                return true;
            }

            response = "You do not have enough permission to run this command";
            return false;
        }
    }
}