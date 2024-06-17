using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuaLab
{
    public class LuaLiveReloadManager
    {
        private readonly Dictionary<LuaPlugin, FileSystemWatcher> _plugins = new Dictionary<LuaPlugin, FileSystemWatcher>();

        public LuaLiveReloadManager()
        {

        }

        public void DisableAllLiveReloads()
        {
            while (_plugins.Count > 0)
            {
                var kvp = _plugins.ElementAt(0);

                TryDisableLiveReload(kvp.Key);
            }
        }

        public void SetLiveReload(bool state, LuaPlugin plugin = null)
        {
            if (plugin == null)
            {
                throw new ArgumentException("Plugin cannot be null");
            }

            if (state)
            {
                TryEnableLiveReload(plugin);
            }
            else
            {
                TryDisableLiveReload(plugin);
            }
        }

        private void TryEnableLiveReload(LuaPlugin plugin)
        {
            if (_plugins.ContainsKey(plugin))
            {
                return;
            }

            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = Path.GetDirectoryName(plugin.PluginPath);
            watcher.Filter = Path.GetFileName(plugin.PluginPath);

            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += Watcher_Changed;
            watcher.Deleted += Watcher_Deleted;

            watcher.EnableRaisingEvents = true;


            _plugins.Add(plugin, watcher);
        }

        private void TryDisableLiveReload(LuaPlugin plugin)
        {
            if (!_plugins.TryGetValue(plugin, out FileSystemWatcher watcher))
            {
                return;
            }

            watcher.Changed -= Watcher_Changed;
            watcher.Deleted -= Watcher_Deleted;
            watcher.Dispose();
            _plugins.Remove(plugin);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            LuaPlugin plugin = _plugins.Keys.FirstOrDefault(n => n.PluginPath == e.FullPath);
            if (plugin == null)
            {
                return;
            }

            plugin.Reload();
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            LuaPlugin plugin = _plugins.Keys.FirstOrDefault(n => n.PluginPath == e.FullPath);
            if (plugin == null)
            {
                return;
            }

            plugin.Unload();
            _plugins.Remove(plugin);
        }
    }
}
