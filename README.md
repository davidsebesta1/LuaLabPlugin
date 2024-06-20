# Lua Lab Plugin

SCP Secret Laboratory Plugin for writing and executing scripts in Lua using Moonsharp interpreter, allowing players to create their own plugins easier than never before

## Features
### Simple API to work with
- API itself was created to be as simple as possible but still being organized into logical segments.
- Simple functions to add and remove event handlers for in game events
  
### Documentation
- Everything is documentated and includes sample code

## Plugin Management
- 1 lua file = 1 plugin. Any required lua dependencies are put into seperate folder

## Sandboxing
- Each .lua and player that executes code has its own environment, so you dont need to worry about collisions

## :fire: Live and Hot Reload Available!
- Simple as Ctrl + S! Save your .lua file in your favourite IDE and it gets automatically updated live
- Enable by a single line of code at start of your .lua file, no other work required!
- Automatic event unregistering in new script, so you dont have to worry about these
### Live Reload
- Reloads the script without keeping the state
### Hot Reload
- Reloads the script with keeping the state of the **global** variables

## Lua Execution in game
- Plugin allows specified players to execute any lua code in game via RA console.
- Useful for doing one-time tasks that may require automation, such as setup up events, loadouts and et cetera.
**Note that allowing players to execute code is a dangerous permission and should be given to trusted individual.**
