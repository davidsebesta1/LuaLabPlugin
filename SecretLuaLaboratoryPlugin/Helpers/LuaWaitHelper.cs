using MEC;
using MoonSharp.Interpreter;

namespace LuaLab.Helpers
{
    public class LuaWaitHelper
    {
        public static void DoAfter(float seconds, DynValue function)
        {
            if (function.Type != DataType.Function)
            {
                return;
            }

            Timing.CallDelayed(seconds, () =>
            {
                function.Function.Call();
            });
        }
    }
}
