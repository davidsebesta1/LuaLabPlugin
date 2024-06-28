# Code Execution With Delay

Any code can be executed with delay by using the `doAfter(delayInSeconds, function)` global function.<br>
This method is primary for some events, as they do need a little bit of time before executing any code that may change player properties especially after changing role event.<br>

Code Example:

```lua
function playerChangeRole(args)
  if args.NewRole == RoleTypeId.NtfCaptain then
      doAfter(0.1, function()
        args.Player.Health = 110
        end)
      return true
    end
  end
Events.PlayerChangeRole:add(playerChangeRole)
```
