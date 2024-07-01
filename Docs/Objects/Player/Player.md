# Player

## About
An object represnting player. Allows developer to access and modify player properties, Inventory, Role and Status Effects.<br>


## Properties
[`PlayerRole`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/PlayerRole.md) - Readonly value providing access to player´s role modification.<br>
[`Inventory`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/PlayerInventory.md) - Readonly value providing access to player´s inventory item addition/modification/removal.<br>
[`Effects`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/PlayerEffects.md) - Readonly value providing access to player´s status effects<.br>
`Username` - Readonly string containing username of player´s steam profile.<br>
`DisplayUsername` - Value of currently displaying nickname in game inside of player list.<br>
`InfoArea` - A flag enum of type [`PlayerInfoArea`](link).<br>
`CustomInfo` - A string of custom info appear with username, badge and other stuff on hovering over played in game. For colors use [`PlayerInfoColorTypes`](link) for available colors.<br>
`InfoViewRange` - A decimal number of the player info appearing range.<br>

> [!IMPORTANT]
> If you want to make the text colorful and you dont use the [`PlayerInfoColorTypes`](link) then the custom info wont be set and error will be thrown in server console.

`IsOffline` - Readonly bool value whenever player has left.<br>

> [!IMPORTANT]
> If you store any player reference, make sure you check if he is still present on server once you use it as it will otherwise cause unwanted behavior.

`UserId` - Readonly player´s steamID in format XYZ@steam<.br>
`StaminaRemaining` - Value of current players stamina between 0f - 1f.<br>
`Health` - Value of player´s health.<br>
`Shield` - Value of player´s Hume Shield (for SCPs) or AHP (For humans).<br>
`PlayerId` - Readonly number of player´s in game id (that number in brackets behind player´s name in RA).<br>
`DoNotTrack` - Readonly boolean value whenever the player requests to not be tracked.<br>
`CurrentZone` - Readonly object of current player´s zone.<br>
`CurrentRoom` - Readonly object of current player´s room.<br>
`Muted` - Bool value whenever player is muted.<br>
`IntercomMuted` - Bool value whenever player is muted in intercom.<br>

`Position` - Vector3 of player´s position.<br>
`Rotation` - Y rotation (horizontal) rotation of player.<br>
`Scale` - Vector3 player scale.<br>

> [!WARNING]
> Setting player scale may cause unintended behavior.

`AimingPoint` - A Vector3 position where the player is aiming.<br>

`SessionVariables` - A table that anyone can use to share any value by string key across multiple scripts.<br>

`IsDisarmed` - Bool value representing whenever the player is disarmed.<br>
`IsAlive` - Bool value if the player is alive or not. Setting it to true will cause player to die normally.<br>

## Functions
`Kick(reason)` - Kicks the player.<br>
`Ban(reason, durationInSeconds)` -  Bans the player with reason for specified duration.<br>
`Broadcast(message,durationInSeconds,ClearPrevious)` - Sends broadcast to the player.<br>
`Hint(message, duration)` - Sends a hint to the player.<br>

> [!TIP]
> Moonsharp version of Lua supports indexing at multiple values, so you can get multiple players by example `Players["Player1", "Player2", "Player3"]`.

Code Examples:

```lua
function playerChangeRole(args)
    if args.NewRole == RoleTypeId.NtfCaptain then
        doAfter(0.1, function()
            args.Player.Health = 110
            end);
        return true
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
