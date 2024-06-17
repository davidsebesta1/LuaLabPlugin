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
        public LuaEvent<MapGeneratedEvent> PluginReloading = new LuaEvent<MapGeneratedEvent>();

        [MoonSharpVisible(true)]
        public LuaEvent<MapGeneratedEvent> PluginReloaded = new LuaEvent<MapGeneratedEvent>();

        [MoonSharpVisible(true)]
        public LuaEvent<MapGeneratedEvent> PluginUnloading = new LuaEvent<MapGeneratedEvent>();

        [MoonSharpVisible(true)]
        public string Name;

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
        public void Unload()
        {
            try
            {
                PluginUnloading?.Invoke(null);
                UnloadCurrentScript();
            }
            catch (Exception e)
            {
                Log.Raw($"<color=Red>[LuaLab] Error at unloading {Name}: {e}</color>");
            }
        }

        private void UnloadCurrentScript()
        {
            if (PluginReloading != null)
            {
                PluginReloading.ClearHandlersForScript(Script);
            }

            if (PluginReloaded != null)
            {
                PluginReloaded.ClearHandlersForScript(Script);
            }

            if (PluginUnloading != null)
            {
                PluginUnloading.ClearHandlersForScript(Script);
            }

            Plugin.Instance.LuaEventManager.ClearHandlersForScript(Script);
        }

        [MoonSharpVisible(true)]
        public bool Reload()
        {
            bool res = false;
            try
            {
                PluginReloading?.Invoke(null);
                UnloadCurrentScript();

                Script = Plugin.Instance.LuaScriptManager.CreateScript(ReferenceHub.HostHub, LuaOutputType.ServerConsole, this);
                Plugin.Instance.LuaPluginManager.PluginGlobalTableInsert(this, Script);
                res = Plugin.Instance.LuaPluginManager.RunScriptCodeFromPath(Script, PluginPath);

                PluginReloaded?.Invoke(null);
            }
            catch (Exception e)
            {
                Log.Raw($"<color=Red>[LuaLab] Error at reloading {Name}: {e}</color>");
            }
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
