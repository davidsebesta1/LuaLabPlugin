# Round

## About
`Round` is a global variable representing the current in game round and its status.

## Properties
`IsStarted` - Readonly boolean value about if the round is started.<br>
`IsEnded` - Readonly boolean value if the round has ended.<br>
`IsLocked` - Boolean value if the round is locked. (Locked = cant end by normal game means)<br>
`IsLobbyLocked` - Boolean value if the round can start by normal countdown means.<br>
`DurationInSeconds` - Readonly whole number value of the seconds that passed since the round started.<br>

## Functions
`Start()` - Forcefully starts the round.<br>
`End()` - Forcefully ends the round.<br>
`Restart(fast)` - Restarts the round. If the paramater is set to true, no loading screen for players is presents and their screen is frozen until round has restarted.<br>
`RespawnWave(SpawnableTeamType)` - Respawns a wave via normal game means by specified type.<br>
`InstantRespawnWave(SpawnableTeamType)` - Instantly respawns a wave of players.<br>

Code Examples:

```lua
function onPlayerDied(args) 
    if #Players.AllPlayers < 2 then
        Round:restart() -- not specifying the fast restart value is defaulted to false
    end
end

Events.PlayerDeath:add(onPlayerDied)
```