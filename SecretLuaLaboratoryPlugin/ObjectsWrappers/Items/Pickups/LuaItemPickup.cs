using InventorySystem.Items.Pickups;
using Mirror;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaLab.ObjectsWrappers.Items.Pickups
{
    public class LuaItemPickup : IEquatable<LuaItemPickup>
    {
        [MoonSharpHidden]
        protected ItemPickupBase _itemPickupBase;

        public LuaItemPickup(ItemPickupBase itemPickupBase)
        {
            _itemPickupBase = itemPickupBase;
        }

        [MoonSharpVisible(true)]
        public Vector3 Position
        {
            get
            {
                return _itemPickupBase.Position;
            }
            set
            {
                _itemPickupBase.Position = value;
            }
        }

        [MoonSharpVisible(true)]
        public Vector3 Rotation
        {
            get
            {
                return _itemPickupBase.Rotation.eulerAngles;
            }
            set
            {
                _itemPickupBase.Rotation = Quaternion.Euler(value);
            }
        }

        [MoonSharpVisible(true)]
        public Vector3 Scale
        {
            get
            {
                return _itemPickupBase.gameObject.transform.localScale;
            }
            set
            {
                NetworkServer.UnSpawn(_itemPickupBase.gameObject);
                _itemPickupBase.gameObject.transform.localScale = value;
                NetworkServer.Spawn(_itemPickupBase.gameObject);
            }
        }

        [MoonSharpVisible(true)]
        public ItemType ItemType
        {
            get
            {
                return _itemPickupBase.Info.ItemId;
            }
        }

        [MoonSharpVisible(true)]
        public ushort ItemSerial
        {
            get
            {
                return _itemPickupBase.Info.Serial;
            }
        }

        [MoonSharpVisible(true)]
        public bool Locked
        {
            get
            {
                return _itemPickupBase.Info.Locked;
            }
            set
            {
                _itemPickupBase.Info.Locked = value;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaItemPickup);
        }

        public bool Equals(LuaItemPickup other)
        {
            return other is not null &&
                   EqualityComparer<ItemPickupBase>.Default.Equals(_itemPickupBase, other._itemPickupBase);
        }

        public override int GetHashCode()
        {
            return 200970496 + EqualityComparer<ItemPickupBase>.Default.GetHashCode(_itemPickupBase);
        }

        public static bool operator ==(LuaItemPickup left, LuaItemPickup right)
        {
            return EqualityComparer<LuaItemPickup>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaItemPickup left, LuaItemPickup right)
        {
            return !(left == right);
        }
    }
}
