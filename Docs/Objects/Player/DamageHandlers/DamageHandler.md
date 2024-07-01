# Damage Handler

## About
An object representing the damage taken from it.

## Properties
`DamageTaken` - Readonly value of the damage that has been applied to the player<br>

Code Example:

```lua
function onPlayerDying(args)
    args.Player:broadcast(args.DamageHandler.Reason .. "\nTaken " .. args.DamageHandler.Damage .. " damage", 5)
    return true
end

Events.PlayerDying:add(onPlayerDying)
```