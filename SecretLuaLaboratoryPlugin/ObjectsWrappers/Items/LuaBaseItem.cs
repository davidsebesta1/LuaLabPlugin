using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Events;
using SecretLuaLaboratoryPlugin.Objects.Player;
using System;
using System.Collections.Generic;

namespace LuaLab.ObjectsWrappers.Items
{
    public class LuaBaseItem : IEquatable<LuaBaseItem>
    {
        [MoonSharpHidden]
        protected ItemBase _itemBase;

        public LuaBaseItem(ItemBase itemBase)
        {
            _itemBase = itemBase;
        }

        [MoonSharpVisible(true)]
        public ItemType ItemType
        {
            get
            {
                return _itemBase.ItemTypeId;
            }
        }

        [MoonSharpVisible(true)]
        public ItemCategory ItemCategory
        {
            get
            {
                return _itemBase.Category;
            }
        }

        [MoonSharpVisible(true)]
        public ushort Serial
        {
            get
            {
                return _itemBase.ItemSerial;
            }
        }

        [MoonSharpVisible(true)]
        public float Weight
        {
            get
            {
                return _itemBase.Weight;
            }
        }

        [MoonSharpVisible(true)]
        public LuaPlayer Owner
        {
            get
            {
                return Plugin.Instance.LuaPlayerManager[_itemBase.Owner];
            }
        }

        [MoonSharpVisible(true)]
        public ItemPickupBase Drop()
        {
            return _itemBase.ServerDropItem();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaBaseItem);
        }

        public bool Equals(LuaBaseItem other)
        {
            return other is not null &&
                   EqualityComparer<ItemBase>.Default.Equals(_itemBase, other._itemBase);
        }

        public override int GetHashCode()
        {
            return -998414240 + EqualityComparer<ItemBase>.Default.GetHashCode(_itemBase);
        }

        public static bool operator ==(LuaBaseItem left, LuaBaseItem right)
        {
            return EqualityComparer<LuaBaseItem>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaBaseItem left, LuaBaseItem right)
        {
            return !(left == right);
        }
    }
}
