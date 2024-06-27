using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core.Doors;

namespace LuaLab.ObjectsWrappers.Facility
{
    public class LuaDoor
    {
        [MoonSharpHidden]
        private readonly FacilityDoor _facilityDoor;

        [MoonSharpHidden]
        private readonly DoorType _doorType;

        public LuaDoor(FacilityDoor facilityDoor)
        {
            _facilityDoor = facilityDoor;

            switch (_facilityDoor.OriginalObject)
            {
                case PryableDoor pryableDoor:
                    _doorType = DoorType.Gate;
                    break;
                case BreakableDoor door:
                    _doorType = DoorType.Standard;
                    break;
                case CheckpointDoor checkpointDoor:
                    _doorType = DoorType.Checkpoint;
                    break;
                default:
                    _doorType = DoorType.Standard;
                    break;
            }
        }

        [MoonSharpVisible(true)]
        public DoorType DoorType
        {
            get
            {
                return _doorType;
            }
        }

        [MoonSharpVisible(true)]
        public bool IsOpened
        {
            get
            {
                return _facilityDoor.OriginalObject.NetworkTargetState;
            }
            set
            {
                _facilityDoor.OriginalObject.NetworkTargetState = value;
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

        [MoonSharpVisible(true)]
        public void Explode()
        {
            if (_facilityDoor.GetType().IsAssignableFrom(typeof(BreakableDoor)))
            {
                ((BreakableDoor)_facilityDoor.OriginalObject).ServerDamage(float.MaxValue, DoorDamageType.ServerCommand);
            }
        }
    }
}