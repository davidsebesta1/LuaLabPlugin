# Players

## About
`Players` is a global variable for getting all or specific players.

## Usage

### Getting All Players
`AllPlayers`<br>
A get-only variable returning a new array of all players currently in game.

Code example:
```lua
for key, value in pairs(Players.AllPlayers) do
    value:Broadcast("Sí plas plas", 5)
end
```

### Indexers
`Players[X]`<br>
Indexer directly on the `Players` object for getting specific player by his in game id (that one in brackers if you look into RA).<br>

`Players["name"]`<br>
Indexer directly on the `Players` object for getting a specific player by his in game username.<br>

`Players[RoleTypeId]`<br>
Indexer that returns a array of players whose role is equal to the one in parameter.<br>

`Players[Team]`<br>
Indexer returning an array of players whose team is equal to the one in parameter.<br>


Code example:
```lua
player1 = Players[1]

player1:broadcast("Hello from Lua", 5)

Players["YourNickname"]:Hint("Hint", 3)
```

```lua
for key,value in pairs(Players[RoleTypeId.ClassD]) do
    value:GiveItem(ItemType.Medkit)
end
--Gives medkit to all Class D players
```

### Functions
This object also contains functions for administrating players.<br>
`Mute(player)` - Mutes the player<br>
`Unmute(player)` - Unutes the player<br>
`Kick(player, reason)` - Kicks the player<br>
`Kick(playerSteamID, reason)` - Kicks the player<br>
`Ban(player, reason, durationInSeconds)` - Bans the player<br>
`Ban(playerSteamID, reason, durationInSeconds)` - Bans the player<br>
`Unban(playerSteamID)` - Bans the player<br>

> [!NOTE]
> Player itself has these methods too except for unban. And since there is one I just added the rest..

<br>
Code example:

```lua
Players.Kick(Players["YourNickname"], "Grenade in elevator")

Players["YourNickname"]:Kick("Grenade in elevator")
--both options are valid
```
