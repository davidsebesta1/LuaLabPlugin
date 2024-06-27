# Firearm Pickup

## About
Inherited class from [ItemPickup](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Items/Pickups/ItemPickup.md). Allows you to modify firearm properties.

## Properties
`Ammo` - Whole number value of current ammo inside of magazine (255 max).<br>
`StatusFlags` - Flags enum of type [`FirearmStatusFlags`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/FirearmStatusFlags.md).<br>
`Attachments` - Whole number value of current attachments.<br>

> [!IMPORTANT]
> For setting it I suggest just printing the value in game and copying it. I dont know how exactly this system works.

Code Examples:

```lua
local gun = SpawnFirearm(Players["YourName"].Position, ItemType.GunRevolver, 255) --Spawns a revolver on player pos with 255 rounds inside of it :trollface:
```

```lua
local gun = SpawnFirearm(Players["YourName"].Position, ItemType.GunRevolver, 6)

gun.StatusFlags = FirearmStatusFlags.Cocked

--Spawns a revolver, sets its ammo to 6 and sets its cocked status to cocked
```
