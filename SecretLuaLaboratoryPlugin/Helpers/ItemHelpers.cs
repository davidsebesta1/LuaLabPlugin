using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Ammo;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Pickups;
using PluginAPI.Core;
using UnityEngine;

namespace LuaLab.Helpers
{
    public static class ItemHelpers
    {
        public static readonly ItemType[] MedicalItems = [ItemType.Painkillers, ItemType.Medkit, ItemType.Adrenaline];

        public static readonly ItemType[] ExplosiveItems = [ItemType.GrenadeFlash, ItemType.GrenadeHE];

        public static readonly ItemType[] FirearmItems = [ItemType.GunCOM15, ItemType.GunCOM18, ItemType.GunFSP9, ItemType.GunCrossvec, ItemType.GunE11SR, ItemType.GunFRMG0, ItemType.GunAK, ItemType.GunLogicer, ItemType.GunShotgun, ItemType.GunRevolver, ItemType.GunA7];

        public static readonly ItemType[] SpecialFirearmItems = [ItemType.GunCom45, ItemType.GunA7, ItemType.ParticleDisruptor, ItemType.MicroHID];

        public static readonly ItemType[] AmmoItems = [ItemType.Ammo9x19, ItemType.Ammo556x45, ItemType.Ammo762x39, ItemType.Ammo44cal, ItemType.Ammo12gauge];

        public static ItemPickupBase SpawnItem(Vector3 coordinates, ItemType itemType)
        {
            ItemBase item = Server.Instance.ReferenceHub.inventory.ServerAddItem(itemType);

            ItemPickupBase itemPickup = item.ServerDropItem();
            itemPickup.transform.position = coordinates;

            return itemPickup;
        }

        public static FirearmPickup SpawnGun(Vector3 coordinates, ItemType itemType, byte ammo = 0, FirearmStatusFlags flags = FirearmStatusFlags.None, uint attachments = 0)
        {
            ItemBase item = Server.Instance.ReferenceHub.inventory.ServerAddItem(itemType);
            Firearm gun = item as Firearm;

            if (gun is ParticleDisruptor)
            {
                gun.Status = new FirearmStatus(ammo, FirearmStatusFlags.MagazineInserted, AttachmentsUtils.GetCurrentAttachmentsCode(gun));
            }
            else
            {
                gun.Status = new FirearmStatus(ammo, flags, attachments != 0 ? attachments : AttachmentsUtils.GetCurrentAttachmentsCode(gun));

            }

            ItemPickupBase itemPickup = item.ServerDropItem();
            itemPickup.transform.position = coordinates;

            return itemPickup as FirearmPickup;
        }

        public static AmmoPickup SpawnAmmo(Vector3 coordinates, ItemType itemType, ushort amount)
        {
            ItemBase item = Server.Instance.ReferenceHub.inventory.ServerAddItem(itemType);

            ItemPickupBase itemPickup = item.ServerDropItem();
            itemPickup.transform.position = coordinates;

            (itemPickup as AmmoPickup).NetworkSavedAmmo = amount;

            return itemPickup as AmmoPickup;
        }
    }
}
