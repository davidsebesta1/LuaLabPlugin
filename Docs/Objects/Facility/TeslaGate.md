# Tesla Gate

## About
An object representing a single tesla gate, allowing you to control it.

## Properties
`Room` - Readonly value about the room this tesla is in.

## Functions
`Burst()` - Plays the tesla gate burst.<br>
`InstantBurst()` - Plays the instant burst as the 079 ability does.<br>

Code Examples:

```lua
--Instantly activates all tesla gates
lua for _,value in pairs(Facility.TeslaGates) do v
  value.InstantBurst()
end
```
