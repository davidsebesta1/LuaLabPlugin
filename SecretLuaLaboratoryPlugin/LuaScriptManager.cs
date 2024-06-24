using Footprinting;
using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Ammo;
using InventorySystem.Items.Pickups;
using LuaLab.Helpers;
using LuaLab.ObjectsWrappers.Facility;
using MapGeneration;
using MoonSharp.Interpreter;
using PlayerRoles;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LuaLab
{
    public class LuaScriptManager
    {
        private readonly Dictionary<ReferenceHub, Script> _scriptsByCreators = new Dictionary<ReferenceHub, Script>();

        public LuaScriptManager()
        {

        }

        public void ExecuteLuaInGame(ReferenceHub executor, string code, LuaOutputType outputType)
        {
            if (!_scriptsByCreators.TryGetValue(executor, out Script script))
            {
                script = CreateScript(executor, outputType);
                _scriptsByCreators.Add(executor, script);
            }

            try
            {
                script.DoString(code);
            }
            catch (InterpreterException ex)
            {
                Log.Error(ex.Message, $"Lua Interpreter Error ({executor.nicknameSync.DisplayName})");

                if (executor == ReferenceHub.HostHub)
                {
                    Log.Error($"[Lua Error] {ex}");
                }
                else
                {
                    executor.queryProcessor._sender.RaReply(ex.DecoratedMessage, true, true, "Lua Error");
                }
            }
            catch (Exception ex)
            {
                if (executor == ReferenceHub.HostHub)
                {
                    Log.Error($"[Lua Hard Error] {ex}");
                }
                else
                {
                    executor.queryProcessor._sender.RaReply(ex.Message, true, true, "Lua Hard Error");
                }
            }
        }

        public Script CreateScript(ReferenceHub owner, LuaOutputType outputType, LuaPlugin pluginObject = null)
        {
            Script script = new Script(CoreModules.Preset_HardSandbox);
            if (pluginObject != null)
            {
                pluginObject.Script = script;
                script.Globals["Plugin"] = pluginObject;
            }

            //Global handlers
            script.Globals["Players"] = Plugin.Instance.LuaPlayerManager;
            script.Globals["Events"] = Plugin.Instance.LuaEventManager;
            script.Globals["Round"] = Plugin.Instance.LuaRoundManager;
            script.Globals["Server"] = Plugin.Instance.LuaServerManager;
            script.Globals["Cassie"] = Plugin.Instance.LuaCassie;

            //Enums
            script.Globals["RoleTypes"] = UserData.CreateStatic<RoleTypeId>();
            script.Globals["RoleChangeReason"] = UserData.CreateStatic<RoleChangeReason>();
            script.Globals["RoleSpawnFlags"] = UserData.CreateStatic<RoleSpawnFlags>();
            script.Globals["ItemType"] = UserData.CreateStatic<ItemType>();
            script.Globals["ItemCategory"] = UserData.CreateStatic<ItemCategory>();
            script.Globals["ItemTierFlags"] = UserData.CreateStatic<ItemTierFlags>();
            script.Globals["RoomShapes"] = UserData.CreateStatic<RoomShape>();
            script.Globals["RoomNames"] = UserData.CreateStatic<RoomName>();
            script.Globals["FacilityZones"] = UserData.CreateStatic<FacilityZone>();
            script.Globals["DoorLockReasons"] = UserData.CreateStatic<DoorLockReason>();
            script.Globals["KeycardPermissions"] = UserData.CreateStatic<KeycardPermissions>();
            script.Globals["FirearmStatusFlags"] = UserData.CreateStatic<FirearmStatusFlags>();
            script.Globals["Team"] = UserData.CreateStatic<Team>();
            script.Globals["DoorType"] = UserData.CreateStatic<DoorType>();

            //Global functions

            //Items
            script.Globals["SpawnItem"] = (Func<Vector3, ItemType, ItemPickupBase>)ItemHelpers.SpawnItem;
            script.Globals["SpawnFirearm"] = (Func<Vector3, ItemType, byte, FirearmStatusFlags, uint, FirearmPickup>)ItemHelpers.SpawnGun;
            script.Globals["SpawnAmmo"] = (Func<Vector3, ItemType, ushort, AmmoPickup>)ItemHelpers.SpawnAmmo;

            //Vector3
            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Table, typeof(Vector3),
            dynVal =>
            {
                Table table = dynVal.Table;
                float x = (float)(double)table[1];
                float y = (float)(double)table[2];
                float z = (float)(double)table[3];
                return new Vector3(x, y, z);
            });

            Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Vector3>(
            (script, vector) =>
            {
                DynValue x = DynValue.NewNumber(vector.x);
                DynValue y = DynValue.NewNumber(vector.y);
                DynValue z = DynValue.NewNumber(vector.z);
                DynValue dynVal = DynValue.NewTable(script, [x, y, z]);
                return dynVal;
            });

            //Player
            Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Player>(
            (script, player) =>
            {
                DynValue dynVal = DynValue.FromObject(script, Plugin.Instance.LuaPlayerManager[player.ReferenceHub]);
                return dynVal;
            });

            //Color
            Script.GlobalOptions.CustomConverters.SetScriptToClrCustomConversion(DataType.Table, typeof(Color),
            dynVal =>
            {
                Table table = dynVal.Table;
                float x = (float)(double)table[1];
                float y = (float)(double)table[2];
                float z = (float)(double)table[3];
                return new Color(x, y, z);
            });

            Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Color>(
            (script, color) =>
            {
                DynValue r = DynValue.NewNumber(color.r);
                DynValue g = DynValue.NewNumber(color.g);
                DynValue b = DynValue.NewNumber(color.b);
                DynValue dynVal = DynValue.NewTable(script, [r, g, b]);
                return dynVal;
            });

            //Footprint
            Script.GlobalOptions.CustomConverters.SetClrToScriptCustomConversion<Footprint>(
            (script, footprint) =>
            {
                DynValue dynVal = DynValue.FromObject(script, Player.Get(footprint.Hub));
                return dynVal;
            });

            switch (outputType)
            {
                case LuaOutputType.ServerConsole:
                    script.Options.DebugPrint = message => { Log.Info(message, "Lua"); };
                    break;
                case LuaOutputType.PlayerConsole:
                    script.Options.DebugPrint = message =>
                    {
                        Log.Info(message, $"Lua ({owner.nicknameSync.DisplayName})");
                        owner.queryProcessor._sender.RaReply(message, true, true, "Lua");
                    };
                    break;
            }

            return script;
        }
    }

    public enum LuaOutputType : byte
    {
        ServerConsole = 0,
        PlayerConsole = 1
    }
}
