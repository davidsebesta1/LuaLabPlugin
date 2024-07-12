using MoonSharp.Interpreter;
using SecretLuaLaboratoryPlugin.Objects.Player;

namespace LuaLab.ObjectsWrappers.Command
{
    [MoonSharpUserData]
    public class CommandArguments
    {
        public readonly LuaPlayer Sender;
        public readonly string[] Args;

        public string Response = string.Empty;

        public CommandArguments(LuaPlayer sender, string[] args)
        {
            Sender = sender;
            Args = args;
        }
    }
}
