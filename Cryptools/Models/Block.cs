// Copyright file="Block.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Models
{
	using System.Collections;

	public class Block
    {
        public Block(BitArray data)
        {
            this.Data = data;
		}

        public BlockSize BlockSize
        {
            get
            {
                return new BlockSize(this.Data.Length);
            }
        }

        public BitArray Data { get; private set; }

		public void ModifyBlock(BitArray wholeBlockBits)
		{
			this.Data = wholeBlockBits;
		}
    }
}
