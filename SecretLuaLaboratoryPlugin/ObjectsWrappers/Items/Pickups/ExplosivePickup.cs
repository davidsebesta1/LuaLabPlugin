using InventorySystem.Items.ThrowableProjectiles;
using Mirror;
using MoonSharp.Interpreter.Interop;
using SecretLuaLaboratoryPlugin.Objects.Player;

namespace LuaLab.ObjectsWrappers.Items.Pickups
{
    public class ExplosivePickup : LuaItemPickup
    {
        public ExplosivePickup(TimeGrenade itemPickupBase) : base(itemPickupBase)
        {

        }

        [MoonSharpVisible(true)]
        public LuaPlayer Owner
        {
            get
            {
                return Plugin.Instance.LuaPlayerManager[((TimeGrenade)_itemPickupBase).PreviousOwner.Hub];
            }
        }

        [MoonSharpVisible(true)]
        public double FuseTime
        {
            get
            {
                return ((TimeGrenade)_itemPickupBase).Network_syncTargetTime - NetworkTime.time;
            }
            set
            {
                if (FuseTime <= 0d)
                {
                    ((TimeGrenade)_itemPickupBase).ServerFuseEnd();
                    return;
                }

                ((TimeGrenade)_itemPickupBase).ServerActivate();
                ((TimeGrenade)_itemPickupBase).Network_syncTargetTime = NetworkTime.time + value;
            }
        }

        [MoonSharpVisible(true)]
        public bool Detonated
        {
            get
            {
                return ((TimeGrenade)_itemPickupBase)._alreadyDetonated;
            }
            set
            {
                if (value)
                {
                    ((TimeGrenade)_itemPickupBase).ServerFuseEnd();
                }
            }
        }
    }
}
