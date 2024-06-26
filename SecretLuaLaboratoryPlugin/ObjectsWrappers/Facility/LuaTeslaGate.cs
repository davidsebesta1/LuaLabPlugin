using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;

namespace LuaLab.ObjectsWrappers.Facility
{
    public class LuaTeslaGate : IEquatable<LuaTeslaGate>
    {
        [MoonSharpHidden]
        private readonly TeslaGate _tesla;

        public LuaTeslaGate(TeslaGate tesla)
        {
            _tesla = tesla;
        }

        [MoonSharpVisible(true)]
        public void Burst()
        {
            _tesla.RpcPlayAnimation();
        }

        [MoonSharpVisible(true)]
        public void InstantBurts()
        {
            _tesla.RpcInstantBurst();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaTeslaGate);
        }

        public bool Equals(LuaTeslaGate other)
        {
            return other is not null &&
                   EqualityComparer<TeslaGate>.Default.Equals(_tesla, other._tesla);
        }

        public override int GetHashCode()
        {
            return 1929257653 + EqualityComparer<TeslaGate>.Default.GetHashCode(_tesla);
        }

        public static bool operator ==(LuaTeslaGate left, LuaTeslaGate right)
        {
            return EqualityComparer<LuaTeslaGate>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaTeslaGate left, LuaTeslaGate right)
        {
            return !(left == right);
        }
    }
}
