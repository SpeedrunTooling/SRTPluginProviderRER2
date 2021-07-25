using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SRTPluginProviderRER2.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x1C)]

    public struct GameInventory
    {
        [FieldOffset(0x4)] private short id;
        [FieldOffset(0x8)] private int unknown;
        [FieldOffset(0xC)] private int quantity;
        [FieldOffset(0x10)] private int maxQuantity;
        [FieldOffset(0x18)] private int partsBoxType;

        public short ID => id;
        public string Name => Items.Names.ContainsKey(ID) ? Items.Names[ID] : "";
        public int Unknown => unknown;
        public int Quantity => quantity;
        public int MaxQuantity => maxQuantity;
        public int PartsBoxType => partsBoxType;
    }

    public class Items
    {
        public static Dictionary<short, string> Names = new Dictionary<short, string>()
        {
            { 0x0102, "Bubble Gun" },
            { 0x0105, "Handgun MPM" },
            { 0x0106, "Handgun P10" },
            { 0x0107, "Samurai Edge" },
            { 0x0108, "Handgun MPM/S" },
            { 0x0109, "Handgun Triple Shot" },
            { 0x010C, "Shotgun M147S" },
            { 0x010D, "Shotgun TAP194" },
            { 0x010F, "Shotgun Hydra" },
            { 0x0110, "Shotgun Drake" },
            { 0x0115, "MP-AB50" },
            { 0x0116, "MP-AF" },
            { 0x0117, "Chicago Typewriter" },
            { 0x0118, "MP-AB50G" },
            { 0x011C, "Assault Rifle AK-7" },
            { 0x011D, "Assault Rifle NSR47" },
            { 0x011E, "AR High Roller" },
            { 0x0125, "Rifle M1891/30" },
            { 0x0126, "Rifle SVD" },
            { 0x0127, "Anti-Materiel Rifle" },
            { 0x0128, "Rifle Muramasa" },
            { 0x012C, "Magnum Model 329" },
            { 0x012D, "Magnum 2005M" },
            { 0x012E, "Magnum Python" },
            { 0x012F, "Magnum Anaconda" },
            { 0x0130, "Pale Rider" },
            { 0x0136, "Bowgun" },
            { 0x013A, "Rocket Launcher" },
            { 0x013B, "Infinite Rocket Launcher" },
            { 0x014A, "Knife" },
            { 0x014B, "Survival Knife" },
            { 0x014D, "Crowbar" },
            { 0x014E, "Crowbar" },
            { 0x014F, "Katana" },
            { 0x0150, "Short Sword" },
            { 0x0155, "Flashlight" },
            { 0x0157, "Flashlight" },
            { 0x0158, "Meat Grinder" },
            { 0x0174, "G18 G.E.O." },
            { 0x0175, "G18 Type-T" },
            { 0x0176, "G18 Candiru" },
            { 0x0177, "G18 JC2" },
            { 0x0178, "G18" },
            { 0x0145, "Smokescreen Bottle" },
            { 0x0146, "Firebomb Bottle" },
            { 0x0147, "Exploding Bottle" },
            { 0x0149, "Decoy Bottle" },
            { 0x0201, "Handgun Ammo" },
            { 0x0202, "Shotgun Ammo" },
            { 0x0203, "Assault Rifle Ammo" },
            { 0x0204, "Rifle Ammo" },
            { 0x0205, "Magnum Ammo" },
            { 0x020A, "Machine Pistol Ammo" },
            { 0x0900, "Handgun Ammo Case" },
            { 0x0901, "Shotgun Ammo Case" },
            { 0x0902, "Machine Pistol Ammo Case" },
            { 0x0903, "Assault Rifle Ammo Case" },
            { 0x0904, "Rifle Ammo Case" },
            { 0x0905, "Magnum Ammo Case" },
            { 0x0920, "Expansion Bag (Claire)" },
            { 0x0921, "Expansion Bag (Barry)" },
            { 0x0922, "Expansion Bag (Moira)" },
            { 0x0923, "Expansion Bag (Natalia)" },
            { 0x0301, "Green Herb" },
            { 0x0303, "Red Herb" },
            { 0x0304, "Tourniquet" },
            { 0x0305, "Disinfectant" },
            { 0x0800, "Smoke Powder" },
            { 0x0801, "Empty Bottle" },
            { 0x0802, "Cloth" },
            { 0x0803, "Alcohol" },
            { 0x0804, "Gunpowder" },
            { 0x0805, "Odorous Chemical" },
            { 0x0601, "Parts Box" },
            { 0x0602, "Rare Parts Box" },
            { 0x0401, "Diamond (2000 BP)" },
            { 0x0402, "Ruby (250 BP)" },
            { 0x0403, "Sapphire (500 BP)" },
            { 0x0404, "Emerald (1000 BP)" },
            { 0x0405, "Topaz (100 BP)" },
            { 0x0501, "Experiment Block Key" },
            { 0x0506, "Battery" },
            { 0x0507, "Fuel" },
            { 0x0508, "Back Gate Key" },
            { 0x0509, "Drill" },
            { 0x0510, "Control Room Key" },
            { 0x0511, "Key of Guilt" },
            { 0x0512, "Processing Plant Key" },
            { 0x0513, "Glass Eyeball" },
            { 0x0514, "Artificial Eye" },
            { 0x0515, "Liver Replica (Left)" },
            { 0x0516, "Slaughterhouse Key" },
            { 0x0517, "Fence Key" },
            { 0x0518, "Liver Replica (Right)" },
            { 0x0519, "Sewer Passage Key" },
            { 0x0520, "Lift Activation Key" },
            { 0x0521, "Security Card Level 1" },
            { 0x0522, "Security Card Level 2" },
            { 0x0523, "Emblem Key" },
            { 0x0524, "Gear Kog" },
            { 0x0525, "Rusty Key" },
            { 0x0526, "Area Key" },
            { 0x1000, "Detention Center Map" },
            { 0x1001, "Detention Center Map" },
            { 0x1002, "Fishing Village Map" },
            { 0x1003, "Fishing Village Map" },
            { 0x1004, "Food Processing Plant Map" },
            { 0x1005, "Underground Research Facility Map" },
            { 0x1006, "Forest Map" },
            { 0x1007, "Town Map" },
            { 0x1008, "Town Map" },
            { 0x1009, "Sewers Map" },
            { 0x1010, "Sewers Map" },
            { 0x1011, "Sewage Treatment Plant Map" },
            { 0x1012, "Quarry Map" },
            { 0x1013, "Mines Map" },
            { 0x1101, "Rat Meat (20)" },
            { 0x1102, "Rabbit Meat (15)" }
        };
    }
}
