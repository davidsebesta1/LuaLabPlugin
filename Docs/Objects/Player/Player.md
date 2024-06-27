# Player

## About
An object represnting player. Allows developer to access and modify player properties, Inventory, Role and Status Effects.<br>


## Properties
[`PlayerRole`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/PlayerRole.md) - Readonly value providing access to player´s role modification<br>
[`Inventory`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/PlayerInventory.md) - Readonly value providing access to player´s inventory item addition/modification/removal<br>
[`Effects`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/PlayerEffects.md) - Readonly value providing access to player´s status effects<br>
`Username` - Readonly string containing username of player´s steam profile<br>
`DisplayUsername` - Value of currently displaying nickname in game inside of player list<br>
`IsOffline` - Readonly bool value whenever player has left<br>

> [!IMPORTANT]
> If you store any player reference, make sure you check if he is still present on server once you use it as it will otherwise cause unwanted behavior

`UserId` - Readonly player´s steamID in format XYZ@steam<br>
`StaminaRemaining` - Value of current players stamina between 0f - 1f<br>
`Health` - Value of player´s health<br>
`Shield` - Value of player´s Hume Shield (for SCPs) or AHP (For humans)<br>
`PlayerId` - Readonly number of player´s in game id (that number in brackets behind player´s name in RA)<br>
`CurrentZone` - Readonly object of current player´s zone<br>
`CurrentRoom` - Readonly object of current player´s room<br>
`Muted` - Bool value whenever player is muted<br>
`IntercomMuted` - Bool value whenever player is muted in intercom<br>

`Position` - Vector3 of player´s position<br>
`Rotation` - Y rotation (horizontal) rotation of player<br>
`AimingPoint` - A Vector3 position where the player is aiming.<br>

## Functions
`Kick(reason)` - Kicks the player<br>
`Ban(reason, durationInSeconds)` -  Bans the player with reason for specified duration<br>
`Broadcast(message,durationInSeconds,ClearPrevious)` - Sends broadcast to the player
`Hint(message, duration)` - Sends a hint to the player

> [!TIP]
> Moonsharp version of Lua supports indexing at multiple values, so you can get multiple players by example `Players["Player1", "Player2", "Player3"]`

Code Examples:

```lua
function playerChangeRole(args)
    if args.NewRole == RoleTypeId.NtfCaptain then
        args.Player.Health = 110 -- Setting player health to 110 if new role is NtfCaptain
    end
end

Events.PlayerChangeRole:add(playerChangeRole)
```

```lua
Players["YourPlayer"].Position = Players["AnotherPlayer"].Position
--Teleporting YourPlayer to AnotherPlayer
```

```lua
Players["YourPlayer"].CurrentRoom.LightsEnabled = false
--Disables the lights in YourPlayer´s room
```

```lua
Players["YourPlayer"].Position = Players["YourPlayer"].AimingPoint
--Teleports the player to position he is currently looking at
```
