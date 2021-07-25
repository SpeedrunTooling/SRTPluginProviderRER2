using ProcessMemory;
using SRTPluginProviderRER2.Structs.GameStructs;
using System;
using System.Diagnostics;

namespace SRTPluginProviderRER2
{
    internal unsafe class GameMemoryRER2Scanner : IDisposable
    {
        private readonly int MAX_ENTITIES = 16;
        private readonly int MAX_ITEMS = 20;
        private readonly int MAX_EQUIP = 4;

        // Variables
        private ProcessMemoryHandler memoryAccess;
        private GameMemoryRER2 gameMemoryValues;
        public bool HasScanned;
        public bool ProcessRunning => memoryAccess != null && memoryAccess.ProcessRunning;
        public int ProcessExitCode => (memoryAccess != null) ? memoryAccess.ProcessExitCode : 0;

        // Pointer Address Variables
        private int pointerAddressPlayer;
        private int pointerAddressStats;
        private int pointerAddressInventory;
        private int pointerAddressEnemies;

        // Pointer Classes
        private IntPtr BaseAddress { get; set; }
        private MultilevelPointer PointerHP { get; set; }
        private MultilevelPointer PointerPlayerID { get; set; }
        private MultilevelPointer PointerHP2 { get; set; }
        private MultilevelPointer PointerPlayer2ID { get; set; }
        private MultilevelPointer PointerStats { get; set; }

        private MultilevelPointer PointerInventorySize { get; set; }
        private MultilevelPointer PointerInventorySize2 { get; set; }
        private MultilevelPointer[] PointerInventory { get; set; }
        private MultilevelPointer[] PointerInventory2 { get; set; }
        private MultilevelPointer[] PointerEquipped { get; set; }
        private MultilevelPointer[] PointerEquipped2 { get; set; }

        private MultilevelPointer PointerEnemyCount { get; set; }
        private MultilevelPointer[] PointerEnemyEntires { get; set; }

        internal GameMemoryRER2Scanner(Process process = null)
        {
            gameMemoryValues = new GameMemoryRER2();
            if (process != null)
                Initialize(process);
        }

        internal unsafe void Initialize(Process process)
        {
            if (process == null)
                return; // Do not continue if this is null.

            GameHashes.DetectVersion(process.MainModule.FileName);
            SelectPointerAddresses();

            int pid = GetProcessId(process).Value;
            memoryAccess = new ProcessMemoryHandler(pid);
            if (ProcessRunning)
            {
                BaseAddress = NativeWrappers.GetProcessBaseAddress(pid, PInvoke.ListModules.LIST_MODULES_32BIT); // Bypass .NET's managed solution for getting this and attempt to get this info ourselves via PInvoke since some users are getting 299 PARTIAL COPY when they seemingly shouldn't.

                //POINTERS
                PointerHP = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressPlayer), 0x20);
                PointerPlayerID = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressPlayer), 0x20, 0x0, 0x10, 0x1);
                PointerHP2 = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressPlayer), 0x24);
                PointerPlayer2ID = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressPlayer), 0x24, 0x0, 0x10, 0x1);
                PointerStats = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressStats));

                gameMemoryValues._playerInventory = new GameInventory[MAX_ITEMS];
                gameMemoryValues._player2Inventory = new GameInventory[MAX_ITEMS];
                PointerInventory = new MultilevelPointer[MAX_ITEMS];
                PointerInventory2 = new MultilevelPointer[MAX_ITEMS];
                PointerInventorySize = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressInventory), 0x30, 0x150);
                PointerInventorySize2 = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressInventory), 0x30, 0x154);
                var position = 0;
                for (var i = 0; i < MAX_ITEMS; i++)
                {
                    position = i * 0x4 + 0xC;
                    gameMemoryValues._playerInventory[i] = new GameInventory();
                    gameMemoryValues._player2Inventory[i] = new GameInventory();
                    PointerInventory[i] = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressInventory), 0x30, 0x150, position);
                    PointerInventory2[i] = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressInventory), 0x30, 0x154, position);
                }

                gameMemoryValues._playerEquipped = new GameInventory[MAX_EQUIP];
                gameMemoryValues._player2Equipped = new GameInventory[MAX_EQUIP];
                PointerEquipped = new MultilevelPointer[MAX_EQUIP];
                PointerEquipped2 = new MultilevelPointer[MAX_EQUIP];
                for (var i = 0; i < MAX_EQUIP; i++)
                {
                    position = i * 0x4 + 0x5C;
                    gameMemoryValues._playerEquipped[i] = new GameInventory();
                    gameMemoryValues._player2Equipped[i] = new GameInventory();
                    PointerEquipped[i] = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressInventory), 0x30, 0x150, position);
                    PointerEquipped2[i] = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressInventory), 0x30, 0x154, position);
                }

                gameMemoryValues._enemyHealth = new GameEnemy[MAX_ENTITIES];
                PointerEnemyEntires = new MultilevelPointer[MAX_ENTITIES];
                PointerEnemyCount = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressEnemies), 0x3C, 0x7C);
                for (var i = 0; i < MAX_ENTITIES; i++)
                {
                    position = i * 0x4 + 0x30;
                    PointerEnemyEntires[i] = new MultilevelPointer(memoryAccess, IntPtr.Add(BaseAddress, pointerAddressEnemies), 0x3C, 0x7C, position);
                }
            }
        }

        private void SelectPointerAddresses()
        {
            pointerAddressPlayer = 0x1167EAC;
            pointerAddressStats = 0x117D120;
            pointerAddressInventory = 0x117D1E8;
            pointerAddressEnemies = 0x1161E70;
        }

        internal void UpdatePointers()
        {
            PointerHP.UpdatePointers();
            PointerPlayerID.UpdatePointers();
            PointerHP2.UpdatePointers();
            PointerPlayer2ID.UpdatePointers();
            PointerStats.UpdatePointers();

            PointerInventorySize.UpdatePointers();
            PointerInventorySize2.UpdatePointers();

            // Player Inventories
            for (var i = 0; i < MAX_ITEMS; i++)
            {
                PointerInventory[i].UpdatePointers();
                PointerInventory2[i].UpdatePointers();
            }

            // Player Equippment
            for (var i = 0; i < MAX_EQUIP; i++)
            {
                PointerEquipped[i].UpdatePointers();
                PointerEquipped2[i].UpdatePointers();
            }

            PointerEnemyCount.UpdatePointers();
            for (var i = 0; i < MAX_ENTITIES; i++)
            {
                PointerEnemyEntires[i].UpdatePointers();
            }
        }

        internal unsafe IGameMemoryRER2 Refresh()
        {
            //
            gameMemoryValues._bp = PointerStats.DerefInt(0xE88);

            // Player 1
            gameMemoryValues._player = PointerHP.Deref<GamePlayer>(0x1A00);
            gameMemoryValues._playerName = PointerPlayerID.Deref<GamePlayerID>(0x1C).Name;

            // Player 2
            gameMemoryValues._player2 = PointerHP2.Deref<GamePlayer>(0x1A00);
            gameMemoryValues._player2Name = PointerPlayer2ID.Deref<GamePlayerID>(0x1C).Name;

            // Game Statistics
            gameMemoryValues._endResults = PointerStats.Deref<GameEndResults>(0x1403F0);

            // Player Inventories
            gameMemoryValues._playerInventorySize = PointerInventorySize.DerefInt(0x8);
            gameMemoryValues._player2InventorySize = PointerInventorySize2.DerefInt(0x8);

            for (var i = 0; i < MAX_ITEMS; i++)
            {
                gameMemoryValues._playerInventory[i] = PointerInventory[i].Deref<GameInventory>(0x0);
                gameMemoryValues._player2Inventory[i] = PointerInventory2[i].Deref<GameInventory>(0x0);
            }

            // Player Equippment
            for (var i = 0; i < MAX_EQUIP; i++)
            {
                gameMemoryValues._playerEquipped[i] = PointerEquipped[i].Deref<GameInventory>(0x0);
                gameMemoryValues._player2Equipped[i] = PointerEquipped2[i].Deref<GameInventory>(0x0);
            }

            gameMemoryValues._enemyCount = PointerEnemyCount.DerefInt(0x2C);
            for (var i = 0; i < MAX_ENTITIES; i++)
            {
                gameMemoryValues._enemyHealth[i] = PointerEnemyEntires[i].Deref<GameEnemy>(0x1A00);
            }

            HasScanned = true;
            return gameMemoryValues;
        }

        private int? GetProcessId(Process process) => process?.Id;

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    if (memoryAccess != null)
                        memoryAccess.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~REmake1Memory() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
