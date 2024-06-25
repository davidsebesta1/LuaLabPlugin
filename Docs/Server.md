# Server

## About
Global variable `Server` represents the server itself.

## Properties
`Address` - Readonly address of the server.<br>
`Port` - Readonly port on which the server is running on.
`PlayerCount` - Readonly amount of current players online.
`MaxPlayerCount` - Readonly amount of maximum players possible on the server.
`ReservedSlots` - Readonly amount of reserved slots for players.
`TPS` - Readonly TPS (ticks per second) of the server.
`FriendlyFire` - Value whenever the players on the same team can damage each other.

Code Example:

```lua
Server.FriendlyFire = Server.PlayerCount < 5 -- Sets the friendly fire status to true if the current playerr count is less than 5
```