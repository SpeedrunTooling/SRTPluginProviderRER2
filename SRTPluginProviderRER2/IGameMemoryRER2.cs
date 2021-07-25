using SRTPluginProviderRER2.Structs.GameStructs;
using System;

namespace SRTPluginProviderRER2
{
    public interface IGameMemoryRER2
    {
        // Versioninfo
        string GameName { get; }
        string VersionInfo { get; }

        int BP { get; set; }
        string PlayerName { get; set; }
        GamePlayer Player { get; set; }
        int PlayerInventorySize { get; set; }
        GameInventory[] PlayerInventory { get; set; }
        GameInventory[] PlayerEquipped { get; set; }

        string Player2Name { get; set; }
        GamePlayer Player2 { get; set; }
        int Player2InventorySize { get; set; }
        GameInventory[] Player2Inventory { get; set; }
        GameInventory[] Player2Equipped { get; set; }


        GameEndResults EndResults { get; set; }

        int EnemyCount { get; set; }
        GameEnemy[] EnemyHealth { get; set; }

        TimeSpan IGTTimeSpan { get; }
        string IGTFormattedString { get; }

    }
}