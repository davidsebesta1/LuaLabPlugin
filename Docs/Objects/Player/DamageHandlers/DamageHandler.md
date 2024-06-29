# Damage Handler

## About
An object representing the damage taken from it.

## Properties
`DamageTaken` - Readonly value of the damage that has been applied to the player<br>

Code Example:

```lua
function onPlayerDying(args)
    args.Player:broadcast("You died, damage taken: " .. args.DamageHandler.Damage, 5)
    return true
end

Events.PlayerDying:add(onPlayerDying)
```