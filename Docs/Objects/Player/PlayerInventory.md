# Player Inventory

## About
An object inside of [`Player`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/Player.md) for adding/modifying/deleting content from players inventory

## Properties
`CurrentItem` - Item that is player currently holding *(nil if holding nothing)*<br>
`AllItems` - Returns new array of all items that the player has on him

# Functions
`GiveItem(ItemType)` - Attempts to give the player item of ItemType and returns the appropriate object. If player has full inventory, no item is given and returns nil<br>
`GetItem(index)` - Attempts to return item at specified inventory index (0-8). Returns nil if no item is present<br>
`GetItems(ItemType)` - Returns a new array of all items of specified item type<br>
`RemoveItem(ItemType)` - Removes first found item from player´s inventory if specified item type<br>
`RemoveItem(item)` - Removes item from player inventory by item reference.<br>
`DropItem(ItemType)` - Drops the first item found with specified ItemType and returns pickup object<br>
`DropItem(item)` - Drops the item by reference and returns pickup object<br>
`GiveAmmo(ItemType, amount)` - Gives the player ammo of specified item<br>

> [!TIP]
> Player inventory ammo can also hold other types than ammo itself :trollface:

`SetAmmo(ItemType, amount)` - Sets the ammo amount of specified ItemType to amount<br>
`GetAmmo(ItemType)` - Returns the amount of ammo of the specified type player current has<br>
`DropAmmo(ItemType, amount)` - Drops the specified amount of ammo and returns array of ammo pickups that user has dropped<br>

Code Examples:

```lua
function playerChangeRole(args)
    if args.NewRole == RoleTypeId.ClassD and args.ChangeReason == RoleChangeReason.RoundStart then
        doAfter(0.1, function()
            if math.random() > 0.5 then
                args.Player.Inventory:GiveItem(ItemType.Coin)
            end
        end)
    end
end

Events.PlayerChangeRole:Add(playerChangeRole)
-- gives player a coin by 50% chance on round start
```

```lua
for key, value in pairs(Players["YourPlayer"].Inventory.AllItems) do
    if value.ItemType == ItemType.Medkit then
        Players["YourPlayer"].Inventory:RemoveItem(value)
    end
end

--Removes all medkits from player´s inventory
```

```lua
for key, value in pairs(Players.AllPlayers) do
    value.Inventory:GiveItem(ItemType.Medkit)
end

--Gives all players a medkit
```
