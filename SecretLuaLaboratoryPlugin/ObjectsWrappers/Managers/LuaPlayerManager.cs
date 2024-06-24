using CommandSystem.Commands.RemoteAdmin;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;
using SecretLuaLaboratoryPlugin.Objects.Player;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace LuaLab.ObjectsWrappers.Managers
{
    [MoonSharpUserData]
    public class LuaPlayerManager
    {
        [MoonSharpHidden]
        private readonly Dictionary<ReferenceHub, LuaPlayer> _luaPlayersByHub = new Dictionary<ReferenceHub, LuaPlayer>(Server.MaxPlayers);

        [MoonSharpVisible(true)]
        public LuaPlayer[] AllPlayers => _luaPlayersByHub.Values.ToArray();

        public LuaPlayerManager()
        {
        }

        [MoonSharpVisible(true)]
        public void Mute(LuaPlayer player)
        {
            player.Muted = true;
        }

        [MoonSharpVisible(true)]
        public void Unmute(LuaPlayer player)
        {
            player.Muted = false;
        }

        [MoonSharpVisible(true)]
        public void Kick(LuaPlayer player, string reason)
        {
            player.Kick(reason);
        }

        [MoonSharpVisible(true)]
        public void Kick(string sid, string reason)
        {
            ReferenceHub hub = ReferenceHub.AllHubs.FirstOrDefault(n => n.authManager.UserId == sid);
            if (hub != null)
            {
                Kick(Plugin.Instance.LuaPlayerManager[hub], reason);
            }
        }

        [MoonSharpVisible(true)]
        public void Ban(LuaPlayer player, string reason, int durationSeconds)
        {
            player.Ban(reason, durationSeconds);
        }

        [MoonSharpVisible(true)]
        public void Ban(string sid, string reason, int durationSeconds)
        {
            ReferenceHub hub = ReferenceHub.AllHubs.FirstOrDefault(n => n.authManager.UserId == sid);
            if (hub != null)
            {
                Ban(Plugin.Instance.LuaPlayerManager[hub], reason, durationSeconds);
            }
        }

        [MoonSharpVisible(true)]
        public void Unban(string sid)
        {
            BanHandler.RemoveBan(sid, BanHandler.BanType.UserId, true);
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
                return _luaPlayersByHub.FirstOrDefault(n => n.Key.nicknameSync.MyNick == name).Value;
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
