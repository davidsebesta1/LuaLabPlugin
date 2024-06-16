using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Ammo;
using InventorySystem.Items.Pickups;
using LuaLab.Helpers;
using MapGeneration;
using MoonSharp.Interpreter;
using PlayerRoles;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.IO;
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
            catch (Exception ex)
            {
                Log.Error(ex.Message, $"Lua Error ({executor.nicknameSync.DisplayName})");
                executor.queryProcessor._sender.RaReply(ex.Message, true, true, "Lua Error");
            }
        }

        public Script CreateScript(ReferenceHub owner, LuaOutputType outputType, LuaPlugin pluginObject = null)
        {
            Script script = new Script(CoreModules.Preset_HardSandbox);
            if (pluginObject != null)
            {
                pluginObject.Script = script;
                script.Globals["plugin"] = pluginObject;
            }

            //Global handlers
            script.Globals["players"] = Plugin.Instance.LuaPlayerManager;
            script.Globals["events"] = Plugin.Instance.LuaEventManager;
            script.Globals["round"] = Plugin.Instance.LuaRoundManager;
            script.Globals["server"] = Plugin.Instance.LuaServerManager;


            //Enums
            script.Globals["roleTypes"] = UserData.CreateStatic<RoleTypeId>();
            script.Globals["roleChangeReason"] = UserData.CreateStatic<RoleChangeReason>();
            script.Globals["roleSpawnFlags"] = UserData.CreateStatic<RoleSpawnFlags>();
            script.Globals["itemType"] = UserData.CreateStatic<ItemType>();
            script.Globals["itemCategory"] = UserData.CreateStatic<ItemCategory>();
            script.Globals["itemTierFlags"] = UserData.CreateStatic<ItemTierFlags>();
            script.Globals["roomShapes"] = UserData.CreateStatic<RoomShape>();
            script.Globals["roomNames"] = UserData.CreateStatic<RoomName>();
            script.Globals["facilityZones"] = UserData.CreateStatic<FacilityZone>();
            script.Globals["doorLockReasons"] = UserData.CreateStatic<DoorLockReason>();
            script.Globals["keycardPermissions"] = UserData.CreateStatic<KeycardPermissions>();
            script.Globals["firearmStatusFlags"] = UserData.CreateStatic<FirearmStatusFlags>();

            //Global functions

            //Items
            script.Globals["spawnItem"] = (Func<Vector3, ItemType, ItemPickupBase>)ItemHelpers.SpawnItem;
            script.Globals["spawnFirearm"] = (Func<Vector3, ItemType, byte, FirearmStatusFlags, uint, FirearmPickup>)ItemHelpers.SpawnGun;
            script.Globals["spawnAmmo"] = (Func<Vector3, ItemType, ushort, AmmoPickup>)ItemHelpers.SpawnAmmo;

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
