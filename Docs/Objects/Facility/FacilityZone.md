# Facility Zone

## About
Representation of a whole zone inside of the facility. Allows scripters to get it´s rooms and access light features

## Properties
`ZoneType` - Readonly enum FacilityZone value of this zone type.<br>
`Rooms` - Returns a new array of all rooms inside of this facility zone<br>

## Functions
`FlickerLights(duration)` - Shuts all lights inside of this zone for specified duration<br>

## Accessing Special Rooms
You can access special rooms (Lcz914, Hcz049...) by indexing this object with `RoomName` enum value, which returns a `FacilityRoom` object. Returns nil of no room if this type found.<br>

Code Examples:

```lua
Facility[FacilityZone.LightContainment][RoomName.Lcz914].LightColor = {0, 1, 0}

--Sets lights color to green in 914 room
```

```lua
for key, value in pairs(Facility[FacilityZone.LightContainment].Rooms) do
    value.LightsEnabled = !value.LightsEnabled
end

--Shuts all turned on lights and turns on all lights that were previously shut down inside of LCZ
```