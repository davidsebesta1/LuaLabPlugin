# Grenade Pickup

## About
Inherited class from [ItemPickup]() allowing you to control grenades. Grenades in SL are considered Explosive grenade, flashbang and SCP-018.

## Properties
`Owner` - Readonly player reference to the player which dropped it.<br>
`FuseTime` - Decimal value of when the grenade is gonna explode. Setting it <= 0 will cause grenade to instantly explode. Setting it anything above will set it off and start the detonation countdown<br>
`Detonated` - Boolean value of if the grenade has exploded. Setting it to true will result in a instant detonation.<br>

Code Example:

```lua
local grenade = SpawnItem(Players["YourName"].Position, ItemType.GrenadeHE)

grenade.Detonated = true
-- Spawns a grenade at player and detonates it instantly (significant amount of trolling)
```

```lua
local grenade = SpawnItem(Players["YourName"].Position, ItemType.GrenadeHE)

grenade.FuseTime = 60
-- Spawns a grenade at player and sets its fuse time to 60. Making it slowly beep until explosion
```

```lua
function onGrenadeExploding(args)
    local grenade = SpawnItem(args.Position, ItemType.GrenadeHE)
    grenade.FuseTime = 1
    return true
end

Events.GrenadeExploded.add(onGrenadeExploding) -- Sisyphean problem of detonating grenades
```