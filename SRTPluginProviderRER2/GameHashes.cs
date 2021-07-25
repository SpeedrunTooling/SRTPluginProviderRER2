using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace SRTPluginProviderRER2
{
    /// <summary>
    /// SHA256 hashes for the RE5/BIO5 game executables.
    /// </summary>
    public static class GameHashes
    {
        private static readonly byte[] rerev2ww_20210702_1 = new byte[32] { 0x41, 0xB4, 0x16, 0x96, 0x75, 0x8C, 0x41, 0x77, 0xE9, 0x33, 0x9F, 0x27, 0xF0, 0x51, 0xAA, 0x4D, 0x72, 0x90, 0x8E, 0xD7, 0x26, 0x3A, 0x82, 0xAA, 0x63, 0xB0, 0x93, 0x3A, 0x3B, 0xCA, 0x9C, 0x77 };
        public static GameVersion DetectVersion(string filePath)
        {
            byte[] checksum;
            using (SHA256 hashFunc = SHA256.Create())
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete))
                checksum = hashFunc.ComputeHash(fs);

            if (checksum.SequenceEqual(rerev2ww_20210702_1))
            {
                Console.WriteLine("Steam Version WW Detected");
                return GameVersion.REREV2WW_20210702_1;
            }

            Console.WriteLine("Unknown Version");
            return GameVersion.Unknown;
        }
    }
}
