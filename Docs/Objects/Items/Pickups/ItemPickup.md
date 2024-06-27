# Item Pickup

## About
An object representing pickable item. Allowing you to access its properties and modify them.<br>
This class has inherited classes for firearm and ammo pickup.

## Properties
`Position` - Vector3 world position of this pickup.<br>
`Rotation` - Euler angles (degrees angle) of this item pickup.<br>
`Scale` - Vector3 scale of this pickup.<br>
`ItemType` - Readonly enum of type [`ItemType`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/ItemTypeId.md).<br>
`ItemSerial` - Unique readonly number of associated item.<br>
`Locked` - Boolean value representing whenever this can be picked up.<br>

## Spawning Items
Items can be spawned using the global function `SpawnItem(Position,ItemType)`, which spawns item of type at position and returns the pickup object.<br>
There are also methods for specifically spawning firearm or ammo. You can find these on [FirearmPickup](link) and [AmmoPickup](link).<br>

Code examples:

```lua
local plrPosition = Players["YourName"].Position

local pickup = SpawnItem(plrPosition, ItemType.Medkit)

--Spawns medkit on player
```

```lua
local plrPosition = Players["YourName"].Position

local pickup = SpawnItem(plrPosition, ItemType.Medkit)

pickup.Rotation = {0, 90, 0} -- sets rotation to 90 degrees on Y axis
pickup.Locked = true
```
