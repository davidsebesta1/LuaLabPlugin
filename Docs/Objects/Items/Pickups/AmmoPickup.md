# Ammo Pickup

## About
Inherited class from [ItemPickup](link). Allows you to change the content of the ammo pickup

## Properties
`Ammo` - Amount of the ammo inside of this pickup

## Spawning
`SpawnAmmo(Position, ItemType, Amount)` - Spawns ammo type box on specified position with specified amount

Code Examples:

```lua
local ammoBox = SpawnAmmo(Players["YourName"].Position, ItemType.Ammo556x45, 100)

ammoBox.Scale = {2, 2, 2} -- Spawns a 5.56 box and on the player position and sets its scale to 2x
```