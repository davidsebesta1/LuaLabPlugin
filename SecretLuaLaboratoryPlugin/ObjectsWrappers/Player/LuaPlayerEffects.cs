using CustomPlayerEffects;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using SecretLuaLaboratoryPlugin.Objects.Player;
using System;
using System.Collections.Generic;

namespace SecretLuaLaboratoryPlugin.ObjectsWrappers.Player
{
    [MoonSharpUserData]
    public class LuaPlayerEffects : IEquatable<LuaPlayerEffects>
    {
        [MoonSharpHidden]
        private readonly LuaPlayer _luaPlayer;

        public LuaPlayerEffects(LuaPlayer luaPlayer)
        {
            _luaPlayer = luaPlayer;
        }

        [MoonSharpVisible(true)]
        public StatusEffectBase this[string name]
        {
            get
            {
                if (_luaPlayer.Hub.playerEffectsController.TryGetEffect(name, out StatusEffectBase playerEffect))
                {
                    return playerEffect;
                }

                return null;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaPlayerEffects);
        }

        public bool Equals(LuaPlayerEffects other)
        {
            return other is not null &&
                   EqualityComparer<LuaPlayer>.Default.Equals(_luaPlayer, other._luaPlayer);
        }

        public override int GetHashCode()
        {
            return 994639317 + EqualityComparer<LuaPlayer>.Default.GetHashCode(_luaPlayer);
        }

        public static bool operator ==(LuaPlayerEffects left, LuaPlayerEffects right)
        {
            return EqualityComparer<LuaPlayerEffects>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaPlayerEffects left, LuaPlayerEffects right)
        {
            return !(left == right);
        }
    }
}
