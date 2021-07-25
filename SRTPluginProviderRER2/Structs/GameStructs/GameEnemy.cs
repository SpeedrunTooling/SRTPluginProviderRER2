using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SRTPluginProviderRER2.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x10)]
    public struct GameEnemy
    {
        [FieldOffset(0x8)] private int currentHP;
        [FieldOffset(0xC)] private int maximumHP;
        public bool IsNaN(float hp) => hp.CompareTo(float.NaN) == 0;
        public float CurrentHP => !IsNaN(currentHP) ? currentHP : 0f;
        public float MaximumHP => !IsNaN(maximumHP) ? maximumHP : 0f;
        public bool IsTrigger => CurrentHP <= 10 && MaximumHP <= 10 || IsNaN(currentHP) || IsNaN(maximumHP);
        public bool IsAlive => !IsTrigger && CurrentHP != 0;
        public bool IsDamaged => IsAlive ? CurrentHP < MaximumHP : false;
        public float Percentage => (IsAlive) ? CurrentHP / MaximumHP : 0f;
    }
}