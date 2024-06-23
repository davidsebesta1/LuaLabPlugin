using LuaLab.ObjectsWrappers.Events;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using Log = PluginAPI.Core.Log;

namespace LuaLab
{
    [MoonSharpUserData]
    public class LuaPlugin : IEquatable<LuaPlugin>
    {
        [MoonSharpVisible(true)]
        public LuaEvent<MapGeneratedEvent> PluginLoaded = new LuaEvent<MapGeneratedEvent>();

        [MoonSharpVisible(true)]
        public LuaEvent<MapGeneratedEvent> PluginReloading = new LuaEvent<MapGeneratedEvent>();

        [MoonSharpVisible(true)]
        public LuaEvent<MapGeneratedEvent> PluginReloaded = new LuaEvent<MapGeneratedEvent>();

        [MoonSharpVisible(true)]
        public LuaEvent<MapGeneratedEvent> PluginUnloading = new LuaEvent<MapGeneratedEvent>();

        [MoonSharpVisible(true)]
        public string Name { get; private set; }

        [MoonSharpVisible(true)]
        public string PluginPath;

        [MoonSharpHidden]
        public Script Script;

        [MoonSharpHidden]
        public LuaPlugin(string name, Script script, string pluginPath)
        {
            Name = name;
            Script = script;
            PluginPath = pluginPath;
        }

        [MoonSharpHidden]
        public LuaPlugin(string name, string pluginPath)
        {
            Name = name;
            PluginPath = pluginPath;
        }

        [MoonSharpVisible(true)]
        public bool Load()
        {
            bool loadSuccess = false;
            try
            {
                Script script = Plugin.Instance.LuaScriptManager.CreateScript(ReferenceHub.HostHub, LuaOutputType.ServerConsole, this);
                Plugin.Instance.LuaPluginManager.PluginGlobalTableInsert(this, script);
                loadSuccess = Plugin.Instance.LuaPluginManager.RunScriptCodeFromPath(script, PluginPath);
                PluginLoaded?.Invoke(null);
            }
            catch (Exception e)
            {
                Log.Raw($"<color=Red>[LuaLab] Error at loading {Name}: {e}</color>");
            }
            return loadSuccess;
        }

        [MoonSharpVisible(true)]
        public void Unload()
        {
            try
            {
                PluginUnloading?.Invoke(null);
                UnloadCurrentScriptEvents();
            }
            catch (Exception e)
            {
                Log.Raw($"<color=Red>[LuaLab] Error at unloading {Name}: {e}</color>");
            }
        }

        [MoonSharpVisible(false)]
        private void UnloadCurrentScriptEvents()
        {
            PluginLoaded?.ClearHandlersForScript(Script);
            PluginReloading?.ClearHandlersForScript(Script);
            PluginReloaded?.ClearHandlersForScript(Script);
            PluginUnloading?.ClearHandlersForScript(Script);

            Plugin.Instance.LuaEventManager.ClearHandlersForScript(Script);
        }

        [MoonSharpVisible(true)]
        public bool HotReload()
        {
            Log.Info($"Hot Reloading {Name}");
            bool res = false;
            try
            {
                PluginReloading?.Invoke(null);
                UnloadCurrentScriptEvents();

                res = Plugin.Instance.LuaPluginManager.RunScriptCodeFromPath(Script, PluginPath);

                PluginReloaded?.Invoke(null);
            }
            catch (Exception e)
            {
                Log.Raw($"<color=Red>[LuaLab] Error at reloading {Name}: {e}</color>");
            }

            Log.Info($"Hot Reloaded {Name}");
            return res;
        }

        [MoonSharpVisible(true)]
        public bool LiveReload()
        {
            Log.Info($"Live Reloading {Name}");
            bool res = false;
            try
            {
                PluginReloading?.Invoke(null);
                UnloadCurrentScriptEvents();

                Script = Plugin.Instance.LuaScriptManager.CreateScript(ReferenceHub.HostHub, LuaOutputType.ServerConsole, this);
                Plugin.Instance.LuaPluginManager.PluginGlobalTableInsert(this, Script);
                res = Plugin.Instance.LuaPluginManager.RunScriptCodeFromPath(Script, PluginPath);

                PluginReloaded?.Invoke(null);
            }
            catch (Exception e)
            {
                Log.Raw($"<color=Red>[LuaLab] Error at reloading {Name}: {e}</color>");
            }

            Log.Info($"Live Reloaded {Name}");
            return res;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaPlugin);
        }

        public bool Equals(LuaPlugin other)
        {
            return other is not null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(LuaPlugin left, LuaPlugin right)
        {
            return EqualityComparer<LuaPlugin>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaPlugin left, LuaPlugin right)
        {
            return !(left == right);
        }
    }
}
