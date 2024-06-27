# Cassie

## About
`Cassie` is a global variable providing access to the cassie´s in game functions.

## Properties
`IsSpeaking` - Readonly bool value whenever cassie is currently speaking

## Functions
`Message(message, isHeld, isNoisy, isSubtitles)` - Sends a cassie message, isHeld waits for the current message to finish, isNoisy inserts random glitching noises and isSubtitles enables subtitles for this message (shown only if user has them enabled locally).<br>
`GlitchyMessage(message, glitchChance, jamChance)` - Sends a cassie message with glitch chance after each word (0-1 value) and jam chance after each word (0-1 value).<br>
`Clear()` - Clears the cassie current queue.<br>
`CalculateDuration(message)` - Returns a decimal number for however message is gonna take for it to be spoken by cassie<br>
`ConvertTeam(Team, unitName)` - Converts team to death message.
<br><br> 
Example: [Team.ClassD](https://github.com/davidsebesta1/LuaLabPlugin/blob/master/Docs/Objects/Enums/Team.md) returns 'BY CLASSD PERSONNEL' and etc. <br>
Unit name is converted to NATO_X Y.
<br><br>
Example: 'ALPHA-5' is converted to 'NATO_A 5'<br>
`ConvertNumber(number)` - Converts a whole number to text.<br>
Example: 505 is converted to 'Five hundered and five'<br>
`IsValid(word)` - Returns true whenever the word is in cassie´s dictionary<br>

Code Examples:

```lua
Cassie.Message("Class D personnel escape detected", true, false, true)
Cassie.Message("Class D personnel escape detected") -- note that all of these boolean values are optional in this case and can be ignored if needed

Cassie.GlitchyMessage("Unknown personnel detected at gate A", 0.45, 0.3)
```
