using MoonSharp.Interpreter;
using PluginAPI.Events;

namespace LuaLab.ObjectsWrappers.Events
{
    public interface ILuaEvent
    {
        void Add(DynValue function);
        void ClearHandlersForScript(Script script);
        bool Invoke(IEventArguments eventArgs);
        void Remove(DynValue function);
    }
}