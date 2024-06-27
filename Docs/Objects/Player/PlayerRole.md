# Player Role

## About
Object inside of [`Player`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/Player.md) for in game role management

## Properties
`RoleType` - Value of type [`RoleTypeId`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/ItemTypeId.md) for getting and setting current role type¨

## Functions
`SetRole(RoleTypeId, RoleChangeReason, RoleSpawnFlags)` - Advanced function for setting player role with specifed changing reason and spawn flags

Code Example:

```lua
--basic role setting or getting
Players["YourUsername"].PlayerRole.RoleType = RoleTypeId.ClassD

--Advanced with specified arguments
Players["YourUsername"].PlayerRole:SetRole(RoleTypeId.Scientist, RoleChangeReason.RemoteAdmin, RoleSpawnFlags.UseSpawnpoint) -- sets player role to Scientist with reason RemoteAdmin but does not give the player default inventory (AssignInventory flag is missing)
```
