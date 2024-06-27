# Facility Door

## About
Object representing a door and its status.

## Properties
`IsOpened` - Bool value whenever the door is opened or not.<br>
`IsLocked` - Bool value about door locked status.<br>
`LockReason` - Enum of type [`DoorLockReason`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/DoorLockReason.md) having the reason for why the door is locked (RA, 079.. etc).<br>
`Permissions` - Enum flag type of [`KeycardPermissions`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/KeycardPermissions.md) containing value of which keycard permissions can open this door (Its flag system, use &, | operator for setting it).<br>

## Functions
`Explode()` - Will cause door to explode... if possible. Doesnt do anything to gates or unbreakable doors.<br>

Code Examples:

```lua
for key,value in pair(Facility[RoomName.Lcz914].Doors) do
    value.IsOpened = false
end

--Closes all doors inside of 914 room
```

```lua
Facility[RoomName.Lcz914].Doors[0].Permissions = KeycardPermissions.ContainmentLevelThree | KeycardPermissions.ScpOverride


--Makes 914 room accessible by either containment level 3 card or any SCP
```
