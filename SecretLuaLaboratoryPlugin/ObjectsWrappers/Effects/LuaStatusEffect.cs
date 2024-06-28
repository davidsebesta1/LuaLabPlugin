using CustomPlayerEffects;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;
using static CustomPlayerEffects.StatusEffectBase;

namespace LuaLab.ObjectsWrappers.Effects
{
    public class LuaStatusEffect : IEquatable<LuaStatusEffect>
    {
        [MoonSharpHidden]
        private readonly StatusEffectBase _statusEffectBase;

        public LuaStatusEffect(StatusEffectBase statusEffectBase)
        {
            _statusEffectBase = statusEffectBase;
        }

        [MoonSharpVisible(true)]
        public string EffectName
        {
            get
            {
                return _statusEffectBase.ToString();
            }
        }

        [MoonSharpVisible(true)]
        public EffectClassification Classification
        {
            get
            {
                return _statusEffectBase.Classification;
            }
        }

        [MoonSharpVisible(true)]
        public bool IsEnabled
        {
            get
            {
                return _statusEffectBase.IsEnabled;
            }
            set
            {
                _statusEffectBase.ServerSetState((byte)(value ? 1 : 0));
            }
        }

        [MoonSharpVisible(true)]
        public byte Intensity
        {
            get
            {
                return _statusEffectBase.Intensity;
            }
            set
            {
                _statusEffectBase.ServerSetState(value, _statusEffectBase.Duration);
            }
        }

        [MoonSharpVisible(true)]
        public float Duration
        {
            get
            {
                return _statusEffectBase.Duration;
            }
            set
            {
                _statusEffectBase.ServerSetState(_statusEffectBase.Intensity, value);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaStatusEffect);
        }

        public bool Equals(LuaStatusEffect other)
        {
            return other is not null &&
                   EqualityComparer<StatusEffectBase>.Default.Equals(_statusEffectBase, other._statusEffectBase);
        }

        public override int GetHashCode()
        {
            return -1412107284 + EqualityComparer<StatusEffectBase>.Default.GetHashCode(_statusEffectBase);
        }

        public static bool operator ==(LuaStatusEffect left, LuaStatusEffect right)
        {
            return EqualityComparer<LuaStatusEffect>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaStatusEffect left, LuaStatusEffect right)
        {
            return !(left == right);
        }
    }
}
