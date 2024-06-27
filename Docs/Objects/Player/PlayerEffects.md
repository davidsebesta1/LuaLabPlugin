# Player Effects

## About
An object for accessing player [status effects](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Player/PlayerStatusEffect.md)

## Accessing status effects
Accessing them is done by indexing this object with name of the effect.<br>
Code Example:

```lua
local burnedEffect = Players["YourUsername"].Effects["Burned"]
burnedEffect.Duration = 50.5
burnedEffect.Intensity = 1

--Enabling burned effect and then setting its duration and intensity
```
