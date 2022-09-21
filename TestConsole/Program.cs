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

            var network = new CryptoNetwork(new List<ICryptoModule>()
            {
                new Permutator(CreatePermutationTable(blockLength)),
            });

            ICipherModeVisitor visitor = new Encryptor(network);

            var blockFormatter = new BlockFormatter(blockLength);
            var key = new SymmetricKey(Convert.FromHexString("4125442A472D4B6150645367566B5970"));


            var plainText = Console.ReadLine();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            plainTextBytes = AppendPadding(plainTextBytes, blockLength);
            List<Block> encryptedBlocks = new List<Block>();
            
            var blocks = blockFormatter.TransformTextToBlocks(plainTextBytes);
            visitor.SetCryptoParams(key);

            foreach (var item in blocks)
            {
                var mode = new ElectronicCodebookMode(item);
                await mode.Accept(visitor);
                var singleBlockResult = visitor.GetResult();
                encryptedBlocks.Add(singleBlockResult);
            }

            Console.WriteLine($"Plaintext without padding: {plainText}");
            Console.WriteLine($"Plaintext with padding: {Encoding.UTF8.GetString(plainTextBytes)}");

            Console.WriteLine("Ciphertext:");

            foreach (var item in encryptedBlocks)
            {
                Console.WriteLine("Block start");
                Console.Write(Encoding.UTF8.GetString(item.Data.ToByteArray()));
                Console.WriteLine("Block End");
            }

            Console.WriteLine();

            var decipheredBytes = new List<Block>();

            var decryptor = new Decryptor(network);

            decryptor.SetCryptoParams(key);
            foreach (var item in encryptedBlocks)
            {
                var mode = new ElectronicCodebookMode(item);
                await mode.Accept(decryptor);
                var singleResult = decryptor.GetResult();
                decipheredBytes.Add(singleResult);
            }

            var finalResult = blockFormatter.TransformBlocksToText(decipheredBytes);

            var plaintextWithoutPadding = RemovePadding(finalResult);

            Console.WriteLine("Plain text ohne padding Schlussresultat:");
            Console.WriteLine(Encoding.UTF8.GetString(plaintextWithoutPadding));

            Console.ReadLine();
        }

        private static byte[] AppendPadding(byte[] plainText, int blockLength)
        {
            // currently only support 128 bit length.
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

        private static int[] Permutate(int[] table)
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
    }
}