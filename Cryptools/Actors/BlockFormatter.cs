// Copyright file="BlockFormatter.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Actors
{
	using Cryptools.Models;
	using System.Security.Cryptography;

	public class BlockFormatter
    {
        private int blockSize;

        public BlockFormatter(int blockSize)
        {
            this.BlockSize = blockSize;
        }

        public int BlockSize
        {
            get
            {
                return blockSize;
            }
            set
            {
                blockSize =
                    value > 127 && Math.Log2(value) % 1 == 0
                    ? value
                    : throw new ArgumentOutOfRangeException("Block size must be at least 128 and a power of 2");
            }
        }

        protected int BlockSizeInBytes => this.BlockSize / 8;

        public Block[] TransformText(byte[] text)
        {
            if (text.Length == 0)
            {
                throw new ArgumentException(nameof(text), "Must specify text to be able to separate it into blocks");
            }

            // Note to self: Block size is calculated in bits, I'm dealing with an array of bytes, don't forget to convert!
            var blocks = new List<Block>();
            byte[] current;
            byte[]? padding;

            do
            {
                current = text.Take(BlockSizeInBytes).ToArray();
                padding = current.Length != BlockSizeInBytes
                    ? RandomNumberGenerator.GetBytes(BlockSizeInBytes - current.Length)
                    : null;

                blocks.Add(new Block(current, padding));
                text = text[0..(text.Length - current.Length)];
            }
            while (current.Length == BlockSizeInBytes);

            return blocks.ToArray();
        }
    }
}
