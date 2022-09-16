// Copyright file="Block.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Models
{
    public class Block
    {
        public Block(byte[] data, byte[]? padding = null)
        {
            this.Data = data;
            this.Padding = padding;
        }

        public BlockSize BlockSize
        {
            get
            {
                return new BlockSize(Data.Length + Padding?.Length ?? 0);
            }
        }

        public byte[] Data { get; }

        public byte[]? Padding { get; }
    }
}
