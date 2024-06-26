﻿using Footprinting;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;
using PluginAPI.Core.Items;
using SecretLuaLaboratoryPlugin.Objects.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace LuaLab.ObjectsWrappers.Items.Pickups
{
    public class LuaExplosivePickup : LuaBaseItemPickup, IEquatable<LuaExplosivePickup>
    {
        [MoonSharpHidden]
        private TimeGrenade _activateGrenade;

        public LuaExplosivePickup(TimedGrenadePickup itemPickupBase) : base(itemPickupBase)
        {

        }

        [MoonSharpVisible(true)]
        public LuaPlayer Owner
        {
            get
            {
                return Plugin.Instance.LuaPlayerManager[((TimedGrenadePickup)_itemPickupBase).PreviousOwner.Hub];
            }
        }

        [MoonSharpVisible(true)]
        public float FuseTime
        {
            get
            {
                if (_activateGrenade == null)
                {
                    return -1;
                }

                return (float)(_activateGrenade.Network_syncTargetTime - NetworkTime.time);
            }
            set
            {
                try
                {
                    if (_activateGrenade == null)
                    {
                        ThrowableItem projectile = CreateThrowable(_itemPickupBase.Info.ItemId, _itemPickupBase.PreviousOwner.Hub);
                        _activateGrenade = SpawnActive(projectile, _itemPickupBase.Position, fuseTime: value, _itemPickupBase.PreviousOwner.Hub);
                        _itemPickupBase.DestroySelf();

                        return;
                    }

                    if (FuseTime <= 0d)
                    {
                        _activateGrenade.ServerFuseEnd();
                        return;
                    }

                    _activateGrenade.Network_syncTargetTime = NetworkTime.time + value;

                }
                catch (Exception ex)
                {
                    Log.Debug(ex.ToString());
                }
            }
        }

        [MoonSharpVisible(true)]
        public bool Detonated
        {
            get
            {
                if (_activateGrenade == null)
                {
                    return false;
                }

                return _activateGrenade._alreadyDetonated;
            }
            set
            {
                if (value && _activateGrenade == null)
                {
                    ThrowableItem projectile = CreateThrowable(_itemPickupBase.Info.ItemId, _itemPickupBase.PreviousOwner.Hub);
                    _activateGrenade = SpawnActive(projectile, _itemPickupBase.Position, fuseTime: 0.1f, _itemPickupBase.PreviousOwner.Hub);
                    _itemPickupBase.DestroySelf();
                }
            }
        }

        [MoonSharpHidden]
        private static ThrowableItem CreateThrowable(ItemType type, ReferenceHub player = null)
        {
            return ((player != null) ? player : ReferenceHub.HostHub).inventory.CreateItemInstance(new ItemIdentifier(type, ItemSerialGenerator.GenerateNext()), updateViewmodel: false) as ThrowableItem;
        }

        [MoonSharpHidden]
        private static TimeGrenade SpawnActive(ThrowableItem item, Vector3 position, float fuseTime = -1f, ReferenceHub owner = null)
        {
            TimeGrenade timeGrenade = (TimeGrenade)Object.Instantiate(item.Projectile, position, Quaternion.identity);

            timeGrenade.NetworkInfo = new PickupSyncInfo(item.ItemTypeId, item.Weight, item.ItemSerial);
            timeGrenade.PreviousOwner = new Footprint(owner ?? ReferenceHub.HostHub);
            NetworkServer.Spawn(timeGrenade.gameObject);
            timeGrenade.ServerActivate();

            timeGrenade.Network_syncTargetTime = NetworkTime.time + fuseTime;
            return timeGrenade;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaExplosivePickup);
        }

        public bool Equals(LuaExplosivePickup other)
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

        public static bool operator ==(LuaExplosivePickup left, LuaExplosivePickup right)
        {
            return EqualityComparer<LuaExplosivePickup>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaExplosivePickup left, LuaExplosivePickup right)
        {
            return !(left == right);
        }
    }
}
