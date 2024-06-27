using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;
using PluginAPI.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuaLab.ObjectsWrappers.Events
{
    [MoonSharpUserData]
    public class LuaEvent<T> : ILuaEvent where T : IEventArguments
    {
        [MoonSharpHidden]
        private readonly Dictionary<Closure, Func<IEventArguments, bool>> _functions = new Dictionary<Closure, Func<IEventArguments, bool>>();

        [MoonSharpHidden]
        private readonly Dictionary<Script, List<Func<IEventArguments, bool>>> _registeredFunctions = new Dictionary<Script, List<Func<IEventArguments, bool>>>();

        [MoonSharpVisible(true)]
        public void Add(DynValue function)
        {
            try
            {

                if (function.Type != DataType.Function)
                {
                    throw new ArgumentException("Unable to add function to event handler");
                }
                Func<IEventArguments, bool> handler = (args) =>
                {
                    DynValue luaArgs = DynValue.FromObject(null, args);

                    DynValue res = function.Function.Call(luaArgs);
                    return res.Boolean;
                };

                if (!_registeredFunctions.TryGetValue(function.Function.OwnerScript, out List<Func<IEventArguments, bool>> list))
                {
                    list = new List<Func<IEventArguments, bool>>();
                    _registeredFunctions.Add(function.Function.OwnerScript, list);
                }
                list.Add(handler);

                _functions.Add(function.Function, handler);
            }
            catch (Exception ex)
            {
                Log.Raw($"<color=Red>[LuaLab] Error at adding event handler: {ex.Message}</color>");
            }
        }

        [MoonSharpVisible(true)]
        public void Remove(DynValue function)
        {
            try
            {
                if (function.Type != DataType.Function)
                {
                    throw new ArgumentException("Unable to remove function from event handler");
                }

                if (_registeredFunctions.TryGetValue(function.Function.OwnerScript, out List<Func<IEventArguments, bool>> list))
                {
                    Func<IEventArguments, bool> handlerToRemove = _functions[function.Function];

                    if (handlerToRemove != null)
                    {
                        _functions.Remove(function.Function);
                        list.Remove(handlerToRemove);
                        if (list.Count == 0)
                        {
                            _registeredFunctions.Remove(function.Function.OwnerScript);
                            return;
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("This function isnt registered for this event");
                }
            }
            catch (Exception ex)
            {
                Log.Raw($"<color=Red>[LuaLab] Error at removing event handler: {ex.Message}</color>");
            }
        }

        [MoonSharpVisible(true)]
        public bool Invoke(IEventArguments eventArgs)
        {
            return _functions.Values.All(n => n.Invoke(eventArgs));
        }

        [MoonSharpHidden]
        public void ClearHandlersForScript(Script script)
        {
            if (_registeredFunctions.TryGetValue(script, out List<Func<IEventArguments, bool>> list))
            {
                while (list.Count > 0)
                {
                    Func<IEventArguments, bool> element = list[0];
                    list.Remove(element);
                    _functions.Remove(_functions.FirstOrDefault(n => n.Value == element).Key);
                }
            }
        }
    }
}
