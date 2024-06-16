using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.IO;

namespace LuaLab
{
    [MoonSharpUserData]
    public class LuaPlugin : IEquatable<LuaPlugin>
    {
        [MoonSharpVisible(true)]
        public event EventHandler PluginReloading;

        [MoonSharpVisible(true)]
        public event EventHandler PluginReloaded;

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
        public bool Reload()
        {
            bool res = false;
            try
            {

                PluginReloading?.Invoke(this, null);
                foreach (Delegate d in PluginReloading.GetInvocationList())
                {
                    PluginReloading -= (EventHandler)d;
                }
                foreach (Delegate d in PluginReloaded.GetInvocationList())
                {
                    PluginReloaded -= (EventHandler)d;
                }

                Script = Plugin.Instance.LuaScriptManager.CreateScript(ReferenceHub.HostHub, LuaOutputType.ServerConsole, this);
                res = Plugin.Instance.LuaPluginManager.RunScriptCodeFromPath(Script, PluginPath);
                PluginReloaded?.Invoke(this, null);
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
                   Name == other.Name &&
                   EqualityComparer<Script>.Default.Equals(Script, other.Script);
        }

        public override int GetHashCode()
        {
            int hashCode = 475330100;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<Script>.Default.GetHashCode(Script);
            return hashCode;
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
