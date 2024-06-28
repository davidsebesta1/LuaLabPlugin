using HarmonyLib;
using MapGeneration;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using FacilityZone = PluginAPI.Core.Zones.FacilityZone;

namespace LuaLab.ObjectsWrappers.Facility
{
    public class LuaFacilityZone : IEquatable<LuaFacilityZone>
    {
        [MoonSharpHidden]
        private readonly FacilityZone _zone;

        [MoonSharpHidden]
        private readonly RoomIdentifier[] _rooms;

        public LuaFacilityZone(FacilityZone zone)
        {
            _zone = zone;
            _rooms = RoomIdentifier.AllRoomIdentifiers.Where(n => n.Zone == _zone.ZoneType).ToArray();
        }

        [MoonSharpVisible(true)]
        public MapGeneration.FacilityZone ZoneType
        {
            get
            {
                return _zone.ZoneType;
            }
        }

        [MoonSharpVisible(true)]
        public RoomIdentifier[] Rooms
        {
            get
            {
                return _rooms.ToArray();
            }
        }

        [MoonSharpVisible(true)]
        public void FlickerLights(float duration)
        {
            RoomLightController.Instances.Where(n => _rooms.Contains(n.Room)).Do(n => n.ServerFlickerLights(duration));
        }

        [MoonSharpVisible(true)]
        public RoomIdentifier this[RoomName roomName]
        {
            get
            {
                return _rooms.FirstOrDefault(n => n.Name == roomName);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaFacilityZone);
        }

        public bool Equals(LuaFacilityZone other)
        {
            return other is not null &&
                   EqualityComparer<FacilityZone>.Default.Equals(_zone, other._zone);
        }

        public override int GetHashCode()
        {
            return 1380831392 + EqualityComparer<FacilityZone>.Default.GetHashCode(_zone);
        }

        public static bool operator ==(LuaFacilityZone left, LuaFacilityZone right)
        {
            return EqualityComparer<LuaFacilityZone>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaFacilityZone left, LuaFacilityZone right)
        {
            return !(left == right);
        }
    }
}