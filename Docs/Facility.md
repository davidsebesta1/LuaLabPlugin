# Facility

## About
A global variable `Facility` represents the whole in game facility and allows scripters to access each Facility Zone, named Room and all lights.

## Properties
`TeslaGates` - Returns a new array of all [TeslaGate](link) objects.<br>
`Generators` - Returns a new array of all [Generators](link).<br>

## Indexers
`Facility` has two readonly indexers:
1. For FacilityZone. Returning zone as a object

Code Example:

```lua
Facility[FacilityZone.LightContainment]:FlickerLights(5)

--Turns off all lights in LCZ for 5 seconds
```

2. For Room. Returning a room object

Code Example:

```lua
Facility[RoomName.Lcz914].LightsColor = {1, 0, 0}

--Changes 914 room color to red
```

## Functions
`TurnOffAllLight()` - Guess
`TurnOnAllLights()` - Guess again

Code Example:

```lua
Facility:TurnOffAllLights() -- Guess what it does.. you wont believe it but it shuts all lights
```
