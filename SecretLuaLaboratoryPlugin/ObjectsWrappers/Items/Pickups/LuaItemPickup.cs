using InventorySystem.Items.Pickups;
using System;
using System.Collections.Generic;

namespace LuaLab.ObjectsWrappers.Items.Pickups
{
    public class LuaItemPickup : LuaBaseItemPickup, IEquatable<LuaItemPickup>
    {
        public LuaItemPickup(ItemPickupBase itemPickupBase) : base(itemPickupBase)
        {
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaItemPickup);
        }

        public bool Equals(LuaItemPickup other)
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
