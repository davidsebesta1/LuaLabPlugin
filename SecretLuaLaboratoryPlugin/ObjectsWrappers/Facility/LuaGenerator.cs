using MapGeneration.Distributors;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;
using static MapGeneration.Distributors.Scp079Generator;

namespace LuaLab.ObjectsWrappers.Facility
{
    public class LuaGenerator : IEquatable<LuaGenerator>
    {
        [MoonSharpHidden]
        private readonly Scp079Generator _generator;

        public LuaGenerator(Scp079Generator generator)
        {
            _generator = generator;
        }

        [MoonSharpVisible(true)]
        public bool ActivationReady
        {
            get
            {
                if (Activating)
                {
                    return _generator._leverStopwatch.Elapsed.TotalSeconds > _generator._leverDelay;
                }

                return false;
            }
        }

        [MoonSharpVisible(true)]
        public bool IsOpen
        {
            get
            {
                return _generator.HasFlag(_generator.Network_flags, GeneratorFlags.Open);
            }
            set
            {
                _generator.ServerSetFlag(GeneratorFlags.Open, value);
            }
        }

        [MoonSharpVisible(true)]
        public bool IsUnlocked
        {
            get
            {
                return _generator.HasFlag(_generator.Network_flags, GeneratorFlags.Unlocked);
            }
            set
            {
                _generator.ServerSetFlag(GeneratorFlags.Unlocked, value);
            }
        }

        [MoonSharpVisible(true)]
        public bool Engaged
        {
            get
            {
                return _generator.HasFlag(_generator.Network_flags, GeneratorFlags.Engaged);
            }
            set
            {
                _generator.ServerSetFlag(GeneratorFlags.Engaged, value);
            }
        }

        [MoonSharpVisible(true)]
        public bool Activating
        {
            get
            {
                return _generator.HasFlag(_generator.Network_flags, GeneratorFlags.Activating);
            }
            set
            {
                _generator.ServerSetFlag(GeneratorFlags.Activating, value);
            }
        }

        [MoonSharpVisible(true)]
        public int RemainingTime
        {
            get
            {
                return _generator.Network_syncTime;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaGenerator);
        }

        public bool Equals(LuaGenerator other)
        {
            return other is not null &&
                   EqualityComparer<Scp079Generator>.Default.Equals(_generator, other._generator);
        }

        public override int GetHashCode()
        {
            return 1390540171 + EqualityComparer<Scp079Generator>.Default.GetHashCode(_generator);
        }

        public static bool operator ==(LuaGenerator left, LuaGenerator right)
        {
            return EqualityComparer<LuaGenerator>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaGenerator left, LuaGenerator right)
        {
            return !(left == right);
        }
    }
}
