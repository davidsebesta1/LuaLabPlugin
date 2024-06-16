using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;
using SecretLuaLaboratoryPlugin.Objects.Player;
using System.Collections.Generic;
using System.Linq;

namespace LuaLab.ObjectsWrappers.Managers
{
    [MoonSharpUserData]
    public class LuaPlayerManager
    {
        [MoonSharpHidden]
        private readonly Dictionary<ReferenceHub, LuaPlayer> _luaPlayersByHub = new Dictionary<ReferenceHub, LuaPlayer>(Server.MaxPlayers);

        [MoonSharpVisible(true)]
        public LuaPlayer[] Players => _luaPlayersByHub.Values.ToArray();

        public LuaPlayerManager()
        {
        }

        [MoonSharpVisible(true)]
        public static void Mute(LuaPlayer player)
        {
            player.Muted = true;
        }

        [MoonSharpVisible(true)]
        public static void Unmute(LuaPlayer player)
        {
            player.Muted = false;
        }

        [MoonSharpVisible(true)]
        public static void Kick(LuaPlayer player, string reason)
        {
            player.Kick(reason);
        }

        [MoonSharpVisible(true)]
        public static void Ban(LuaPlayer player, string reason, int durationSeconds)
        {
            player.Ban(reason, durationSeconds);
        }

        [MoonSharpHidden]
        [PluginEvent]
        private void OnPlayerJoined(PlayerJoinedEvent args)
        {
            _luaPlayersByHub.Add(args.Player.ReferenceHub, new LuaPlayer(args.Player.ReferenceHub));
        }

        [MoonSharpHidden]
        [PluginEvent]
        private void OnPlayerLeft(PlayerLeftEvent args)
        {
            _luaPlayersByHub.Remove(args.Player.ReferenceHub);
        }

        public LuaPlayer this[int id]
        {
            get
            {
                return _luaPlayersByHub.FirstOrDefault(n => n.Key.PlayerId == id).Value;
            }
        }

        public LuaPlayer this[string name]
        {
            get
            {
                return _luaPlayersByHub.FirstOrDefault(n => n.Key.nicknameSync.DisplayName == name).Value;
            }
        }

        [MoonSharpHidden]
        public LuaPlayer this[ReferenceHub hub]
        {
            get
            {
                if (_luaPlayersByHub.TryGetValue(hub, out LuaPlayer plr))
                {
                    return plr;
                }

                return null;
            }
        }
    }
}
