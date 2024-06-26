using HarmonyLib;
using LuaLab.ObjectsWrappers.Events;
using MoonSharp.Interpreter;
using PluginAPI.Core;
using PluginAPI.Enums;
using PluginAPI.Events;
using System;
using System.Collections.Generic;

namespace LuaLab.ObjectsWrappers.Managers
{
    public class LuaEventManager
    {
        [MoonSharpHidden]
        public readonly Dictionary<ServerEventType, ILuaEvent> Events;

        public LuaEventManager()
        {
            var allTypes = Enum.GetValues(typeof(ServerEventType));
            Events = new Dictionary<ServerEventType, ILuaEvent>(allTypes.Length);

            try
            {
                Log.Raw($"<color=Blue>[LuaLab] Initializing lua events...</color>");
                int registered = 0;

                foreach (ServerEventType type in allTypes)
                {
                    try
                    {
                        Type eventArgsType = typeof(IEventArguments).Assembly.GetType("PluginAPI.Events." + type + "Event", false, true);
                        Type luaEventType = typeof(LuaEvent<>).MakeGenericType(eventArgsType);

                        ILuaEvent instance = (ILuaEvent)Activator.CreateInstance(luaEventType);
                        Events.Add(type, instance);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    registered++;
                }

                // The typo causes type to not be found
                Events.Add(ServerEventType.PlayerDropedpItem, new LuaEvent<PlayerDroppedItemEvent>());
                registered++;

                Log.Raw($"<color=Blue>[LuaLab] Lua events setup! Registered {registered} events</color>");
            }
            catch (Exception e)
            {
                Log.Raw($"<color=Red>[LuaLab] Error initializing lua events: {e}</color>");
            }
        }

        [MoonSharpHidden]
        public void ClearHandlersForScript(Script script)
        {
            foreach (ILuaEvent luaEvent in Events.Values)
            {
                luaEvent.ClearHandlersForScript(script);
            }
        }
    }

    public static class EventManagerPatch
    {
        [HarmonyPostfix]
        public static void Postfix(ref IEventArguments args, ref bool __result)
        {
            try
            {
                if (Plugin.Instance.LuaEventManager.Events.TryGetValue(args.BaseType, out ILuaEvent luaEvent))
                {
                    if (!luaEvent.Invoke(args))
                    {
                        __result = false;
                    }
                }
            }
            catch (InterpreterException ex)
            {
                Log.Raw($"<color=Red>[Lua Error] {ex.DecoratedMessage}</color>");
            }
        }
    }
}