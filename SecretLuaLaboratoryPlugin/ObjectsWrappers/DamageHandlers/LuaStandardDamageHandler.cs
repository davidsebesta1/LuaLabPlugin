using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PlayerStatsSystem;

namespace LuaLab.ObjectsWrappers.DamageHandlers
{
    public class LuaStandardDamageHandler
    {
        [MoonSharpHidden]
        private readonly StandardDamageHandler _originalDamageHandler;

        public LuaStandardDamageHandler(StandardDamageHandler handler)
        {
            _originalDamageHandler = handler;
        }

        [MoonSharpVisible(true)]
        public float Damage
        {
            get
            {
                return _originalDamageHandler.Damage;
            }
        }

    }
}