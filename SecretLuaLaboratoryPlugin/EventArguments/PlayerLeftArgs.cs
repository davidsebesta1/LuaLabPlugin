using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using SecretLuaLaboratoryPlugin.Objects.Player;
using System;

namespace LuaLab.EventArguments
{
    [MoonSharpUserData]
    public class PlayerLeftArgs : EventArgs
    {
        [MoonSharpVisible(true)]
        public LuaPlayer Player { get; private set; }

        public PlayerLeftArgs(LuaPlayer player)
        {
            Player = player;
        }
    }
}
