using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuaLab
{
    public class LuaPluginReloadManager
    {
        private readonly Dictionary<LuaPlugin, FileSystemWatcher> _hotReloads = new Dictionary<LuaPlugin, FileSystemWatcher>();
        private readonly Dictionary<LuaPlugin, FileSystemWatcher> _liveReloads = new Dictionary<LuaPlugin, FileSystemWatcher>();

        public LuaPluginReloadManager()
        {

        }

        /// <summary>
        /// Disables all of the both hot and live reloads
        /// </summary>
        public void DisableAllReloads()
        {
            DisableAllHotReloads();
            DisableAllLiveReloads();
        }

        /// <summary>
        /// Disables all live reloads
        /// </summary>
        public void DisableAllLiveReloads()
        {
            while (_liveReloads.Count > 0)
            {
                var kvp = _hotReloads.ElementAt(0);

                TryDisableLiveReload(kvp.Key);
            }
        }

        /// <summary>
        /// Disables all of the hot reloads
        /// </summary>
        public void DisableAllHotReloads()
        {
            while (_hotReloads.Count > 0)
            {
                var kvp = _hotReloads.ElementAt(0);

                TryDisableLiveReload(kvp.Key);
            }
        }

        /// <summary>
        /// Sets live reload state for specified plugin
        /// </summary>
        /// <param name="state"></param>
        /// <param name="plugin"></param>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// Sets hot reload state for specified plugin
        /// </summary>
        /// <param name="state"></param>
        /// <param name="plugin"></param>
        /// <exception cref="ArgumentException"></exception>
        public void SetHotReload(bool state, LuaPlugin plugin = null)
        {
            if (plugin == null)
            {
                throw new ArgumentException("Plugin cannot be null");
            }

            if (state)
            {
                TryEnableHotReload(plugin);
            }
            else
            {
                TryDisableHotReload(plugin);
            }
        }

        #region Methods

        private void TryEnableReloadService(LuaPlugin plugin, Dictionary<LuaPlugin, FileSystemWatcher> dict)
        {
            if (dict.ContainsKey(plugin))
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


            dict.Add(plugin, watcher);
        }

        private void TryDisableReloadService(LuaPlugin plugin, Dictionary<LuaPlugin, FileSystemWatcher> dict)
        {
            if (!dict.TryGetValue(plugin, out FileSystemWatcher watcher))
            {
                return;
            }

            watcher.Changed -= Watcher_Changed;
            watcher.Deleted -= Watcher_Deleted;
            watcher.Dispose();
            dict.Remove(plugin);
        }

        #endregion

        #region Implementations

        private void TryEnableLiveReload(LuaPlugin plugin)
        {
            TryEnableReloadService(plugin, _liveReloads);
        }

        private void TryDisableLiveReload(LuaPlugin plugin)
        {
            TryDisableReloadService(plugin, _liveReloads);
        }

        private void TryEnableHotReload(LuaPlugin plugin)
        {
            TryEnableReloadService(plugin, _hotReloads);
        }

        private void TryDisableHotReload(LuaPlugin plugin)
        {
            TryDisableReloadService(plugin, _hotReloads);
        }

        #endregion

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            LuaPlugin plugin = _hotReloads.Keys.FirstOrDefault(n => n.PluginPath == e.FullPath);
            if (plugin == null)
            {
                return;
            }

            plugin.HotReload();
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            LuaPlugin plugin = _hotReloads.Keys.FirstOrDefault(n => n.PluginPath == e.FullPath);
            if (plugin == null)
            {
                return;
            }

            plugin.Unload();
            _hotReloads.Remove(plugin);
        }
    }
}
