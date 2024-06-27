using Footprinting;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;
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
                        _activateGrenade = SpawnActive(((TimedGrenadePickup)_itemPickupBase), _itemPickupBase.Position, fuseTime: value, _itemPickupBase.PreviousOwner.Hub);
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
                    _activateGrenade = SpawnActive(((TimedGrenadePickup)_itemPickupBase), _itemPickupBase.Position, fuseTime: 0.1f, _itemPickupBase.PreviousOwner.Hub);
                    return;
                }
            }
        }

        [MoonSharpHidden]
        private static TimeGrenade SpawnActive(TimedGrenadePickup item, Vector3 position, float fuseTime = 3.5f, ReferenceHub owner = null)
        {
            TimeGrenade thrownProjectile = null;
            if (InventoryItemLoader.AvailableItems.TryGetValue(item.Info.ItemId, out var value) && value is ThrowableItem throwableItem)
            {
                thrownProjectile = (TimeGrenade)Object.Instantiate(throwableItem.Projectile);

                if (thrownProjectile.PhysicsModule is PickupStandardPhysics pickupStandardPhysics && item.PhysicsModule is PickupStandardPhysics pickupStandardPhysics2)
                {
                    Rigidbody rb = pickupStandardPhysics.Rb;
                    Rigidbody rb2 = pickupStandardPhysics2.Rb;
                    rb.position = rb2.position;
                    rb.rotation = rb2.rotation;
                    rb.velocity = rb2.velocity;
                    rb.angularVelocity = rb2.angularVelocity;
                }

                item.Info.Locked = true;
                thrownProjectile.NetworkInfo = item.Info;
                thrownProjectile.PreviousOwner = item._attacker;
                NetworkServer.Spawn(thrownProjectile.gameObject);
                thrownProjectile.ServerActivate();

                thrownProjectile.Network_syncTargetTime = NetworkTime.time + fuseTime;
                item.DestroySelf();
            }

            return thrownProjectile;
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
