# Base Item

## About
A base class for any inventory item. Contains base information about it.

## Properties
`ItemType` - Readonly Enum of type [`ItemType`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/ItemTypeId.md).<br>
`ItemCategory` - Readonly Enum of type [`ItemCategory`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/ItemCategory.md).<br>
`Serial` - Readonly Whole number of this items unique number.<br>
`Weight` - Readonly Decimal number of this item weight.<br>
`Owner` - Readonly Reference to the player in which inventory it is<br>

## Functions
`Drop()` - Drops the item from player´s inventory and returns pickup object.<br>

Code Examples

```lua

function onChangeItem(args)
    for key,value in pairs(args.Player.Inventory.AllItems) do
        if value.Serial == args.NewItem and value.ItemType == ItemType.Medkit then
            value.Drop()
        end
    end
end

Events.PlayerChangeItem:add(onChangeItem)
--This code drops any medkit equipped item by the player, note that NewItem and OldItem args are just serial numbers of the items
```
