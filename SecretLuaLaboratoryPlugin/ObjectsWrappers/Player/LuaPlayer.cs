using CommandSystem.Commands.RemoteAdmin;
using Hints;
using InventorySystem;
using InventorySystem.Disarming;
using InventorySystem.Items.Firearms.Modules;
using LuaLab.Helpers;
using MapGeneration;
using Mirror;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;
using PlayerRoles;
using PlayerRoles.FirstPersonControl;
using PlayerStatsSystem;
using PluginAPI.Core;
using PluginAPI.Core.Items;
using SecretLuaLaboratoryPlugin.ObjectsWrappers.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Pool;
using Utils.Networking;
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
        public PlayerInfoArea InfoArea
        {
            get
            {
                return Hub.nicknameSync.Network_playerInfoToShow;
            }
            set
            {
                Hub.nicknameSync.Network_playerInfoToShow = value;
            }
        }

        [MoonSharpVisible(true)]
        public float InfoViewRange
        {
            get
            {
                return Hub.nicknameSync.NetworkViewRange;
            }
            set
            {
                Hub.nicknameSync.NetworkViewRange = value;
            }
        }

        [MoonSharpVisible(true)]
        public string CustomInfo
        {
            get
            {
                return Hub.nicknameSync.Network_customPlayerInfoString;
            }
            set
            {
                if (CustomInfoColorValidator.IsValid(value))
                {
                    Hub.nicknameSync.Network_customPlayerInfoString = value;
                    return;
                }

                Log.Error($"Invalid color type for custom info of player {Hub.nicknameSync.MyNick}");
            }
        }

        [MoonSharpVisible(true)]
        public Table SessionVariables;

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
                if (Hub.roleManager.CurrentRole.Team == PlayerRoles.Team.SCPs)
                {
                    return Hub.playerStats.GetModule<HumeShieldStat>().CurValue;
                }

                return Hub.playerStats.GetModule<AhpStat>().CurValue;
            }

            set
            {
                if (Hub.roleManager.CurrentRole.Team == PlayerRoles.Team.SCPs)
                {
                    Hub.playerStats.GetModule<HumeShieldStat>().CurValue = value;
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
        public bool DoNotTrack
        {
            get
            {
                return Hub.authManager.DoNotTrack;

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
                Hub.TryOverridePosition(Hub.transform.position, new Vector3(0f, value, 0f));
            }
        }

        [MoonSharpVisible(true)]
        public Vector3 Scale
        {
            get
            {
                return Hub.transform.localScale;
            }
            set
            {
                try
                {
                    if (Hub.transform.localScale == value) return;

                    Hub.transform.localScale = value;
                    foreach (ReferenceHub plr in ReferenceHub.AllHubs.Where(n => n.authManager.InstanceMode == CentralAuth.ClientInstanceMode.ReadyClient))
                    {
                        NetworkServer.SendSpawnMessage(plr.networkIdentity, plr.connectionToClient);
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex.ToString());
                }
            }
        }


        [MoonSharpVisible(true)]
        public Vector3 AimingPoint
        {
            get
            {
                Physics.Raycast(new Ray(Hub.PlayerCameraReference.position + Hub.PlayerCameraReference.forward * 0.1f, Hub.PlayerCameraReference.forward), out RaycastHit hit, 1000f, StandardHitregBase.HitregMask);
                return hit.point;
            }
        }

        [MoonSharpVisible(true)]
        public bool IsDisarmed
        {
            get
            {
                return Hub.inventory.IsDisarmed();
            }
            set
            {
                if (value)
                {
                    Hub.inventory.SetDisarmedStatus(null);
                    Hub.inventory.ServerDropEverything();
                    DisarmedPlayers.Entries.Add(new DisarmedPlayers.DisarmedEntry(Hub.networkIdentity.netId, 0u));
                    new DisarmedPlayersListMessage(DisarmedPlayers.Entries).SendToAuthenticated();

                    return;
                }

                Hub.inventory.SetDisarmedStatus(null);
                new DisarmedPlayersListMessage(DisarmedPlayers.Entries).SendToAuthenticated();
            }
        }

        [MoonSharpVisible(true)]
        public bool IsAlive
        {
            get
            {
                return Hub.roleManager.CurrentRole.RoleTypeId != RoleTypeId.Spectator && Hub.roleManager.CurrentRole.RoleTypeId != RoleTypeId.None;
            }
            set
            {
                if (Hub.roleManager.CurrentRole.RoleTypeId != RoleTypeId.Spectator && Hub.roleManager.CurrentRole.RoleTypeId != RoleTypeId.None)
                {
                    Hub.playerStats.KillPlayer(new CustomReasonDamageHandler("Death", float.MaxValue));
                }
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
