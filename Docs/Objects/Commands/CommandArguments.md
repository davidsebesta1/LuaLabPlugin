# Command Arguments

## About
An object representing input and output of the command.

## Properties
`Sender` - Readonly [Player](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/Player.md) who send it. Is nil if its sent from server console or executed by another Lua code.<br>
`Args` - Readonly array of string arguments which player sent with the command.<br>
`Response` - String representing command response. Can be edited.<br>
