// Copyright file="Block.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

using Cryptools.Extensions;
using System.Collections;

namespace Cryptools.Models
{
    public class Block
    {
        public Block(BitArray data, BitArray? padding = null)
        {
            this.Data = data;
            this.Padding = padding;
		}

        public BlockSize BlockSize
        {
            get
            {
                return new BlockSize(this.Data.Length);
            }
        }

        public BitArray Data { get; private set; }

        public BitArray? Padding { get; }

		public void ModifyBlock(BitArray wholeBlockBits)
		{
			this.Data = wholeBlockBits;
		}
    }
}
