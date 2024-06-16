using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Firearms.Ammo;
using InventorySystem.Items.Pickups;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretLuaLaboratoryPlugin.Objects.Player
{
    [MoonSharpUserData]
    public class LuaPlayerInventory : IEquatable<LuaPlayerInventory>
    {
        [MoonSharpHidden]
        private readonly LuaPlayer _luaPlayer;

        public LuaPlayerInventory(LuaPlayer luaPlayer)
        {
            _luaPlayer = luaPlayer;
        }

        [MoonSharpVisible(true)]
        public ItemBase CurrentItem
        {
            get
            {
                return _luaPlayer.Hub.inventory.CurInstance;
            }
            set
            {
                _luaPlayer.Hub.inventory.CurInstance = value;
            }
        }

        [MoonSharpVisible(true)]
        public ItemBase GiveItem(ItemType type)
        {
            return _luaPlayer.Hub.inventory.ServerAddItem(type);
        }

        [MoonSharpVisible(true)]
        public void RemoveItem(ItemType type)
        {
            ItemBase item = _luaPlayer.Hub.inventory.UserInventory.Items.FirstOrDefault(x => x.Value.ItemTypeId == type).Value;

            if (item != null)
            {
                _luaPlayer.Hub.inventory.ServerRemoveItem(item.ItemSerial, item.PickupDropModel);
            }
        }

        [MoonSharpVisible(true)]
        public ItemPickupBase DropItem(ItemType type)
        {
            ItemBase item = _luaPlayer.Hub.inventory.UserInventory.Items.FirstOrDefault(x => x.Value.ItemTypeId == type).Value;

            if (item != null)
            {
                return _luaPlayer.Hub.inventory.ServerDropItem(item.ItemSerial);
            }
            return null;
        }

        [MoonSharpVisible(true)]
        public void GiveAmmo(ItemType type, int amount)
        {
            _luaPlayer.Hub.inventory.ServerAddAmmo(type, amount);
        }

        [MoonSharpVisible(true)]
        public void SetAmmo(ItemType type, int amount)
        {
            _luaPlayer.Hub.inventory.ServerSetAmmo(type, amount);
        }

        [MoonSharpVisible(true)]
        public List<AmmoPickup> DropAmmo(ItemType type, ushort amount = ushort.MaxValue)
        {
            return _luaPlayer.Hub.inventory.ServerDropAmmo(type, amount);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaPlayerInventory);
        }

        public bool Equals(LuaPlayerInventory other)
        {
            return other is not null &&
                   EqualityComparer<LuaPlayer>.Default.Equals(_luaPlayer, other._luaPlayer);
        }

        public override int GetHashCode()
        {
            return 994639317 + EqualityComparer<LuaPlayer>.Default.GetHashCode(_luaPlayer);
        }

        public static bool operator ==(LuaPlayerInventory left, LuaPlayerInventory right)
        {
            return EqualityComparer<LuaPlayerInventory>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaPlayerInventory left, LuaPlayerInventory right)
        {
            return !(left == right);
        }
    }
}
