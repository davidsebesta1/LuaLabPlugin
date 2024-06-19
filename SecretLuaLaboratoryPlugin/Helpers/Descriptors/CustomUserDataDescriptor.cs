using LuaLab.ObjectsWrappers.Events;
using LuaLab.ObjectsWrappers.Managers;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;
using PluginAPI.Enums;
using System;

namespace LuaLab.Helpers.Descriptors
{
    public class CustomUserDataDescriptor : StandardUserDataDescriptor
    {
        public CustomUserDataDescriptor() : base(typeof(LuaEventManager), InteropAccessMode.Default) { }

        public override DynValue Index(Script script, object obj, DynValue index, bool isDirectIndexing)
        {
            LuaEventManager eventManager = obj as LuaEventManager;
            if (!Enum.TryParse(index.String, out ServerEventType type))
            {
                return DynValue.Nil;
            }

            if (eventManager != null && eventManager.Events.TryGetValue(type, out ILuaEvent luaEvent))
            {
                return DynValue.FromObject(script, luaEvent);
            }

            return DynValue.Nil;
        }
    }
}