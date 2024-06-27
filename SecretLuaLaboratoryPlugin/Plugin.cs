using CustomPlayerEffects;
using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Ammo;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using LuaLab.Helpers.Descriptors;
using LuaLab.ObjectsWrappers.DamageHandlers;
using LuaLab.ObjectsWrappers.Effects;
using LuaLab.ObjectsWrappers.Facility;
using LuaLab.ObjectsWrappers.Items;
using LuaLab.ObjectsWrappers.Items.Pickups;
using LuaLab.ObjectsWrappers.Managers;
using MapGeneration;
using MapGeneration.Distributors;
using MoonSharp.Interpreter;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp096;
using PlayerRoles.Voice;
using PlayerStatsSystem;
using PluginAPI.Core.Attributes;
using PluginAPI.Core.Doors;
using PluginAPI.Enums;
using PluginAPI.Events;
using SecretLuaLaboratoryPlugin.ObjectsWrappers.Managers;
using System;
using System.Linq;
using System.Reflection;
using static BanHandler;
using static CustomPlayerEffects.StatusEffectBase;

namespace LuaLab
{
    public class Plugin
    {
        public static Plugin Instance { get; private set; }

        [PluginConfig]
        public Config Config;

        public LuaScriptManager LuaScriptManager { get; private set; }
        public LuaPluginReloadManager LuaLiveReloadManager { get; private set; }

        public LuaEventManager LuaEventManager { get; private set; }
        public LuaPlayerManager LuaPlayerManager { get; private set; }
        public LuaRoundManager LuaRoundManager { get; private set; }
        public LuaServerManager LuaServerManager { get; private set; }
        public LuaPluginManager LuaPluginManager { get; private set; }
        public LuaFacilityManager LuaFacilityManager { get; private set; }
        public LuaCassie LuaCassie { get; private set; }

        private Harmony _harmony;

        [PluginEntryPoint("Lua Lab", "0.9.0", "Lua in SL", "davidsebesta")]
        [PluginPriority(LoadPriority.Highest)]
        public void Start()
        {
            if (!Config.IsEnabled)
            {
                return;
            }

            Instance = this;

            _harmony = new Harmony("com.davidsebesta.lualab");
            MethodInfo methodInfo = typeof(EventManager).GetMethods(BindingFlags.Public | BindingFlags.Static)
                                   .Where(m => m.Name == "ExecuteEvent" &&
                                               m.GetParameters().Length == 1 &&
                                               m.GetParameters()[0].ParameterType == typeof(IEventArguments)).FirstOrDefault();

            MethodInfo postfixInfo = typeof(EventManagerPatch).GetMethod("Postfix");
            _harmony.Patch(methodInfo, postfix: new HarmonyMethod(postfixInfo));

            RegisterAllUserData();
            LuaCassie = new LuaCassie();

            LuaLiveReloadManager = new LuaPluginReloadManager();

            LuaEventManager = new LuaEventManager();
            LuaPlayerManager = new LuaPlayerManager();
            LuaRoundManager = new LuaRoundManager();
            LuaServerManager = new LuaServerManager();
            LuaFacilityManager = new LuaFacilityManager();

            LuaScriptManager = new LuaScriptManager();
            LuaPluginManager = new LuaPluginManager();
            LuaPluginManager.Init();

            EventManager.RegisterEvents(this);
            EventManager.RegisterEvents(LuaPlayerManager);
            EventManager.RegisterEvents(LuaEventManager);
            EventManager.RegisterEvents(LuaScriptManager);
            EventManager.RegisterEvents(LuaPluginManager);
        }

        [PluginUnload]
        public void Unloading()
        {
            LuaLiveReloadManager.DisableAllLiveReloads();
            LuaPluginManager.UnloadAllPlugins();
        }

        private void RegisterAllUserData()
        {
            UserData.RegisterAssembly();

            //Register SL event args
            UserData.RegisterType<ServerEventType>();

            var slEvents = typeof(IEventArguments).Assembly.GetTypes().Where(n => n.GetInterface("IEventArguments") != null && !n.IsAbstract);

            MethodInfo methodInfo = typeof(UserData).GetMethod("RegisterType", [typeof(Type), typeof(InteropAccessMode), typeof(string)]);
            foreach (Type eventType in slEvents)
            {
                methodInfo.Invoke(null, [eventType, InteropAccessMode.Default, null]);
            }

            UserData.RegisterType<Script>();

            UserData.RegisterType<EventArgs>();
            UserData.RegisterType<LuaEventManager>(new CustomUserDataDescriptor());

            UserData.RegisterType<Team>();
            UserData.RegisterType<BanType>();
            UserData.RegisterType<Scp096RageState>();
            UserData.RegisterType<IntercomState>();


            //Roles and items
            UserData.RegisterType<RoleTypeId>();
            UserData.RegisterType<RoleChangeReason>();
            UserData.RegisterType<RoleSpawnFlags>();
            UserData.RegisterType<ItemType>();
            UserData.RegisterType<ItemCategory>();
            UserData.RegisterType<ItemTierFlags>();

            //Effects
            UserData.RegisterType<EffectClassification>();

            UserData.RegisterProxyType<LuaStatusEffect, StatusEffectBase>(effect => new LuaStatusEffect(effect));

            //Facility
            UserData.RegisterType<RoomShape>();
            UserData.RegisterType<RoomName>();
            UserData.RegisterType<FacilityZone>();
            UserData.RegisterProxyType<LuaFacilityRoom, RoomIdentifier>(room => new LuaFacilityRoom(room));
            UserData.RegisterProxyType<LuaFacilityZone, PluginAPI.Core.Zones.FacilityZone>(room => new LuaFacilityZone(room));
            UserData.RegisterType<DoorType>();
            UserData.RegisterProxyType<LuaTeslaGate, TeslaGate>(teslaGate => new LuaTeslaGate(teslaGate));
            UserData.RegisterProxyType<LuaGenerator, Scp079Generator>(generator => new LuaGenerator(generator));

            //Doors
            UserData.RegisterProxyType<LuaDoor, FacilityDoor>(room => new LuaDoor(room));
            UserData.RegisterType<DoorLockReason>();
            UserData.RegisterType<KeycardPermissions>();

            //Items
            UserData.RegisterProxyType<LuaBaseItem, ItemBase>(item => new LuaBaseItem(item));

            //Firearms
            UserData.RegisterType<FirearmStatusFlags>();
            UserData.RegisterProxyType<LuaFirearmItem, Firearm>(firearm => new LuaFirearmItem(firearm));

            //Pickups
            UserData.RegisterProxyType<LuaExplosivePickup, TimedGrenadePickup>(grenadePickup => new LuaExplosivePickup(grenadePickup));
            UserData.RegisterProxyType<LuaAmmoItemPickup, AmmoPickup>(ammoPickup => new LuaAmmoItemPickup(ammoPickup));
            UserData.RegisterProxyType<LuaFirearmPickup, FirearmPickup>(firearmPickup => new LuaFirearmPickup(firearmPickup));
            UserData.RegisterProxyType<LuaItemPickup, ItemPickupBase>(pickup => new LuaItemPickup(pickup));

            //Damage handler(s)
            UserData.RegisterProxyType<LuaStandardDamageHandler, StandardDamageHandler>(pickup => new LuaStandardDamageHandler(pickup));
        }
    }
}