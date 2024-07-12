using HarmonyLib;
using LuaLab.ObjectsWrappers.Command;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;
using RemoteAdmin;
using SecretLuaLaboratoryPlugin.Objects.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using static RemoteAdmin.QueryProcessor;

namespace LuaLab.ObjectsWrappers.Managers
{
    [MoonSharpUserData]
    public class LuaCommandsManager
    {
        #region Properties

        [MoonSharpHidden]
        internal Dictionary<string[], Func<CommandArguments, bool>> RACommands = new Dictionary<string[], Func<CommandArguments, bool>>();
        internal Dictionary<string[], Func<CommandArguments, bool>> DotCommands = new Dictionary<string[], Func<CommandArguments, bool>>();

        [MoonSharpHidden]
        internal List<CommandData> LuaRACmdData = new List<CommandData>();

        [MoonSharpHidden]
        public CommandData[] NewRACmdsCached
        {
            get
            {
                if (_newRACmdsCached == null)
                {
                    RecacheCmds();
                }

                return _newRACmdsCached;
            }
        }

        [MoonSharpHidden]
        private CommandData[] _newRACmdsCached;

        #endregion

        #region Command Registration

        [MoonSharpVisible(true)]
        public bool RegisterCommand(string name, DynValue function, string[] aliases = null, string description = null)
        {
            try
            {
                if (string.IsNullOrEmpty(name) || RACommands.Any(n => n.Key.Contains(name)))
                {
                    throw new ArgumentException("Command name cannot be null or whitespace!");
                }

                if (aliases != null && aliases.Any(n => RACommands.Any(m => m.Key.Contains(n))))
                {
                    throw new ArgumentException("Command with same alias already exists");
                }

                if (function == null)
                {
                    throw new ArgumentNullException("Unable to register command. Provided function is null!");
                }

                if (function.Type != DataType.Function)
                {
                    throw new ArgumentException("Unable to register command. Provided value is not a function!");
                }

                Func<CommandArguments, bool> handler = (args) =>
                {
                    DynValue luaArgs = DynValue.FromObject(null, args);

                    DynValue res = function.Function.Call(luaArgs);
                    return res.Boolean;
                };

                if (name[0] == '.')
                {
                    RegisterDotCommand(name.Substring(1, name.Length - 1), handler, aliases);
                }
                else
                {
                    RegisterRACommand(name, handler, aliases, description);
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Error($"Unable to register command. {e}");
            }

            return false;
        }

        [MoonSharpHidden]
        private bool RegisterRACommand(string name, Func<CommandArguments, bool> handler, string[] aliases = null, string description = null)
        {
            try
            {
                List<string> toAdd = new List<string>(aliases ?? Array.Empty<string>()) { name };
                RACommands.Add(toAdd.ToArray(), handler);

                foreach (string cmd in toAdd)
                {
                    LuaRACmdData.Add(new CommandData()
                    {
                        Command = cmd,
                        Description = description,
                        AliasOf = cmd != name ? name : null,
                    });
                }

                RecacheCmds();
                RefreshCommandsDataForAllPlayers();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Error at registering RA command: {ex}");
                return false;
            }
        }

        [MoonSharpHidden]
        private bool RegisterDotCommand(string name, Func<CommandArguments, bool> handler, string[] aliases = null)
        {
            try
            {
                List<string> toAdd = new List<string>(aliases ?? Array.Empty<string>()) { name };
                DotCommands.Add(toAdd.ToArray(), handler);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Error at registering dot command: {ex}");
                return false;
            }
        }

        [MoonSharpVisible(true)]
        public bool UnregisterCommand(string name)
        {
            if (name[0] == '.')
            {
                return UnregisterDotCommand(name.Substring(1, name.Length - 1));
            }
            else
            {
                return UnregisterRACommand(name);
            }
        }

        [MoonSharpHidden]
        public bool UnregisterRACommand(string name)
        {
            var kvp = RACommands.FirstOrDefault(n => n.Key.Contains(name));

            if (!kvp.Equals(default(KeyValuePair<string[], Func<CommandArguments, bool>>)))
            {
                RACommands.Remove(kvp.Key);

                CommandData thisCmd = LuaRACmdData.First(n => n.Command == name);

                string baseCmdName = !string.IsNullOrEmpty(thisCmd.AliasOf) ? thisCmd.AliasOf : name;
                foreach (CommandData data in LuaRACmdData.Where(n => n.Command == baseCmdName || n.AliasOf == baseCmdName).ToArray())
                {
                    LuaRACmdData.Remove(data);
                }

                RecacheCmds();
                RefreshCommandsDataForAllPlayers();
                return true;
            }

            return false;
        }

        [MoonSharpHidden]
        private bool UnregisterDotCommand(string name)
        {
            var kvp = DotCommands.FirstOrDefault(n => n.Key.Contains(name));

            if (!kvp.Equals(default(KeyValuePair<string[], Func<CommandArguments, bool>>)))
            {
                DotCommands.Remove(kvp.Key);
                return true;
            }

            return false;
        }

        #endregion

        #region Command execution

        [MoonSharpVisible(true)]
        public void ExecuteRACommand(string name, ref CommandArguments args)
        {
            try
            {
                var kvp = RACommands.FirstOrDefault(n => n.Key.Contains(name));

                if (!kvp.Equals(default(KeyValuePair<string[], Func<CommandArguments, bool>>)))
                {
                    if (args.Sender.Hub != null)
                    {
                        args.Sender.Hub.queryProcessor._sender.RaReply("Unknown Command!", false, true, "Lua");
                    }
                    else
                    {
                        Log.Info("Attempting to execute unknown command", "Lua");
                    }
                    return;
                }

                ExecuteRACommand(kvp.Value, ref args);
            }

            catch (Exception ex)
            {
                string text = "Command execution failed! Error: " + Misc.RemoveStacktraceZeroes(ex.ToString());

                if (args.Sender != null)
                {
                    args.Sender.Hub.queryProcessor._sender.RaReply(text, success: false, logToConsole: true, name.ToUpperInvariant() + "#" + text);
                }
                else
                {
                    Log.Error(text, "Lua");
                }
            }
        }

        [MoonSharpHidden]
        internal void ExecuteRACommand(Func<CommandArguments, bool> cmd, ref CommandArguments args)
        {
            try
            {
                bool res = cmd.Invoke(args);
                if (args.Sender.Hub != null)
                {
                    args.Sender.Hub.queryProcessor._sender.RaReply(args.Response, res, true, "Lua");
                }
                else
                {
                    Log.Info($"Lua Cmd response: {args.Response}", "Lua");
                }
            }
            catch (InterpreterException ex)
            {
                if (args.Sender.Hub != null)
                {
                    args.Sender.Hub.queryProcessor._sender.RaReply(ex.DecoratedMessage, false, true, "Lua");
                }
                else
                {
                    Log.Info($"Lua Cmd Error: {ex.DecoratedMessage}", "Lua");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Lua hard error: {ex}", "Lua");
            }
        }

        [MoonSharpHidden]
        internal void ExecuteDotCommand(Func<CommandArguments, bool> cmd, ref CommandArguments args)
        {
            try
            {
                bool res = cmd.Invoke(args);
                if (args.Sender.Hub != null)
                {
                    args.Sender.Hub.gameConsoleTransmission.SendToClient(args.Response, "green");
                }
                else
                {
                    Log.Info($"Lua Dot Cmd response: {args.Response}", "Lua");
                }
            }
            catch (InterpreterException ex)
            {
                if (args.Sender.Hub != null)
                {
                    args.Sender.Hub.gameConsoleTransmission.SendToClient($"[Lua] Interpreter error: {ex.DecoratedMessage}", "magenta");
                }
                else
                {
                    Log.Info($"Lua Dot Cmd Error: {ex.DecoratedMessage}", "Lua");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Lua hard error: {ex}", "Lua");
            }
        }

        #endregion

        #region Command caching

        [MoonSharpHidden]
        internal void RefreshCommandsDataForAllPlayers()
        {
            foreach (ReferenceHub hub in ReferenceHub.AllHubs.Where(n => n.authManager.InstanceMode == CentralAuth.ClientInstanceMode.ReadyClient))
            {
                hub.queryProcessor.TargetUpdateCommandList(NewRACmdsCached);
            }
        }

        [MoonSharpHidden]
        private void RecacheCmds()
        {
            _newRACmdsCached = QueryProcessor._commands.ToArray().AddRangeToArray(Plugin.Instance.LuaCommandsManager.LuaRACmdData.ToArray());
        }

        #endregion
    }

    [HarmonyPatch(typeof(QueryProcessor), "ProcessGameConsoleQuery")]
    public static class ExecGameConsoleCommandPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref QueryProcessor __instance, ref string query)
        {
            string[] split = query.Trim().Split(QueryProcessor.SpaceArray, 512, StringSplitOptions.RemoveEmptyEntries);

            string cmdName = split[0];
            string[] args = split.Skip(1).ToArray();

            var kvp = Plugin.Instance.LuaCommandsManager.DotCommands.FirstOrDefault(n => n.Key.Contains(cmdName));
            if (!kvp.Equals(default(KeyValuePair<string[], Func<CommandArguments, bool>>)))
            {
                CommandArguments cmdArgs;
                LuaPlayer plr = Plugin.Instance.LuaPlayerManager[__instance._hub];

                cmdArgs = new CommandArguments(plr, args);

                Plugin.Instance.LuaCommandsManager.ExecuteDotCommand(kvp.Value, ref cmdArgs);
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(QueryProcessor), "SyncCommandsToClient")]
    public static class RACommandsInitialSendPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref QueryProcessor __instance)
        {
            if (!__instance._commandsSynced)
            {
                __instance._commandsSynced = true;
                __instance.TargetUpdateCommandList(Plugin.Instance.LuaCommandsManager.NewRACmdsCached);
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(CommandProcessor), "ProcessQuery")]
    public static class CommandProcessorPatch
    {
        [HarmonyPrefix]
        public static bool Prefix(ref string q, ref CommandSender sender, ref string __result)
        {
            string[] split = q.Trim().Split(QueryProcessor.SpaceArray, 512, StringSplitOptions.RemoveEmptyEntries);

            string cmdName = split[0];
            string[] args = split.Skip(1).ToArray();

            var kvp = Plugin.Instance.LuaCommandsManager.RACommands.FirstOrDefault(n => n.Key.Contains(cmdName));

            if (!kvp.Equals(default(KeyValuePair<string[], Func<CommandArguments, bool>>)))
            {
                CommandArguments cmdArgs;
                LuaPlayer plr = null;
                if (sender is PlayerCommandSender playerCommandSender)
                {
                    plr = Plugin.Instance.LuaPlayerManager[playerCommandSender.ReferenceHub];
                }

                cmdArgs = new CommandArguments(plr, args);

                Plugin.Instance.LuaCommandsManager.ExecuteRACommand(kvp.Value, ref cmdArgs);

                __result = cmdArgs.Response;
                return false;
            }

            return true;
        }
    }
}

