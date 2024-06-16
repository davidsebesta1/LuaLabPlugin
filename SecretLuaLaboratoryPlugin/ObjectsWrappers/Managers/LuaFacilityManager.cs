using MapGeneration;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System.Linq;

namespace SecretLuaLaboratoryPlugin.ObjectsWrappers.Managers
{
    [MoonSharpUserData]
    public class LuaFacilityManager
    {
        public LuaFacilityManager()
        {

        }

        [MoonSharpVisible(true)]
        public void TurnOffAllLights()
        {
            PluginAPI.Core.Facility.TurnOffAllLights();
        }

        [MoonSharpVisible(true)]
        public void TurnOnAllLights()
        {
            PluginAPI.Core.Facility.TurnOnAllLights();
        }

        [MoonSharpVisible(true)]
        public PluginAPI.Core.Zones.FacilityZone this[FacilityZone zone]
        {
            get
            {
                return PluginAPI.Core.Facility.Zones[(int)zone];
            }
        }

        [MoonSharpVisible(true)]
        public RoomIdentifier this[RoomName roomName]
        {
            get
            {
                return PluginAPI.Core.Facility.Rooms.FirstOrDefault(n => n.Identifier.Name == roomName).Identifier;
            }
        }
    }
}
