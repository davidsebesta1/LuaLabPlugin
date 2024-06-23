# Players

## About
`Players` is a global variable for getting all or specific players (wow).

## Usage

### Getting All Players
`AllPlayers`<br>
A get-only variable returning a new array of all players currently in game.

### Indexers
`Players[X]`<br>
Indexer directly on the `Players` object for getting specific player by his in game id (that one in brackers if you look into RA)

`Players["name"]`
Indexer directly on the `Players` object for getting a specific player by his in game username

### Functions
This object also contains functions for administrating players. They are global functions and doesnt need to by called from `Players` object.<br>
`Mute(player)` - Mutes the player<br>
`Unmute(player)` - Unutes the player<br>
`Kick(player)` - Kicks the player<br>
`Ban(player)` - Bans the player<br>
