using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using SecretLuaLaboratoryPlugin.Objects.Player;
using System;

namespace LuaLab.EventArguments
{
    [MoonSharpUserData]
    public class PlayerJoinedArgs : EventArgs
    {
        [MoonSharpVisible(true)]
        public LuaPlayer Player { get; private set; }

        public PlayerJoinedArgs(LuaPlayer player)
        {
            Player = player;
        }
    }
}
