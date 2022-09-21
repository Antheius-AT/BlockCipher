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

        private static async Task Main(string[] args)
        {
            var network = new CryptoNetwork(new List<ICryptoModule>()
            {
                new BitwiseAssociator(),
            });

            ICipherModeVisitor visitor = new Encryptor(network);

            int blockLength = 128;
            var blockFormatter = new BlockFormatter(blockLength);
            var key = new SymmetricKey(Convert.FromHexString("4125442A472D4B6150645367566B5970"));


            var plainText = Console.ReadLine();
            var plainTextBytes = Encoding.ASCII.GetBytes(plainText);

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
            Console.WriteLine($"Plaintext with padding: {Encoding.ASCII.GetString(plainTextBytes)}");

            Console.WriteLine("Ciphertext:");

            foreach (var item in encryptedBlocks)
            {
                Console.Write(Encoding.ASCII.GetString(item.WholeBlock.ToByteArray()));
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
            Console.WriteLine(Encoding.ASCII.GetString(plaintextWithoutPadding));

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

            paddingBytes = RandomNumberGenerator.GetBytes(nextFactor - plainText.Length);

            return plainText.Concat(paddingBytes).ToArray();
        }

        private static byte[] RemovePadding(byte[] plainText)
        {
            return plainText.Take(plainText.Length - paddingBytes.Length).ToArray();
        }
    }
}