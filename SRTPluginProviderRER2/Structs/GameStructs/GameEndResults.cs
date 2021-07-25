using System;
using System.Runtime.InteropServices;

namespace SRTPluginProviderRER2.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0xE8C)]

    public struct GameEndResults
    {
        //[FieldOffset(0x38)] private int rankScore;
        [FieldOffset(0x4)] private float clearTime;
        [FieldOffset(0x8)] private short shotsFired;
        [FieldOffset(0xA)] private short enemiesHit;
        [FieldOffset(0xC)] private short retries;

        //public int Rank => (int)Math.Floor((decimal)rankScore / 1000);
        //public int RankScore => rankScore;
        public short ShotsFired => shotsFired;
        public short EnemiesHit => enemiesHit;
        public float Accuracy => ShotsFired != 0 ? (float)EnemiesHit / (float)ShotsFired : 0f;
        public short Retries => retries;
        public float ClearTime => clearTime;
    }
}
