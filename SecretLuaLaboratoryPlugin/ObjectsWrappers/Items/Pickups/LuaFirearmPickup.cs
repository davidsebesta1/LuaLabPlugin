using InventorySystem.Items.Firearms;
using InventorySystem.Items.Pickups;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;

namespace LuaLab.ObjectsWrappers.Items.Pickups
{
    public class LuaFirearmPickup : LuaBaseItemPickup, IEquatable<LuaFirearmPickup>
    {
        public LuaFirearmPickup(ItemPickupBase itemPickupBase) : base(itemPickupBase)
        {

        }

        [MoonSharpVisible(true)]
        public byte Ammo
        {
            get
            {
                return ((FirearmPickup)_itemPickupBase).Status.Ammo;
            }
            set
            {
                FirearmPickup firearm = ((FirearmPickup)_itemPickupBase);
                firearm.Status = new FirearmStatus(value, firearm.Status.Flags, firearm.Status.Attachments);
            }
        }

        [MoonSharpVisible(true)]
        public FirearmStatusFlags StatusFlags
        {
            get
            {
                return ((FirearmPickup)_itemPickupBase).Status.Flags;
            }
            set
            {
                FirearmPickup firearm = ((FirearmPickup)_itemPickupBase);
                firearm.Status = new FirearmStatus(firearm.Status.Ammo, value, firearm.Status.Attachments);
            }
        }

        [MoonSharpVisible(true)]
        public uint Attachments
        {
            get
            {
                return ((FirearmPickup)_itemPickupBase).Status.Attachments;
            }
            set
            {
                FirearmPickup firearm = ((FirearmPickup)_itemPickupBase);
                firearm.Status = new FirearmStatus(firearm.Status.Ammo, firearm.Status.Flags, value);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaFirearmPickup);
        }

        public bool Equals(LuaFirearmPickup other)
        {
            return other is not null &&
                   base.Equals(other) &&
                   EqualityComparer<ItemPickupBase>.Default.Equals(_itemPickupBase, other._itemPickupBase);
        }

        public override int GetHashCode()
        {
            int hashCode = 2008087446;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ItemPickupBase>.Default.GetHashCode(_itemPickupBase);
            return hashCode;
        }

        public static bool operator ==(LuaFirearmPickup left, LuaFirearmPickup right)
        {
            return EqualityComparer<LuaFirearmPickup>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaFirearmPickup left, LuaFirearmPickup right)
        {
            return !(left == right);
        }
    }
}
