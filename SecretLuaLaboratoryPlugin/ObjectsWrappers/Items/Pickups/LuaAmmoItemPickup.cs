using InventorySystem.Items.Firearms.Ammo;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;

namespace LuaLab.ObjectsWrappers.Items.Pickups
{
    public class LuaAmmoItemPickup : LuaItemPickup, IEquatable<LuaAmmoItemPickup>
    {
        public LuaAmmoItemPickup(ItemPickupBase itemPickupBase) : base(itemPickupBase)
        {
           
        }

        [MoonSharpVisible(true)]
        public ushort Ammo
        {
            get
            {
                return ((AmmoPickup)_itemPickupBase).NetworkSavedAmmo;
            }
            set
            {
                ((AmmoPickup)_itemPickupBase).NetworkSavedAmmo = value;
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaAmmoItemPickup);
        }

        public bool Equals(LuaAmmoItemPickup other)
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

        public static bool operator ==(LuaAmmoItemPickup left, LuaAmmoItemPickup right)
        {
            return EqualityComparer<LuaAmmoItemPickup>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaAmmoItemPickup left, LuaAmmoItemPickup right)
        {
            return !(left == right);
        }
    }
}
