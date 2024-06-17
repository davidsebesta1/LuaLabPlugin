using HarmonyLib;
using LuaLab.ObjectsWrappers.Events;
using MoonSharp.Interpreter;
using PluginAPI.Core;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LuaLab.ObjectsWrappers.Managers
{
    [MoonSharpUserData]
    public class LuaEventManager
    {
        #region Events DO NOT LOOK HERE

        public readonly LuaEvent<PlayerJoinedEvent> PlayerJoined = new LuaEvent<PlayerJoinedEvent>();
        public readonly LuaEvent<PlayerLeftEvent> PlayerLeft = new LuaEvent<PlayerLeftEvent>();
        public readonly LuaEvent<PlayerDyingEvent> PlayerDying = new LuaEvent<PlayerDyingEvent>();
        public readonly LuaEvent<LczDecontaminationStartEvent> LczDecontaminationStart = new LuaEvent<LczDecontaminationStartEvent>();
        public readonly LuaEvent<LczDecontaminationAnnouncementEvent> LczDecontaminationAnnouncement = new LuaEvent<LczDecontaminationAnnouncementEvent>();
        public readonly LuaEvent<MapGeneratedEvent> MapGenerated = new LuaEvent<MapGeneratedEvent>();
        public readonly LuaEvent<GrenadeExplodedEvent> GrenadeExploded = new LuaEvent<GrenadeExplodedEvent>();
        public readonly LuaEvent<ItemSpawnedEvent> ItemSpawned = new LuaEvent<ItemSpawnedEvent>();
        public readonly LuaEvent<GeneratorActivatedEvent> GeneratorActivated = new LuaEvent<GeneratorActivatedEvent>();
        public readonly LuaEvent<PlaceBloodEvent> PlaceBlood = new LuaEvent<PlaceBloodEvent>();
        public readonly LuaEvent<PlaceBulletHoleEvent> PlaceBulletHole = new LuaEvent<PlaceBulletHoleEvent>();
        public readonly LuaEvent<PlayerActivateGeneratorEvent> PlayerActivateGenerator = new LuaEvent<PlayerActivateGeneratorEvent>();
        public readonly LuaEvent<PlayerAimWeaponEvent> PlayerAimWeapon = new LuaEvent<PlayerAimWeaponEvent>();
        public readonly LuaEvent<PlayerBannedEvent> PlayerBanned = new LuaEvent<PlayerBannedEvent>();
        public readonly LuaEvent<PlayerCancelUsingItemEvent> PlayerCancelUsingItem = new LuaEvent<PlayerCancelUsingItemEvent>();
        public readonly LuaEvent<PlayerChangeItemEvent> PlayerChangeItem = new LuaEvent<PlayerChangeItemEvent>();
        public readonly LuaEvent<PlayerChangeRadioRangeEvent> PlayerChangeRadioRange = new LuaEvent<PlayerChangeRadioRangeEvent>();
        public readonly LuaEvent<PlayerChangeSpectatorEvent> PlayerChangeSpectator = new LuaEvent<PlayerChangeSpectatorEvent>();
        public readonly LuaEvent<PlayerCloseGeneratorEvent> PlayerCloseGenerator = new LuaEvent<PlayerCloseGeneratorEvent>();
        public readonly LuaEvent<PlayerDamagedShootingTargetEvent> PlayerDamagedShootingTarget = new LuaEvent<PlayerDamagedShootingTargetEvent>();
        public readonly LuaEvent<PlayerDamagedWindowEvent> PlayerDamagedWindow = new LuaEvent<PlayerDamagedWindowEvent>();
        public readonly LuaEvent<PlayerDeactivatedGeneratorEvent> PlayerDeactivatedGenerator = new LuaEvent<PlayerDeactivatedGeneratorEvent>();
        public readonly LuaEvent<PlayerDropAmmoEvent> PlayerDropAmmo = new LuaEvent<PlayerDropAmmoEvent>();
        public readonly LuaEvent<PlayerDropItemEvent> PlayerDropItem = new LuaEvent<PlayerDropItemEvent>();
        public readonly LuaEvent<PlayerDryfireWeaponEvent> PlayerDryfireWeapon = new LuaEvent<PlayerDryfireWeaponEvent>();
        public readonly LuaEvent<PlayerEscapeEvent> PlayerEscape = new LuaEvent<PlayerEscapeEvent>();
        public readonly LuaEvent<PlayerHandcuffEvent> PlayerHandcuff = new LuaEvent<PlayerHandcuffEvent>();
        public readonly LuaEvent<PlayerRemoveHandcuffsEvent> PlayerRemoveHandcuffs = new LuaEvent<PlayerRemoveHandcuffsEvent>();
        public readonly LuaEvent<PlayerDamageEvent> PlayerDamage = new LuaEvent<PlayerDamageEvent>();
        public readonly LuaEvent<PlayerInteractElevatorEvent> PlayerInteractElevator = new LuaEvent<PlayerInteractElevatorEvent>();
        public readonly LuaEvent<PlayerInteractLockerEvent> PlayerInteractLocker = new LuaEvent<PlayerInteractLockerEvent>();
        public readonly LuaEvent<PlayerInteractScp330Event> PlayerInteractScp330 = new LuaEvent<PlayerInteractScp330Event>();
        public readonly LuaEvent<PlayerInteractShootingTargetEvent> PlayerInteractShootingTarget = new LuaEvent<PlayerInteractShootingTargetEvent>();
        public readonly LuaEvent<PlayerKickedEvent> PlayerKicked = new LuaEvent<PlayerKickedEvent>();
        public readonly LuaEvent<PlayerMakeNoiseEvent> PlayerMakeNoise = new LuaEvent<PlayerMakeNoiseEvent>();
        public readonly LuaEvent<PlayerOpenGeneratorEvent> PlayerOpenGenerator = new LuaEvent<PlayerOpenGeneratorEvent>();
        public readonly LuaEvent<PlayerPickupAmmoEvent> PlayerPickupAmmo = new LuaEvent<PlayerPickupAmmoEvent>();
        public readonly LuaEvent<PlayerPickupArmorEvent> PlayerPickupArmor = new LuaEvent<PlayerPickupArmorEvent>();
        public readonly LuaEvent<PlayerPickupScp330Event> PlayerPickupScp330 = new LuaEvent<PlayerPickupScp330Event>();
        public readonly LuaEvent<PlayerPreauthEvent> PlayerPreauth = new LuaEvent<PlayerPreauthEvent>();
        public readonly LuaEvent<PlayerReceiveEffectEvent> PlayerReceiveEffect = new LuaEvent<PlayerReceiveEffectEvent>();
        public readonly LuaEvent<PlayerReloadWeaponEvent> PlayerReloadWeapon = new LuaEvent<PlayerReloadWeaponEvent>();
        public readonly LuaEvent<PlayerChangeRoleEvent> PlayerChangeRole = new LuaEvent<PlayerChangeRoleEvent>();
        public readonly LuaEvent<PlayerSearchPickupEvent> PlayerSearchPickup = new LuaEvent<PlayerSearchPickupEvent>();
        public readonly LuaEvent<PlayerSearchedPickupEvent> PlayerSearchedPickup = new LuaEvent<PlayerSearchedPickupEvent>();
        public readonly LuaEvent<PlayerShotWeaponEvent> PlayerShotWeapon = new LuaEvent<PlayerShotWeaponEvent>();
        public readonly LuaEvent<PlayerSpawnEvent> PlayerSpawn = new LuaEvent<PlayerSpawnEvent>();
        public readonly LuaEvent<RagdollSpawnEvent> RagdollSpawn = new LuaEvent<RagdollSpawnEvent>();
        public readonly LuaEvent<PlayerThrowItemEvent> PlayerThrowItem = new LuaEvent<PlayerThrowItemEvent>();
        public readonly LuaEvent<PlayerToggleFlashlightEvent> PlayerToggleFlashlight = new LuaEvent<PlayerToggleFlashlightEvent>();
        public readonly LuaEvent<PlayerUnloadWeaponEvent> PlayerUnloadWeapon = new LuaEvent<PlayerUnloadWeaponEvent>();
        public readonly LuaEvent<PlayerUnlockGeneratorEvent> PlayerUnlockGenerator = new LuaEvent<PlayerUnlockGeneratorEvent>();
        public readonly LuaEvent<PlayerUsedItemEvent> PlayerUsedItem = new LuaEvent<PlayerUsedItemEvent>();
        public readonly LuaEvent<PlayerUseHotkeyEvent> PlayerUseHotkey = new LuaEvent<PlayerUseHotkeyEvent>();
        public readonly LuaEvent<PlayerUseItemEvent> PlayerUseItem = new LuaEvent<PlayerUseItemEvent>();
        public readonly LuaEvent<PlayerReportEvent> PlayerReport = new LuaEvent<PlayerReportEvent>();
        public readonly LuaEvent<PlayerCheaterReportEvent> PlayerCheaterReport = new LuaEvent<PlayerCheaterReportEvent>();
        public readonly LuaEvent<RoundEndEvent> RoundEnd = new LuaEvent<RoundEndEvent>();
        public readonly LuaEvent<RoundRestartEvent> RoundRestart = new LuaEvent<RoundRestartEvent>();
        public readonly LuaEvent<RoundStartEvent> RoundStart = new LuaEvent<RoundStartEvent>();
        public readonly LuaEvent<WaitingForPlayersEvent> WaitingForPlayers = new LuaEvent<WaitingForPlayersEvent>();
        public readonly LuaEvent<WarheadStartEvent> WarheadStart = new LuaEvent<WarheadStartEvent>();
        public readonly LuaEvent<WarheadStopEvent> WarheadStop = new LuaEvent<WarheadStopEvent>();
        public readonly LuaEvent<WarheadDetonationEvent> WarheadDetonation = new LuaEvent<WarheadDetonationEvent>();
        public readonly LuaEvent<PlayerMutedEvent> PlayerMuted = new LuaEvent<PlayerMutedEvent>();
        public readonly LuaEvent<PlayerUnmutedEvent> PlayerUnmuted = new LuaEvent<PlayerUnmutedEvent>();
        public readonly LuaEvent<PlayerCheckReservedSlotEvent> PlayerCheckReservedSlot = new LuaEvent<PlayerCheckReservedSlotEvent>();
        public readonly LuaEvent<RemoteAdminCommandEvent> RemoteAdminCommand = new LuaEvent<RemoteAdminCommandEvent>();
        public readonly LuaEvent<PlayerGameConsoleCommandEvent> PlayerGameConsoleCommand = new LuaEvent<PlayerGameConsoleCommandEvent>();
        public readonly LuaEvent<ConsoleCommandEvent> ConsoleCommand = new LuaEvent<ConsoleCommandEvent>();
        public readonly LuaEvent<TeamRespawnSelectedEvent> TeamRespawnSelected = new LuaEvent<TeamRespawnSelectedEvent>();
        public readonly LuaEvent<TeamRespawnEvent> TeamRespawn = new LuaEvent<TeamRespawnEvent>();
        public readonly LuaEvent<Scp106StalkingEvent> Scp106Stalking = new LuaEvent<Scp106StalkingEvent>();
        public readonly LuaEvent<PlayerEnterPocketDimensionEvent> PlayerEnterPocketDimension = new LuaEvent<PlayerEnterPocketDimensionEvent>();
        public readonly LuaEvent<PlayerExitPocketDimensionEvent> PlayerExitPocketDimension = new LuaEvent<PlayerExitPocketDimensionEvent>();
        public readonly LuaEvent<PlayerThrowProjectileEvent> PlayerThrowProjectile = new LuaEvent<PlayerThrowProjectileEvent>();
        public readonly LuaEvent<Scp914ActivateEvent> Scp914Activate = new LuaEvent<Scp914ActivateEvent>();
        public readonly LuaEvent<Scp914KnobChangeEvent> Scp914KnobChange = new LuaEvent<Scp914KnobChangeEvent>();
        public readonly LuaEvent<Scp914UpgradeInventoryEvent> Scp914UpgradeInventory = new LuaEvent<Scp914UpgradeInventoryEvent>();
        public readonly LuaEvent<Scp914UpgradePickupEvent> Scp914UpgradePickup = new LuaEvent<Scp914UpgradePickupEvent>();
        public readonly LuaEvent<Scp106TeleportPlayerEvent> Scp106TeleportPlayer = new LuaEvent<Scp106TeleportPlayerEvent>();
        public readonly LuaEvent<Scp173PlaySoundEvent> Scp173PlaySound = new LuaEvent<Scp173PlaySoundEvent>();
        public readonly LuaEvent<Scp173CreateTantrumEvent> Scp173CreateTantrum = new LuaEvent<Scp173CreateTantrumEvent>();
        public readonly LuaEvent<Scp173BreakneckSpeedsEvent> Scp173BreakneckSpeeds = new LuaEvent<Scp173BreakneckSpeedsEvent>();
        public readonly LuaEvent<Scp173NewObserverEvent> Scp173NewObserver = new LuaEvent<Scp173NewObserverEvent>();
        public readonly LuaEvent<Scp173SnapPlayerEvent> Scp173SnapPlayer = new LuaEvent<Scp173SnapPlayerEvent>();
        public readonly LuaEvent<Scp939CreateAmnesticCloudEvent> Scp939CreateAmnesticCloud = new LuaEvent<Scp939CreateAmnesticCloudEvent>();
        public readonly LuaEvent<Scp939LungeEvent> Scp939Lunge = new LuaEvent<Scp939LungeEvent>();
        public readonly LuaEvent<Scp939AttackEvent> Scp939Attack = new LuaEvent<Scp939AttackEvent>();
        public readonly LuaEvent<Scp079GainExperienceEvent> Scp079GainExperience = new LuaEvent<Scp079GainExperienceEvent>();
        public readonly LuaEvent<Scp079LevelUpTierEvent> Scp079LevelUpTier = new LuaEvent<Scp079LevelUpTierEvent>();
        public readonly LuaEvent<Scp079UseTeslaEvent> Scp079UseTesla = new LuaEvent<Scp079UseTeslaEvent>();
        public readonly LuaEvent<Scp079LockdownRoomEvent> Scp079LockdownRoom = new LuaEvent<Scp079LockdownRoomEvent>();
        public readonly LuaEvent<Scp079CancelRoomLockdownEvent> Scp079CancelRoomLockdown = new LuaEvent<Scp079CancelRoomLockdownEvent>();
        public readonly LuaEvent<Scp079LockDoorEvent> Scp079LockDoor = new LuaEvent<Scp079LockDoorEvent>();
        public readonly LuaEvent<Scp079UnlockDoorEvent> Scp079UnlockDoor = new LuaEvent<Scp079UnlockDoorEvent>();
        public readonly LuaEvent<Scp079BlackoutZoneEvent> Scp079BlackoutZone = new LuaEvent<Scp079BlackoutZoneEvent>();
        public readonly LuaEvent<Scp079BlackoutRoomEvent> Scp079BlackoutRoom = new LuaEvent<Scp079BlackoutRoomEvent>();
        public readonly LuaEvent<Scp049ResurrectBodyEvent> Scp049ResurrectBody = new LuaEvent<Scp049ResurrectBodyEvent>();
        public readonly LuaEvent<Scp049StartResurrectingBodyEvent> Scp049StartResurrectingBody = new LuaEvent<Scp049StartResurrectingBodyEvent>();
        public readonly LuaEvent<PlayerInteractDoorEvent> PlayerInteractDoor = new LuaEvent<PlayerInteractDoorEvent>();
        public readonly LuaEvent<BanIssuedEvent> BanIssued = new LuaEvent<BanIssuedEvent>();
        public readonly LuaEvent<BanRevokedEvent> BanRevoked = new LuaEvent<BanRevokedEvent>();
        public readonly LuaEvent<RemoteAdminCommandExecutedEvent> RemoteAdminCommandExecuted = new LuaEvent<RemoteAdminCommandExecutedEvent>();
        public readonly LuaEvent<PlayerGameConsoleCommandExecutedEvent> PlayerGameConsoleCommandExecuted = new LuaEvent<PlayerGameConsoleCommandExecutedEvent>();
        public readonly LuaEvent<ConsoleCommandExecutedEvent> ConsoleCommandExecuted = new LuaEvent<ConsoleCommandExecutedEvent>();
        public readonly LuaEvent<BanUpdatedEvent> BanUpdated = new LuaEvent<BanUpdatedEvent>();
        public readonly LuaEvent<PlayerPreCoinFlipEvent> PlayerPreCoinFlip = new LuaEvent<PlayerPreCoinFlipEvent>();
        public readonly LuaEvent<PlayerCoinFlipEvent> PlayerCoinFlip = new LuaEvent<PlayerCoinFlipEvent>();
        public readonly LuaEvent<PlayerInteractGeneratorEvent> PlayerInteractGenerator = new LuaEvent<PlayerInteractGeneratorEvent>();
        public readonly LuaEvent<RoundEndConditionsCheckEvent> RoundEndConditionsCheck = new LuaEvent<RoundEndConditionsCheckEvent>();
        public readonly LuaEvent<Scp914PickupUpgradedEvent> Scp914PickupUpgraded = new LuaEvent<Scp914PickupUpgradedEvent>();
        public readonly LuaEvent<Scp914InventoryItemUpgradedEvent> Scp914InventoryItemUpgraded = new LuaEvent<Scp914InventoryItemUpgradedEvent>();
        public readonly LuaEvent<Scp914ProcessPlayerEvent> Scp914ProcessPlayer = new LuaEvent<Scp914ProcessPlayerEvent>();
        public readonly LuaEvent<Scp079CameraChangedEvent> Scp079CameraChanged = new LuaEvent<Scp079CameraChangedEvent>();
        public readonly LuaEvent<Scp096AddingTargetEvent> Scp096AddingTarget = new LuaEvent<Scp096AddingTargetEvent>();
        public readonly LuaEvent<Scp096EnragingEvent> Scp096Enraging = new LuaEvent<Scp096EnragingEvent>();
        public readonly LuaEvent<Scp096ChangeStateEvent> Scp096ChangeState = new LuaEvent<Scp096ChangeStateEvent>();
        public readonly LuaEvent<Scp096ChargingEvent> Scp096Charging = new LuaEvent<Scp096ChargingEvent>();
        public readonly LuaEvent<Scp096PryingGateEvent> Scp096PryingGate = new LuaEvent<Scp096PryingGateEvent>();
        public readonly LuaEvent<Scp096TryNotCryEvent> Scp096TryNotCry = new LuaEvent<Scp096TryNotCryEvent>();
        public readonly LuaEvent<Scp096StartCryingEvent> Scp096StartCrying = new LuaEvent<Scp096StartCryingEvent>();
        public readonly LuaEvent<PlayerUsingRadioEvent> PlayerUsingRadio = new LuaEvent<PlayerUsingRadioEvent>();
        public readonly LuaEvent<CassieAnnouncesScpTerminationEvent> CassieAnnouncesScpTermination = new LuaEvent<CassieAnnouncesScpTerminationEvent>();
        public readonly LuaEvent<PlayerGetGroupEvent> PlayerGetGroup = new LuaEvent<PlayerGetGroupEvent>();
        public readonly LuaEvent<PlayerUsingIntercomEvent> PlayerUsingIntercom = new LuaEvent<PlayerUsingIntercomEvent>();
        public readonly LuaEvent<PlayerDeathEvent> PlayerDeath = new LuaEvent<PlayerDeathEvent>();
        public readonly LuaEvent<PlayerRadioToggleEvent> PlayerRadioToggle = new LuaEvent<PlayerRadioToggleEvent>();
        public readonly LuaEvent<PlayerDroppedItemEvent> PlayerDropedpItem = new LuaEvent<PlayerDroppedItemEvent>();
        public readonly LuaEvent<PlayerDroppedAmmoEvent> PlayerDroppedAmmo = new LuaEvent<PlayerDroppedAmmoEvent>();

        #endregion

        [MoonSharpHidden]
        public readonly Dictionary<ServerEventType, ILuaEvent> Events;

        public LuaEventManager()
        {
            var allTypes = Enum.GetValues(typeof(ServerEventType));
            Events = new Dictionary<ServerEventType, ILuaEvent>(allTypes.Length);

            try
            {
                Log.Raw($"<color=Blue>[LuaLab] Initializing lua events...</color>");
                int registered = 0;
                foreach (ServerEventType type in allTypes)
                {
                    FieldInfo info = this.GetType().GetField(type.ToString());
                    if (info != null)
                    {
                        Events.Add(type, (ILuaEvent)info.GetValue(this));
                        registered++;
                    }
                }

                Log.Raw($"<color=Blue>[LuaLab] Lua events setup! Registered {registered} events</color>");
            }
            catch (Exception e)
            {
                Log.Raw($"<color=Red>[LuaLab] Error initializing lua events: {e}</color>");
            }
        }


        [MoonSharpHidden]
        public void ClearHandlersForScript(Script script)
        {
            foreach (ILuaEvent luaEvent in Events.Values)
            {
                luaEvent.ClearHandlersForScript(script);
            }
        }
    }

    public static class EventManagerPatch
    {
        [HarmonyPrefix]
        public static void Postfix(ref IEventArguments args, ref bool __result)
        {
            if (Plugin.Instance.LuaEventManager.Events.TryGetValue(args.BaseType, out ILuaEvent luaEvent))
            {
                if (!luaEvent.Invoke(args))
                {
                    __result = false;
                }
            }
        }
    }
}