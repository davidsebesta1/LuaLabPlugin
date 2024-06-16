using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;

namespace LuaLab.ObjectsWrappers.Managers
{
    [MoonSharpUserData]
    public class LuaServerManager
    {
        public LuaServerManager()
        {

        }

        [MoonSharpVisible(true)]
        public string Address
        {
            get
            {
                return Server.ServerIpAddress;
            }
        }

        [MoonSharpVisible(true)]
        public ushort Port
        {
            get
            {
                return Server.Port;
            }
        }

        [MoonSharpVisible(true)]
        public int PlayerCount
        {
            get
            {
                return Server.PlayerCount;
            }
        }

        [MoonSharpVisible(true)]
        public int MaxPlayerCount
        {
            get
            {
                return Server.MaxPlayers;
            }
        }

        [MoonSharpVisible(true)]
        public int ReservedSlots
        {
            get
            {
                return Server.ReservedSlots;
            }
        }

        [MoonSharpVisible(true)]
        public int TPS
        {
            get
            {
                return (int)Server.TPS;
            }
        }

        [MoonSharpVisible(true)]
        public bool FriendlyFire
        {
            get
            {
                return Server.FriendlyFire;
            }
            set
            {
                if (FriendlyFire != value)
                {
                    Server.FriendlyFire = value;
                }
            }
        }
    }
}
