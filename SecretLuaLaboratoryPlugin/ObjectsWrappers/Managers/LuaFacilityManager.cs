using MapGeneration;
using MapGeneration.Distributors;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PlayerRoles.PlayableScps.Scp079;
using PluginAPI.Core.Zones.Heavy;
using PluginAPI.Roles;
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
        public TeslaGate[] TeslaGates
        {
            get
            {
                return TeslaGateController.Singleton.TeslaGates.ToArray();
            }
        }

        [MoonSharpVisible(true)]
        public Scp079Generator[] Generators
        {
            get
            {
                return Scp079Recontainer.AllGenerators.ToArray();
            }
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
