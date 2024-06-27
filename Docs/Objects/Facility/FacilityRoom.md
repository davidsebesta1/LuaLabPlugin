# Facility Room

## About
An object representing a single room inside of the facility, allowing you to modify its lights and get overall information about it.

## Properties
`Doors` - Returns a new array of all doors that this room has inside of it.<br>
`Shape` - Readonly [`RoomShape`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/RoomShape.md) enum of shape of this room.<br>
`Name` - Readonly [`RoomName`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/RoomName.md) enum of how is this room internally called.<br>
`Zone` - Readonly [`FacilityZone`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Facility/FacilityZone.md) object of which this room is in.<br>
`LightsEnabled` - Bool value whenever lights in this room.<br>
`LightColor` - A color value (RGB each 0f - 1f) of this room lights color.<br>

## Functions
`FlickerLights(duration)` - Turns off all lights inside this room for specified duration

Code Examples

```lua
Facility[RoomName.Lcz914].LightsEnabled = false

--Turns off lights in 914
```

```lua
for key,value in pairs(Facility[RoomName.Lcz914].Doors) do
    if value.DoorType == DoorType.Gate then
        value.IsLocked = true
    end
end

--Locks the gate in 914
```
