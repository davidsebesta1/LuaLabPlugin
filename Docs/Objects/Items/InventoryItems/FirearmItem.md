# Firearm Item

## About
A inherited class from [BaseItem](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Items/InventoryItems/BaseItem.md) to access properties of a firearm.

## Properties
`Ammo` - Whole number value of the current ammo inside of magazine.<br>
`MaxAmmo` - Whole number value of the max ammo that the current gun can hold.<br>

> [!NOTE]
> Maximum ammo is somehow connected to the max ammo of the magazine and different values for different magazine sizes will produce different final results.

`AmmoType` - Readonly value of Enum of type [`ItemType`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/ItemTypeId.md).<br>
`FirearmStatusFlags` - Flags enum of the type [`FirearmStatusFlags`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/FirearrmStatusFlags.md).<br>
`Attachments` - Whole number value of the attachments code.<br>

> [!IMPORTANT]
> For setting it I suggest just printing the value in game and copying it. I havent got a single clue how the fuck this system works.

## Functions
`TryReload()` - Attempts to reload a gun as a player and returns boolean value about success.<br>
`TryStopReload()` - Attempts to stop reload by the player and returns boolean wabout success.<br>
`TryUnload()` - Attempts to unload the gun as a player and return boolean about success.<br>

> [!NOTE]
> These functions are purely server side and doesnt force client side animations on the target weapon. Stopping functions will do cause clients to stop reloading if they started it.

Code Examples:

```lua
function onPlayerDryFire(args)
    if args.ItemType == ItemType.GunRevolver then
        local grenade = SpawnItem(args.Firearm.AimingPoint, ItemType.GrenadeHE)
        grenade.FuseTime = 3
    end
end

Events.PlayerDryfireWeapon:add(onPlayerDryFire)


--Any dry fired revolver will spawn a activated grenade with time of 3 seconds to explosion :trollface:
```
