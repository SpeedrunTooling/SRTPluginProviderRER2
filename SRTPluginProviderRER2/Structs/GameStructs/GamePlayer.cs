using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SRTPluginProviderRER2.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x10)]

    public struct GamePlayer
    {
        //[FieldOffset(0xE28)] private short id;
        [FieldOffset(0x8)] private int currentHP;
        [FieldOffset(0xC)] private int maxHP;

        //public short ID => id;
        //public string Name => Characters.NamesList.ContainsKey(ID) ? string.Format("{0}: ", Characters.NamesList[ID]) : "";
        public int CurrentHP => currentHP;
        public int MaxHP => maxHP;
        public float Percentage => CurrentHP > 0 ? (float)CurrentHP / (float)MaxHP : 0f;
        public bool IsAlive => CurrentHP != 0 && MaxHP != 0 && CurrentHP > 0 && CurrentHP <= MaxHP;
        public PlayerState HealthState
        {
            get =>
                !IsAlive ? PlayerState.Dead :
                Percentage >= 0.75f ? PlayerState.Fine :
                Percentage >= 0.50f ? PlayerState.FineToo :
                Percentage >= 0.25f ? PlayerState.Caution :
                PlayerState.Danger;
        }
    }

    public enum PlayerState
    {
        Dead,
        Fine,
        FineToo,
        Caution,
        Danger
    }
}