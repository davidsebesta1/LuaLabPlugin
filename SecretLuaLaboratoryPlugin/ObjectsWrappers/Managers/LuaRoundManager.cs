using CommandSystem.Commands.RemoteAdmin.ServerEvent;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PluginAPI.Core;
using Respawning;

namespace LuaLab.ObjectsWrappers.Managers
{
    [MoonSharpUserData]
    public class LuaRoundManager
    {
        public LuaRoundManager()
        {

        }

        [MoonSharpVisible(true)]
        public bool IsStarted
        {
            get
            {
                return Round.IsRoundStarted;
            }
        }

        [MoonSharpVisible(true)]
        public bool IsEnded
        {
            get
            {
                return Round.IsRoundEnded;
            }
        }

        [MoonSharpVisible(true)]
        public bool IsLocked
        {
            get
            {
                return Round.IsLocked;
            }
            set
            {
                Round.IsLocked = value;
            }
        }

        [MoonSharpVisible(true)]
        public bool IsLobbyLocked
        {
            get
            {
                return Round.IsLobbyLocked;
            }
            set
            {
                Round.IsLobbyLocked = value;
            }
        }

        [MoonSharpVisible(true)]
        public long DurationSeconds
        {
            get
            {
                return (long)Round.Duration.TotalSeconds;
            }
        }

        [MoonSharpVisible(true)]
        public void Start()
        {
            Round.Start();
        }

        [MoonSharpVisible(true)]
        public void End()
        {
            Round.End();
        }

        [MoonSharpVisible(true)]
        public void Restart(bool fast = false)
        {
            Round.Restart(fast);
        }

        [MoonSharpVisible(true)]
        public void RespawnWave(SpawnableTeamType team)
        {
            RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, team);
            RespawnManager.Singleton.ForceSpawnTeam(team);
        }

        [MoonSharpVisible(true)]
        public void InstantRespawnWave(SpawnableTeamType team)
        {
            RespawnManager.Singleton.ForceSpawnTeam(team);
        }

    }
}