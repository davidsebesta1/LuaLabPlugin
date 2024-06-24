using Hints;
using MapGeneration;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PlayerStatsSystem;
using PluginAPI.Core;
using SecretLuaLaboratoryPlugin.ObjectsWrappers.Player;
using System;
using System.Collections.Generic;
using UnityEngine;
using VoiceChat;
using static Broadcast;

namespace SecretLuaLaboratoryPlugin.Objects.Player
{
    [MoonSharpUserData]
    public class LuaPlayer : IEquatable<LuaPlayer>
    {
        [MoonSharpHidden]
        public ReferenceHub Hub;

        [MoonSharpVisible(true)]
        public LuaPlayerRole PlayerRole { get; private set; }

        [MoonSharpVisible(true)]
        public LuaPlayerInventory Inventory { get; private set; }

        [MoonSharpVisible(true)]
        public LuaPlayerEffects Effects { get; private set; }

        [MoonSharpVisible(true)]
        public string Username
        {
            get
            {
                if (!string.IsNullOrEmpty(Hub.nicknameSync.MyNick))
                {
                    return Hub.nicknameSync.MyNick;
                }

                return "(unknown nickname)";
            }
        }

        [MoonSharpVisible(true)]
        public string DisplayUsername
        {
            get
            {
                return Hub.nicknameSync.DisplayName;
            }
            set
            {
                Hub.nicknameSync.DisplayName = value;
            }
        }

        [MoonSharpVisible(true)]
        public bool IsOffline
        {
            get
            {
                return Hub.gameObject == null;
            }
        }

        [MoonSharpVisible(true)]
        public string UserId
        {
            get
            {
                if (Hub.isLocalPlayer)
                {
                    return Hub.authManager.UserId;
                }

                return "server@server";
            }
        }

        [MoonSharpVisible(true)]
        public float StaminaRemaining
        {
            get
            {
                return Hub.playerStats.GetModule<StaminaStat>().CurValue;
            }
            set
            {
                Hub.playerStats.GetModule<StaminaStat>().CurValue = value;
            }
        }

        [MoonSharpVisible(true)]
        public float Health
        {
            get
            {
                return Hub.playerStats.GetModule<HealthStat>().CurValue;
            }
            set
            {
                Hub.playerStats.GetModule<HealthStat>().CurValue = value;
            }
        }

        [MoonSharpVisible(true)]
        public float Shield
        {
            get
            {
                if(Hub.roleManager.CurrentRole.Team == PlayerRoles.Team.SCPs)
                {
                    return Hub.playerStats.GetModule<HumeShieldStat>().CurValue;
                }

                return Hub.playerStats.GetModule<AhpStat>().CurValue;
            }

            set
            {
                if (Hub.roleManager.CurrentRole.Team == PlayerRoles.Team.SCPs)
                {
                    Hub.playerStats.GetModule<HumeShieldStat>().CurValue= value;
                    return;
                }

                Hub.playerStats.GetModule<AhpStat>().CurValue = value;
            }
        }

        [MoonSharpVisible(true)]
        public int PlayerId
        {
            get
            {
                return Hub.PlayerId;
            }
        }

        [MoonSharpVisible(true)]
        public RoomIdentifier CurrentRoom
        {
            get
            {
                return RoomIdUtils.RoomAtPosition(Hub.gameObject.transform.position);
            }
        }

        [MoonSharpVisible(true)]
        public PluginAPI.Core.Zones.FacilityZone CurrentZone
        {
            get
            {
                return RoomIdUtils.RoomAtPosition(Hub.gameObject.transform.position).ApiRoom.Zone;
            }
        }

        [MoonSharpVisible(true)]
        public bool Muted
        {
            get
            {
                return VoiceChatMutes.QueryLocalMute(UserId, intercom: false);
            }
            set
            {
                VoiceChatMutes.IssueLocalMute(UserId, intercom: false);
            }
        }

        [MoonSharpVisible(true)]
        public bool IntercomMuted
        {
            get
            {
                return VoiceChatMutes.QueryLocalMute(UserId, intercom: true);
            }
            set
            {
                VoiceChatMutes.IssueLocalMute(UserId, intercom: true);
            }
        }

        [MoonSharpVisible(true)]
        public Vector3 Position
        {
            get
            {
                return Hub.gameObject.transform.position;
            }
            set
            {
                Hub.gameObject.transform.position = value;
            }
        }

        [MoonSharpVisible(true)]
        public float Rotation
        {
            get
            {
                return Hub.transform.rotation.eulerAngles.y;
            }
            set
            {
                Vector3 euler = Hub.transform.rotation.eulerAngles;
                Hub.transform.rotation = Quaternion.Euler(euler.x, value, euler.z);
            }
        }

        [MoonSharpVisible(true)]
        public void Kick(string reason)
        {
            BanPlayer.KickUser(Hub, reason);
        }

        [MoonSharpVisible(true)]
        public void Ban(string reason, long durationSeconds)
        {
            BanPlayer.BanUser(Hub, reason, durationSeconds);
        }

        [MoonSharpVisible(true)]
        public void Broadcast(string message, ushort duration, bool clearPrevious = false)
        {
            if (clearPrevious)
            {
                Server.Broadcast.TargetClearElements(Hub.characterClassManager.connectionToClient);
            }

            Server.Broadcast.TargetAddElement(Hub.characterClassManager.connectionToClient, message, duration, BroadcastFlags.Normal);
        }

        [MoonSharpVisible(true)]
        public void Hint(string message, float duration)
        {
            Hub.connectionToClient.Send(new HintMessage(new TextHint(message, new[] { new StringHintParameter(string.Empty) }, durationScalar: duration)));
        }


        [MoonSharpHidden]
        public LuaPlayer(ReferenceHub hub)
        {
            Hub = hub;
            PlayerRole = new LuaPlayerRole(this);
            Inventory = new LuaPlayerInventory(this);
            Effects = new LuaPlayerEffects(this);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LuaPlayer);
        }

        public bool Equals(LuaPlayer other)
        {
            return !(other is null) &&
                   EqualityComparer<ReferenceHub>.Default.Equals(Hub, other.Hub);
        }

        public override int GetHashCode()
        {
            return 1621941741 + EqualityComparer<ReferenceHub>.Default.GetHashCode(Hub);
        }

        public static bool operator ==(LuaPlayer left, LuaPlayer right)
        {
            return EqualityComparer<LuaPlayer>.Default.Equals(left, right);
        }

        public static bool operator !=(LuaPlayer left, LuaPlayer right)
        {
            return !(left == right);
        }
    }
}
