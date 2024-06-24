# Status Effect

## About
Status effect is a object representing a single SL Player Status Effect, list of all status effects can be found [here](url).

## Properties
`EffectName` - A readonly text value containing in game name of this effect<br>
`Classification` - Enum value about effect class (Positive/Mixed/Negative)<br>
`IsEnabled` - Bool value whenever effect is enabled. Enabling it sets intensity to 1 and duration to endless.<br>
`Intensity` - A whole number value representing intensity of a effect. For their meanings check the url in About section.<br>
`Duration` - Decimal number value for the duration of the effect.<br>¨

Code Example:

```lua
local burnedEffect = Players["YourUsername"].Effects["Burned"]
burnedEffect.Duration = 50.5
burnedEffect.Intensity = 1

--Enabling burned effect and then setting its duration and intensity
```

```lua
Players["YourUsername"].Effects["SeveredHands"].Enabled = true

--Enables the severed hands effect
```