# Player Role

## About
Object inside of [`Player`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/Player.md) for in game role management.<br>

## Properties
`RoleType` - Value of type [`RoleTypeId`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/ItemTypeId.md) for getting and setting current role type.<br>

## Functions
`SetRole(RoleTypeId, RoleChangeReason, RoleSpawnFlags)` - Advanced function for setting player role with specifed changing reason and spawn flags.<br>

`SetFakeRole(RoleTypeId)` - Sets a role of this player to appear as specified role for other players. This player keeps the original role and behavior is not for him or for the server changed.<br>

Code Examples:

```lua
--basic role setting or getting
Players["YourUsername"].PlayerRole.RoleType = RoleTypeId.ClassD

--Advanced with specified arguments
Players["YourUsername"].PlayerRole:SetRole(RoleTypeId.Scientist, RoleChangeReason.RemoteAdmin, RoleSpawnFlags.UseSpawnpoint) -- sets player role to Scientist with reason RemoteAdmin but does not give the player default inventory (AssignInventory flag is missing)
```

```lua
Players["YourUsername"].PlayerRole:SetFakeRole(RoleTypeId.Spectator) -- makes the player completely visible for other players without using the invisiblity effect
```