using MapGeneration;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;
using PluginAPI.Core.Doors;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LuaLab.ObjectsWrappers.Facility
{
    public class LuaFacilityRoom : IEquatable<LuaFacilityRoom>
    {
        [MoonSharpHidden]
        private readonly RoomIdentifier _roomIdentifier;

        [MoonSharpHidden]
        private readonly FacilityDoor[] _doors;

        public LuaFacilityRoom(RoomIdentifier roomIdentifier)
        {
            _roomIdentifier = roomIdentifier;

            _doors = _roomIdentifier.ApiRoom._doors.Values.ToArray();
        }

        [MoonSharpVisible(true)]
        public FacilityDoor[] Doors
        {
            get
            {
                return _doors.ToArray();
            }
        }

        [MoonSharpVisible(true)]
        public RoomShape Shape
        {
            get
            {
                return _roomIdentifier.Shape;
            }
        }

        [MoonSharpVisible(true)]
        public RoomName Name
        {
            get
            {
                return _roomIdentifier.Name;
            }
        }


        [MoonSharpVisible(true)]
        public PluginAPI.Core.Zones.FacilityZone Zone
        {
            get
            {
                return _roomIdentifier.ApiRoom.Zone;
            }
        }

        [MoonSharpVisible(true)]
        public bool LightsEnabled
        {
            get
            {
                return RoomLightController.Instances.First(n => n.Room == _roomIdentifier).NetworkLightsEnabled;
            }
            set
            {
                RoomLightController.Instances.First(n => n.Room == _roomIdentifier).NetworkLightsEnabled = value;
            }
        }

        [MoonSharpVisible(true)]
        public Color LightColor
        {
            get
            {
                return RoomLightController.Instances.First(n => n.Room == _roomIdentifier).NetworkOverrideColor;
            }
            set
            {
                RoomLightController.Instances.First(n => n.Room == _roomIdentifier).NetworkOverrideColor = value;
            }
        }

        [MoonSharpVisible(true)]
        public void FlickerLights(float duration)
        {
            RoomLightController.Instances.First(n => n.Room == _roomIdentifier).ServerFlickerLights(duration);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaFacilityRoom);
        }

        public bool Equals(LuaFacilityRoom other)
        {
            return other is not null &&
                   EqualityComparer<RoomIdentifier>.Default.Equals(_roomIdentifier, other._roomIdentifier);
        }

        public override int GetHashCode()
        {
            return 860246382 + EqualityComparer<RoomIdentifier>.Default.GetHashCode(_roomIdentifier);
        }

        public static bool operator ==(LuaFacilityRoom left, LuaFacilityRoom right)
        {
            return EqualityComparer<LuaFacilityRoom>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaFacilityRoom left, LuaFacilityRoom right)
        {
            return !(left == right);
        }
    }
}
