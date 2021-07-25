using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SRTPluginProviderRER2.Structs.GameStructs
{
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 0x3)]

    public struct GamePlayerID
    {
        [FieldOffset(0x1)] private short id;

        public short ID => id;
        public string Name => Characters.NamesList.ContainsKey(ID) ? string.Format("{0}: ", Characters.NamesList[ID]) : "";
    }

    public class Characters
    {
        public static Dictionary<short, string> NamesList = new Dictionary<short, string>()
        {
            { 0x0015, "Barry" },
            { 0x0016, "Claire" },
            { 0x0017, "Moira" },
            { 0x0018, "Natalia" },
            { 0x001F, "Neil" },
            { 0x0020, "Pedro" },
            { 0x0021, "Evgeny" },
            { 0x0022, "Gabe" },
            { 0x0024, "Alex Wesker" },
            { 0x0025, "Racheal" },
            { 0x0032, "Chris" },
            { 0x0033, "Leon" },
            { 0x0034, "Jill" },
            { 0x0035, "Albert Wesker" },
            { 0x0036, "Hunk" },
            { 0x0037, "Cipher" }
        };
    }
}
