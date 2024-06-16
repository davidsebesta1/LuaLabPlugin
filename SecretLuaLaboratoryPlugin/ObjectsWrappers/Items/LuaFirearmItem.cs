using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;

namespace LuaLab.ObjectsWrappers.Items
{
    public class LuaFirearmItem : LuaBaseItem, IEquatable<LuaFirearmItem>
    {
        public LuaFirearmItem(ItemBase itemBase) : base(itemBase)
        {

        }

        [MoonSharpVisible(true)]
        public byte Ammo
        {
            get
            {
                return ((Firearm)_itemBase).Status.Ammo;
            }
            set
            {
                Firearm firearm = ((Firearm)_itemBase);
                firearm.Status = new FirearmStatus(value, firearm.Status.Flags, firearm.Status.Attachments);
            }
        }

        [MoonSharpVisible(true)]
        public ItemType AmmoType
        {
            get
            {
                return ((Firearm)_itemBase).AmmoType;
            }
        }

        [MoonSharpVisible(true)]
        public byte MaxAmmo
        {
            get
            {
                switch (_itemBase)
                {
                    case AutomaticFirearm autoFirearm:
                        return autoFirearm._baseMaxAmmo;
                    case Revolver revolver:
                        return ((ClipLoadedInternalMagAmmoManager)revolver.AmmoManagerModule).MaxAmmo;
                    case Shotgun shotgun:
                        return ((TubularMagazineAmmoManager)shotgun.AmmoManagerModule).MaxAmmo;
                    default:
                        return ((Firearm)_itemBase).AmmoManagerModule.MaxAmmo;
                }
            }
            set
            {
                switch (_itemBase)
                {
                    case AutomaticFirearm autoFirearm:
                        autoFirearm._baseMaxAmmo = value;
                        ((AutomaticAmmoManager)autoFirearm.AmmoManagerModule).MaxAmmo = value;
                        return;
                    case Revolver revolver:
                        ((ClipLoadedInternalMagAmmoManager)revolver.AmmoManagerModule).MaxAmmo = value;
                        return;
                    case Shotgun shotgun:
                        ((TubularMagazineAmmoManager)shotgun.AmmoManagerModule).MaxAmmo = value;
                        return;
                    default:
                        throw new NotImplementedException("Unable to set max ammo for this firearm type");
                }
            }
        }

        [MoonSharpVisible(true)]
        public FirearmStatusFlags FirearmStatusFlags
        {
            get
            {
                return ((Firearm)_itemBase).Status.Flags;
            }
            set
            {
                Firearm firearm = ((Firearm)_itemBase);
                firearm.Status = new FirearmStatus(firearm.Status.Ammo, value, firearm.Status.Attachments);
            }
        }

        [MoonSharpVisible(true)]
        public uint Attachments
        {
            get
            {
                return ((Firearm)_itemBase).Status.Attachments;
            }
            set
            {
                Firearm firearm = ((Firearm)_itemBase);
                firearm.Status = new FirearmStatus(firearm.Status.Ammo, firearm.Status.Flags, value);
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaFirearmItem);
        }

        public bool Equals(LuaFirearmItem other)
        {
            return other is not null &&
                   EqualityComparer<ItemBase>.Default.Equals(_itemBase, other._itemBase);
        }

        public override int GetHashCode()
        {
            return -998414240 + EqualityComparer<ItemBase>.Default.GetHashCode(_itemBase);
        }

        public static bool operator ==(LuaFirearmItem left, LuaFirearmItem right)
        {
            return EqualityComparer<LuaFirearmItem>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaFirearmItem left, LuaFirearmItem right)
        {
            return !(left == right);
        }
    }
}
