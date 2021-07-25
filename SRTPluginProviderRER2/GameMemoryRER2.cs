using SRTPluginProviderRER2.Structs.GameStructs;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace SRTPluginProviderRER2
{
    public class GameMemoryRER2 : IGameMemoryRER2
    {
        private const string IGT_TIMESPAN_STRING_FORMAT = @"hh\:mm\:ss";
        public string GameName => "REREV2";

        // Versioninfo
        public string VersionInfo => FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion;

        // BP
        public int BP { get => _bp; set => _bp = value; }
        internal int _bp;

        // Player 1 Stats
        public string PlayerName { get => _playerName; set => _playerName = value; }
        internal string _playerName;
        public GamePlayer Player { get => _player; set => _player = value; }
        internal GamePlayer _player;
        public int PlayerInventorySize { get => _playerInventorySize; set => _playerInventorySize = value; }
        internal int _playerInventorySize;
        public GameInventory[] PlayerInventory { get => _playerInventory; set => _playerInventory = value; }
        internal GameInventory[] _playerInventory;
        public GameInventory[] PlayerEquipped { get => _playerEquipped; set => _playerEquipped = value; }
        internal GameInventory[] _playerEquipped;

        // Player 2 Stats
        public string Player2Name { get => _player2Name; set => _player2Name = value; }
        internal string _player2Name;
        public GamePlayer Player2 { get => _player2; set => _player2 = value; }
        internal GamePlayer _player2;
        public int Player2InventorySize { get => _player2InventorySize; set => _player2InventorySize = value; }
        internal int _player2InventorySize;
        public GameInventory[] Player2Inventory { get => _player2Inventory; set => _player2Inventory = value; }
        internal GameInventory[] _player2Inventory;
        public GameInventory[] Player2Equipped { get => _player2Equipped; set => _player2Equipped = value; }
        internal GameInventory[] _player2Equipped;

        public GameEndResults EndResults { get => _endResults; set => _endResults = value; }
        internal GameEndResults _endResults;

        public int EnemyCount { get => _enemyCount; set => _enemyCount = value; }
        internal int _enemyCount;
        public GameEnemy[] EnemyHealth { get => _enemyHealth; set => _enemyHealth = value; }
        internal GameEnemy[] _enemyHealth;

        public TimeSpan IGTTimeSpan
        {
            get
            {
                TimeSpan timespanIGT;

                if (EndResults.ClearTime >= 0f)
                    timespanIGT = TimeSpan.FromSeconds(EndResults.ClearTime);
                else
                    timespanIGT = new TimeSpan();

                return timespanIGT;
            }
        }

        public string IGTFormattedString => IGTTimeSpan.ToString(IGT_TIMESPAN_STRING_FORMAT, CultureInfo.InvariantCulture);
    }
}
