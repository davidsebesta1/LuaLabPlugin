using MoonSharp.Interpreter;
using PluginAPI.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuaLab
{
    public class LuaPluginManager
    {
        private readonly Dictionary<string, LuaPlugin> _plugins = new Dictionary<string, LuaPlugin>();

        public LuaPluginManager()
        {
            LoadScriptsFromFolder(Path.Combine(Path.GetDirectoryName(PluginHandler.Get(Plugin.Instance).MainConfigPath), "LuaPlugins"));
        }

        public void UnloadAllPlugins()
        {
            while (_plugins.Count > 0)
            {
                var kvp = _plugins.ElementAt(0);

                UnloadPlugin(kvp.Key);
            }
        }

        public void UnloadPlugin(string name)
        {
            if (_plugins.TryGetValue(name, out LuaPlugin plugin))
            {
                plugin.Unload();
                _plugins.Remove(name);
            }
        }

        public bool ReloadLuaPlugin(string name)
        {
            if (_plugins.TryGetValue(name, out LuaPlugin plugin))
            {
                return plugin.Reload();
            }
            else
            {
                Log.Error($"Couldnt find {name} plugin");
            }

            return false;
        }

        public void PluginGlobalTableInsert(LuaPlugin plugin, Script script)
        {
            script.Globals["liveReload"] = (bool state) =>
            {
                Plugin.Instance.LuaLiveReloadManager.SetLiveReload(state, plugin);
            };
        }

        private void LoadScriptsFromFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string[] files = Directory.GetFiles(path);
            Log.Raw($"<color=Blue>[LuaLab] Loading {files.Length} lua plugins</color>");

            int loaded = 0;
            foreach (string file in files)
            {
                if (Path.GetExtension(file) == ".lua")
                {
                    string name = Path.GetFileNameWithoutExtension(file);
                    Log.Raw($"<color=Blue>[LuaLab] Loading {name}.lua ...</color>");

                    LuaPlugin plugin = new LuaPlugin(name, file);
                    Script script = Plugin.Instance.LuaScriptManager.CreateScript(ReferenceHub.HostHub, LuaOutputType.ServerConsole, plugin);

                    PluginGlobalTableInsert(plugin, script);

                    bool loadSuccess = RunScriptCodeFromPath(script, file);
                    loaded += Convert.ToInt32(loadSuccess);
                    _plugins.Add(name, plugin);
                }
            }

            Log.Raw($"<color=Blue>[LuaLab] Loaded {loaded}/{files.Length} lua plugins</color>");
        }

        public bool RunScriptCodeFromPath(Script script, string path)
        {
            using (Stream stream = File.OpenRead(path))
            {
                try
                {
                    script.DoStream(stream);
                    return true;
                }
                catch (Exception e)
                {
                    Log.Raw($"<color=Red>[LuaLab] Error at loading {Path.GetFileName(path)}: {e.Message}</color>");
                }

            }
            return false;
        }
    }
}