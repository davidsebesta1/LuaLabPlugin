using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PlayerRoles;
using PlayerStatsSystem;
using PluginAPI.Core;
using Respawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuaLab.ObjectsWrappers.Facility
{
    [MoonSharpUserData]
    public class LuaCassie
    {
        [MoonSharpVisible(true)]
        public bool IsSpeaking => NineTailedFoxAnnouncer.singleton.queue.Count != 0;

        [MoonSharpVisible(true)]
        public void Message(string message, bool isHeld = false, bool isNoisy = true, bool isSubtitles = false)
        {
           Cassie.Message(message, isHeld, isNoisy, isSubtitles);
        }

        [MoonSharpVisible(true)]
        public void GlitchyMessage(string message, float glitchChance, float jamChance)
        {
            Cassie.GlitchyMessage(message, glitchChance, jamChance);
        }

        [MoonSharpVisible(true)]
        public void Clear()
        {
            Cassie.Clear();
        }

        [MoonSharpVisible(true)]
        public string ConvertTeam(Team team, string unitName)
        {
            return Cassie.ConvertTeam(team, unitName);
        }

        [MoonSharpVisible(true)]
        public string ConvertNumber(int num)
        {
            return Cassie.ConvertNumber(num);
        }

        [MoonSharpVisible(true)]
        public bool IsValid(string word)
        {
            return Cassie.IsValid(word);
        }
    }
}
