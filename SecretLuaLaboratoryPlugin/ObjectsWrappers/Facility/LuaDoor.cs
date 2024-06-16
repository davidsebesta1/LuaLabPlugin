using Interactables.Interobjects.DoorUtils;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core.Doors;

namespace LuaLab.ObjectsWrappers.Facility
{
    public class LuaDoor
    {
        [MoonSharpHidden]
        private FacilityDoor _facilityDoor;

        public LuaDoor(FacilityDoor facilityDoor)
        {
            _facilityDoor = facilityDoor;
        }

        [MoonSharpVisible(true)]
        public bool IsOpened
        {
            get
            {
                return _facilityDoor.IsOpened;
            }
            set
            {
                _facilityDoor.IsOpened = value;
            }
        }

        [MoonSharpVisible(true)]
        public bool IsLocked
        {
            get
            {
                return _facilityDoor.IsLocked;
            }
            set
            {
                _facilityDoor.Lock(LockReason, value);
            }
        }

        [MoonSharpVisible(true)]
        public DoorLockReason LockReason
        {
            get
            {
                return _facilityDoor.LockReason;
            }
            set
            {
                _facilityDoor.LockReason = value;
            }
        }

        [MoonSharpVisible(true)]
        public KeycardPermissions Permissions
        {
            get
            {
                return _facilityDoor.Permissions;
            }
            set
            {
                _facilityDoor.Permissions = value;
            }
        }
    }
}
