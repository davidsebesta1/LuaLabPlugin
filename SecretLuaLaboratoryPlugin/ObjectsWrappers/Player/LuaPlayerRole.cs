using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PlayerRoles;
using System;
using System.Collections.Generic;

namespace SecretLuaLaboratoryPlugin.Objects.Player
{
    [MoonSharpUserData]
    public class LuaPlayerRole : IEquatable<LuaPlayerRole>
    {
        [MoonSharpHidden]
        private readonly LuaPlayer _luaPlayer;

        public LuaPlayerRole(LuaPlayer luaPlayer)
        {
            _luaPlayer = luaPlayer;
        }

        [MoonSharpVisible(true)]
        public RoleTypeId RoleType
        {
            get
            {
                return _luaPlayer.Hub.roleManager.CurrentRole.RoleTypeId;
            }
            set
            {
                _luaPlayer.Hub.roleManager.ServerSetRole(value, RoleChangeReason.RemoteAdmin, RoleSpawnFlags.All);
            }
        }

        [MoonSharpVisible(true)]
        public void SetRole(RoleTypeId role, RoleChangeReason reason = RoleChangeReason.RemoteAdmin, RoleSpawnFlags flags = RoleSpawnFlags.All)
        {
            _luaPlayer.Hub.roleManager.ServerSetRole(role, reason, flags);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaPlayerRole);
        }

        public bool Equals(LuaPlayerRole other)
        {
            return other is not null &&
                   EqualityComparer<LuaPlayer>.Default.Equals(_luaPlayer, other._luaPlayer);
        }

        public override int GetHashCode()
        {
            return 994639317 + EqualityComparer<LuaPlayer>.Default.GetHashCode(_luaPlayer);
        }

        public static bool operator ==(LuaPlayerRole left, LuaPlayerRole right)
        {
            return EqualityComparer<LuaPlayerRole>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaPlayerRole left, LuaPlayerRole right)
        {
            return !(left == right);
        }
    }
}
