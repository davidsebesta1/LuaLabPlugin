# Commands

## About Commands
There are 2 command types currently available. Those are Remote Admin console commands and Dot commands (Those you start in player console with '.' at start). Both can be added and removed at real time.

## Adding Commands
Both command types have a single method that is inside of global variable `Commands`.<br> 
**Attempting to add existing command or any same alias will throw an exception.**<br>
`RegisterCommand(name, function, aliases, description)`<br>
Paramaters:<br>
`name` - String type for main name of the command.<br>
`function` - Function with a single [`CommandArgument`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Commands/CommandArguments.md) paramater for whole args and etc.<br>
`alias` - Optional array of string alias for this command.<br>

**Return type of true/false is required as this changes the console response icon and color.**<br>

> [!WARNING]
> If the command is dot command, aliaes must start with dot too. Not doing so will cause unintended behavior.

`description` - Optional string of description of this command.<br>

> [!NOTE]
> Dot commands doesnt show descriptions. And therefore this paramater will be ignored.

Code Example:

```lua

function test(args)
    args.Response = "test from dot command"
    return true
end

Commands:RegisterCommand(".testDotCommand", test)
```

```lua
Commands:RegisterCommand("TestRACommand", function(args) args.Response = "remote admin" return false end, {"testtwo"}, "This is a test lua command description")
```

```lua

function test(args)
    if(args.Args[1] == "markiplier") then
        args.Response = "Mork"
        return true
    else 
        args.Response = "Not mork"
        return false
    end
end

Commands:RegisterCommand(".tempeventcommand", test, {".someotherstuff", ".andanother"})
```

## Removing Commands
Commands can be removed by name or any of their alias.<br>
`UnregisterCommand(name)`<br>

Code Example:

```lua
Commands:UnregisterCommand(".testDotCommand")
```

```lua
Commands:RegisterCommand("TestRACommand", function(args) args.Response = "remote admin" return false end, {"testtwo"}, "This is a test lua command description")

Commands:UnregisterCommand("testtwo") -- both options TestRACommand and testtwo are unregistered
```

## Executing Commands
Any command executio can be executed using<br>
`ExecuteCommand(name, args)`<br>
`name` - String type for name or alias of the command.<br>
`args` - String array of command arguments.<br>
Its response are two variables, first boolean value whenever the command succeded and second is [`CommandArgument`](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Commands/CommandArguments.md) that has other information about it.<br>
**If player executes command from RA console, its response is automatically sent back to the console. Otherwise it is sent to the game console.**<br>
**Command name or aliases are caps insensitive**<br>

Code Example:

```lua
Commands:RegisterCommand("TestRACommand", function(args) args.Response = "remote admin" return false end, {"testtwo"}, "This is a test lua command description")

Commands:ExecuteCommand("testracommand", {})
```

```lua
Commands:RegisterCommand("TestRACommand", function(args) args.Response = "remote admin" return false end, {"testtwo"}, "This is a test lua command description")

success, args = Commands:ExecuteCommand("testracommand", {})

print(Command execution " .. (success and "success" or "failed") .. ":" .. args.Response)
```
