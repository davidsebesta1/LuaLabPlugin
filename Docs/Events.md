# Events

## About Events
Event handlers are stored inside of a `Events` global variable, each event is a property of this object.<br>

## Subscription
You can subscribe to them by using `:add(function)`, and unsubscribe by `:remove(function)`.<br>
If you are using hot or live reload then EVERY subscribes event is automatically unsubscribed on reload.<br>

Every event has a single argument, which is a object containing all information about the event.<br>
Code example:
```lua
function playerJoined(args) -- single args object containing all event properties
    LastPlayerJoined = args.Player
    print("Welcome " .. args.Player.Username)
end

Events.PlayerJoined:add(playerJoined); -- registering the event
```

## Event Cancellation
Some events can be cancelled, this is done by returning false.
```lua
function onGrenadeExploding(args)
    return false
end

Events.GrenadeExploded.add(onGrenadeExploding)
```

> [!WARNING]
> Not specifing the return value will always evaluate to false, please specify return to **true** if you do not want to cancel it

```lua
function onGrenadeExploding(args) -- grenade wont cause any damage as it doesnt return true - therefore its cancelled
    print("kaboom")
end

Events.GrenadeExploded.add(onGrenadeExploding)
```

<br><br>
## Available Events
These are the events that are currently available in format EventName - Objects that are inside of args param<br>
`PlayerJoined` - `Player` <br>
`PlayerLeft` - `Player` <br>
`PlayerDying` - `Player` `Attacker` `DamageHandler` <br>
`LczDecontaminationStart` - <br>
`LczDecontaminationAnnouncement` - `Id` <br>
`MapGenerated` - <br>
`GrenadeExploded` - `Thrower` `Position` `Grenade` <br>
`ItemSpawned` - `Item` `Position` <br>
`GeneratorActivated` - `Generator` <br>
`PlaceBlood` - `Player` `Position` <br>
`PlaceBulletHole` - `Position` <br>
`PlayerActivateGenerator` - `Player` `Generator` <br>
`PlayerAimWeapon` - `Player` `Firearm` `IsAiming` <br>
`PlayerBanned` - `Player` `Issuer` `Reason` `Duration` <br>
`PlayerCancelUsingItem` - `Player` `Item` <br>
`PlayerChangeItem` - `Player` `OldItem` `NewItem` <br>
`PlayerChangeRadioRange` - `Player` `Radio` `Range` <br>
`PlayerChangeSpectator` - `Player` `OldTarget` `NewTarget` <br>
`PlayerCloseGenerator` - `Player` `Generator` <br>
`PlayerDamagedShootingTarget` - `Player` `ShootingTarget` `DamageHandler` `DamageAmount` <br>
`PlayerDamagedWindow` - `Player` `Window` `DamageHandler` `DamageAmount` <br>
`PlayerDeactivatedGenerator` - `Player` `Generator` <br>
`PlayerDropAmmo` - `Player` `Item` `Amount` <br>
`PlayerDropItem` - `Player` `Item` <br>
`PlayerDryfireWeapon` - `Player` `Firearm` <br>
`PlayerEscape` - `Player` `NewRole` <br>
`PlayerHandcuff` - `Player` `Target` <br>
`PlayerRemoveHandcuffs` - `Player` `Target` `CanRemoveHandcuffsAsScp` <br>
`PlayerDamage` - `Player` `Target` `DamageHandler` <br>
`PlayerInteractElevator` - `Player` `Elevator` <br>
`PlayerInteractLocker` - `Player` `Locker` `Chamber` `CanOpen` <br>
`PlayerInteractScp330` - `Player` `Uses` `PlaySound` `AllowPunishment` <br>
`PlayerInteractShootingTarget` - `Player` `ShootingTarget` <br>
`PlayerKicked` - `Player` `Issuer` `Reason` <br>
`PlayerMakeNoise` - `Player` <br>
`PlayerOpenGenerator` - `Player` `Generator` <br>
`PlayerPickupAmmo` - `Player` `Item` <br>
`PlayerPickupArmor` - `Player` `Item` <br>
`PlayerPickupScp330` - `Player` `Item` <br>
`PlayerPreauth` - `UserId` `IpAddress` `Expiration` `CentralFlags` `Region` `Signature` `ConnectionRequest` `ReaderStartPosition` <br>
`PlayerReceiveEffect` - `Player` `Effect` `Intensity` `Duration` <br>
`PlayerReloadWeapon` - `Player` `Firearm` <br>
`PlayerChangeRole` - `Player` `OldRole` `NewRole` `ChangeReason` <br>
`PlayerSearchPickup` - `Player` `Item` <br>
`PlayerSearchedPickup` - `Player` `Item` <br>
`PlayerShotWeapon` - `Player` `Firearm` <br>
`PlayerSpawn` - `Player` `Role` <br>
`RagdollSpawn` - `Player` `Ragdoll` `DamageHandler` <br>
`PlayerThrowItem` - `Player` `Item` `Rigidbody` <br>
`PlayerToggleFlashlight` - `Player` `Item` `IsToggled` <br>
`PlayerUnloadWeapon` - `Player` `Firearm` <br>
`PlayerUnlockGenerator` - `Player` `Generator` <br>
`PlayerUsedItem` - `Player` `Item` <br>
`PlayerUseHotkey` - `Player` `Action` <br>
`PlayerUseItem` - `Player` `Item` <br>
`PlayerReport` - `Player` `Target` `Reason` <br>
`PlayerCheaterReport` - `Player` `Target` `Reason` <br>
`RoundEnd` - `LeadingTeam` <br>
`RoundRestart` - <br>
`RoundStart` - <br>
`WaitingForPlayers` - <br>
`WarheadStart` - `IsAutomatic` `Player` `IsResumed` <br>
`WarheadStop` - `Player` <br>
`WarheadDetonation` - <br>
`PlayerMuted` - `Player` `Issuer` `IsIntercom` <br>
`PlayerUnmuted` - `Player` `Issuer` `IsIntercom` <br>
`PlayerCheckReservedSlot` - `Userid` `HasReservedSlot` <br>
`RemoteAdminCommand` - `Sender` `Command` `Arguments` <br>
`PlayerGameConsoleCommand` - `Player` `Command` `Arguments` <br>
`ConsoleCommand` - `Sender` `Command` `Arguments` <br>
`TeamRespawnSelected` - `Team` <br>
`TeamRespawn` - `Team` `Players` `NextWaveMaxSize` <br>
`Scp106Stalking` - `Player` `Activated` <br>
`PlayerEnterPocketDimension` - `Player` <br>
`PlayerExitPocketDimension` - `Player` `IsSuccessful` <br>
`PlayerThrowProjectile` - `Thrower` `Item` `ProjectileSettings` `FullForce` <br>
`Scp914Activate` - `Player` `KnobSetting` <br>
`Scp914KnobChange` - `Player` `KnobSetting` `PreviousKnobSetting` <br>
`Scp914UpgradeInventory` - `Player` `Item` `KnobSetting` <br>
`Scp914UpgradePickup` - `Item` `OutputPosition` `KnobSetting` <br>
`Scp106TeleportPlayer` - `Player` `Target` <br>
`Scp173PlaySound` - `Player` `SoundId` <br>
`Scp173CreateTantrum` - `Player` <br>
`Scp173BreakneckSpeeds` - `Player` `Activate` <br>
`Scp173NewObserver` - `Player` `Target` <br>
`Scp939CreateAmnesticCloud` - `Player` <br>
`Scp939Lunge` - `Player` `State` <br>
`Scp939Attack` - `Player` `Target` <br>
`Scp079GainExperience` - `Player` `Amount` `Reason` <br>
`Scp079LevelUpTier` - `Player` `Tier` <br>
`Scp079UseTesla` - `Player` `Tesla` <br>
`Scp079LockdownRoom` - `Player` `Room` <br>
`Scp079LockDoor` - `Player` `Door` <br>
`Scp079UnlockDoor` - `Player` `Door` <br>
`Scp079BlackoutZone` - `Player` `Zone` <br>
`Scp079BlackoutRoom` - `Player` `Room` <br>
`Scp049ResurrectBody` - `Player` `Target` `Body` <br>
`Scp049StartResurrectingBody` - `Player` `Target` `Body` `CanResurrct` <br>
`PlayerInteractDoor` - `Player` `Door` `CanOpen` <br>
`Scp173SnapPlayer` - `Player` `Target` <br>
`Scp079CancelRoomLockdown` - `Player` `Room` <br>
`BanIssued` - `BanDetails` `BanType` <br>
`BanRevoked` - `Id` `BanType` <br>
`RemoteAdminCommandExecuted` - `Sender` `Command` `Arguments` `Result` `Response` <br>
`PlayerGameConsoleCommandExecuted` - `Player` `Command` `Arguments` `Result` `Response` <br>
`ConsoleCommandExecuted` - `Sender` `Command` `Arguments` `Result` `Response` <br>
`BanUpdated` - `BanDetails` `BanType` <br>
`PlayerPreCoinFlip` - `Player` <br>
`PlayerCoinFlip` - `Player` `IsTails` <br>
`PlayerInteractGenerator` - `Player` `Generator` `GeneratorColliderId` <br>
`RoundEndConditionsCheck` - `BaseGameConditionsSatisfied` <br>
`Scp914PickupUpgraded` - `Item` `NewPosition` `KnobSetting` <br>
`Scp914InventoryItemUpgraded` - `Player` `Item` `KnobSetting` <br>
`Scp914ProcessPlayer` - `Player` `KnobSetting` `OutPosition` <br>
`Scp079CameraChanged` - `Player` `Camera` <br>
`Scp096AddingTarget` - `Player` `Target` `IsForLook` <br>
`Scp096Enraging` - `Player` `InitialDuration` <br>
`Scp096ChangeState` - `Player` `RageState` <br>
`Scp096Charging` - `Player` <br>
`Scp096PryingGate` - `Player` `GateDoor` <br>
`Scp096TryNotCry` - `Player` <br>
`Scp096StartCrying` - `Player` <br>
`PlayerUsingRadio` - `Player` `Radio` `Drain` <br>
`CassieAnnouncesScpTermination` - `Player` `DamageHandler` `Announcement` <br>
`PlayerGetGroup` - `UserId` `Group` <br>
`PlayerUsingIntercom` - `Player` `IntercomState` <br>
`PlayerDeath` - `Player` `Attacker` `DamageHandler` <br>
`PlayerRadioToggle` - `Player` `Radio` `NewState` <br>
`PlayerDroppedAmmo` - `Player` `Item` `Amount` `MaxAmount` <br>
