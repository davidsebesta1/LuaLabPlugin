# Server

## About
Global variable `Server` represents the server itself.

## Properties
`Address` - Readonly address of the server.<br>
`Port` - Readonly port on which the server is running on.<br>
`PlayerCount` - Readonly amount of current players online.<br>
`MaxPlayerCount` - Readonly amount of maximum players possible on the server.<br>
`ReservedSlots` - Readonly amount of reserved slots for players.<br>
`TPS` - Readonly TPS (ticks per second) of the server.<br>
`FriendlyFire` - Value whenever the players on the same team can damage each other.<br>

Code Example:

```lua
Server.FriendlyFire = Server.PlayerCount < 5 -- Sets the friendly fire status to true if the current playerr count is less than 5
```
