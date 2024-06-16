using LuaLab.EventArguments;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core.Attributes;
using PluginAPI.Events;
using System;

namespace LuaLab.ObjectsWrappers.Managers
{
    [MoonSharpUserData]
    public class LuaEventManager
    {
        [MoonSharpVisible(true)]
        public event EventHandler<PlayerJoinedArgs> PlayerJoined;

        [MoonSharpVisible(true)]
        public event EventHandler<PlayerLeftArgs> PlayerLeft;

        [PluginEvent]
        [MoonSharpHidden]
        public void OnPlayerJoined(PlayerJoinedEvent args)
        {
            PlayerJoined?.Invoke(null, new PlayerJoinedArgs(Plugin.Instance.LuaPlayerManager[args.Player.ReferenceHub]));
        }

        [PluginEvent]
        [MoonSharpHidden]
        public void OnPlayerLeft(PlayerLeftEvent args)
        {
            PlayerLeft?.Invoke(null, new PlayerLeftArgs(Plugin.Instance.LuaPlayerManager[args.Player.ReferenceHub]));
        }
    }
}