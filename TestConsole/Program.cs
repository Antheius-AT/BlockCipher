// Copyright file="Program.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools
{
    using Cryptools.Actors;
    using Cryptools.Extensions;
    using Cryptools.Interfaces;
    using Cryptools.Models;
    using Cryptools.Models.CipherModes;
    using Cryptools.Modules;
    using System.Collections;
    using System.Security.Cryptography;
    using System.Text;
    using System.Linq;

    public class Program
    {
        private static byte[] paddingBytes;
        private static string paddingText = "paddingpaddingpaddingpaddingpadding";

        private static async Task Main(string[] args)
        {
            int blockLength = 128;

            // Convert key to BitArray from hex string.
            var key = new SymmetricKey(Convert.FromHexString("4125442A472D4B6150645367566B5970"));
            var currentKey = new BitArray(key.KeyBytes);

            // Here the network is configured by adding modules to the network.
            // Each module is called once per block encryption/decryption.
            // Note that the BitwiseAssociator (XOR) module requires the key and block length to be 128 bit in size.
            var network = new CryptoNetwork(new List<ICryptoModule>()
            {
                new BitwiseAssociator(),
                new Substitutor(GetSubstitutionTable()),
                new Permutator(CreatePermutationTable(blockLength)),
                new Permutator(CreatePermutationTable(blockLength)),
                new Substitutor(GetSubstitutionTable()),
                new BitwiseAssociator(),
                new Substitutor(GetSubstitutionTable()),
            });

            // Start encryption process
            ICipherModeVisitor visitor = new Encryptor(network);
            var blockFormatter = new BlockFormatter(blockLength);

            // User input plaintext and convert plaintext from string to bytes.
            var plainText = Console.ReadLine();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Append padding if required, based on blockLength
            plainTextBytes = AppendPadding(plainTextBytes, blockLength);

            // Create individual blocks based on text and block size.
            var blocks = blockFormatter.TransformTextToBlocks(plainTextBytes);

            visitor.SetCryptoParams(currentKey);

            // Initialize list that stores encrypted blocks.
            List<Block> encryptedBlocks = new List<Block>();

            // Encrypt blocks.
            foreach (var item in blocks)
            {
                var mode = new ElectronicCodebookMode(item);
                visitor.SetCryptoParams(currentKey);
                await mode.Accept(visitor);
                var roundResult = visitor.GetResult();
                currentKey = roundResult.CurrentKey;
                encryptedBlocks.Add(roundResult.Block);
            }

            PrintInColor($"Plaintext without padding: {plainText}", ConsoleColor.Yellow);
            PrintInColor($"Plaintext with padding: {Encoding.UTF8.GetString(plainTextBytes)}", ConsoleColor.Yellow);
            PrintInColor($"Plaintext without padding in base 64: {Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText))}", ConsoleColor.Yellow);
            PrintInColor("Ciphertext (in base 64 ausgegeben):", ConsoleColor.Yellow);

            // Visualize encrypted blocks on console
            foreach (var item in encryptedBlocks)
            {
                PrintInColor("Block start", ConsoleColor.Red);
                PrintInColor(Convert.ToBase64String(item.Data.ToByteArray()), ConsoleColor.Yellow);
                PrintInColor("Block end", ConsoleColor.Red);
            }

            var decipheredBytes = new List<Block>();

            // Start decryption process
            ICipherModeVisitor decryptor = new Decryptor(network);
            decryptor.SetCryptoParams(currentKey);

            // Reverse blocks to start deciphering from last block to first block.
            // Required when using the permutator due to bitshift shenanigans.
            encryptedBlocks.Reverse();

            // Decrypt blocks
            foreach (var item in encryptedBlocks)
            {
                var mode = new ElectronicCodebookMode(item);
                await mode.Accept(decryptor);
                var singleResult = decryptor.GetResult();
                currentKey = singleResult.CurrentKey;
                decipheredBytes.Add(singleResult.Block);
            }

            // Bring deciphered blocks into correct order again, as they were deciphered last to first.
            decipheredBytes.Reverse();

            // Transform blocks to single byte array and remove padding from deciphered plaintext.
            var finalResult = blockFormatter.TransformBlocksToText(decipheredBytes);
            var plaintextWithoutPadding = RemovePadding(finalResult);

            // Output result
            PrintInColor("Plain text ohne padding Schlussresultat base64:", ConsoleColor.Red);
            PrintInColor(Convert.ToBase64String(plaintextWithoutPadding), ConsoleColor.Yellow);

            PrintInColor("Plain text ohne padding Schlussresultat:", ConsoleColor.Red);
            PrintInColor(Encoding.UTF8.GetString(plaintextWithoutPadding), ConsoleColor.Yellow);

            Console.ReadLine();
        }

        private static byte[] AppendPadding(byte[] plainText, int blockLength)
        {
            if (plainText.Length % (blockLength / 8) == 0)
            {
                return plainText;
            }

            var nextFactor = blockLength / 8;

            while (plainText.Length > nextFactor)
            {
                nextFactor += (blockLength / 8);
            }

            paddingBytes = Encoding.UTF8.GetBytes(paddingText.Take(nextFactor - plainText.Length).ToArray());

            return plainText.Concat(paddingBytes).ToArray();
        }

        private static byte[] RemovePadding(byte[] plainText)
        {
            if (paddingBytes == null)
            {
                return plainText;
            }

            return plainText.Take(plainText.Length - paddingBytes.Length).ToArray();
        }

        private static int[] CreatePermutationTable(int blockLength)
        {
            var table = new int[blockLength];

            for (int i = 0; i < table.Length; i++)
            {
                table[i] = i;
            }

            for (int i = 0; i < table.Length * 5; i++)
            {
                Permutate(table);
            }

            return table;
        }

        private static char[] GetSubstitutionTable()
        {
            var result = new char[256];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (char)i;
            }

            for (int i = 0; i < result.Length * 5; i++)
            {
                Permutate(result);
            }

            return result;
        }

        private static T[] Permutate<T>(T[] table)
        {
            for (int i = 0; i < table.Length; i++)
            {
                var newIndex = RandomNumberGenerator.GetInt32(0, table.Length);
                var current = table[i];

                table[i] = table[newIndex];
                table[newIndex] = current;
            }

            return table;
        }

        private static void PrintInColor(string message, ConsoleColor color)
        {
            var currentColor = Console.ForegroundColor;

            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ForegroundColor = currentColor;
        }
    }
}