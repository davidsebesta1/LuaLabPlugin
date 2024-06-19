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
        private readonly string _pluginDirPath;

        public LuaPluginManager()
        {
            _pluginDirPath = Path.GetDirectoryName(PluginHandler.Get(Plugin.Instance).MainConfigPath);
            LoadScriptsFromFolder(Path.Combine(_pluginDirPath, "LuaPlugins"));
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
                return plugin.HotReload();
            }
            else
            {
                Log.Error($"Couldnt find {name} plugin");
            }

            return false;
        }

        public void PluginGlobalTableInsert(LuaPlugin plugin, Script script)
        {
            script.Globals["LiveReload"] = (bool state) =>
            {
                Plugin.Instance.LuaLiveReloadManager.SetLiveReload(state, plugin);
            };

            script.Globals["HotReload"] = (bool state) =>
            {
                Plugin.Instance.LuaLiveReloadManager.SetHotReload(state, plugin);
            };
        }

        private void LoadScriptsFromFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                Directory.CreateDirectory(Path.Combine(path, "deps"));
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
                    loaded += Convert.ToInt32(plugin.Load());
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