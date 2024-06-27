# Generator

## About
An object representing generator to recontain SCP-079. All generator instances can be get by `Facility.Generators`

## Properties
`ActivationReady` - Readonly bool about if the generator can be activated.<br>
`IsOpen` - Boolean value if the generator is open.<br>
`IsUnlocked` - Boolean value if the generator is unlocked and can be opened/closed.<br>
`RemainingTime` - Readonly decimal number of how much time is left until the generator is activated.<br>
`Engaged` - Boolean value if the generator is engaged.<br>
`Activating` - Boolean value if the generator is active and time is going down.<br>

Code Examples:

```lua
for key,value in pairs(Facility.Generators) do
    value.IsOpen = true
end

--Opens all generators
```