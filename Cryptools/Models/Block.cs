// Copyright file="Block.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

using Cryptools.Extensions;
using System.Collections;

namespace Cryptools.Models
{
    public class Block
    {
		private BitArray wholeBlock;

        public Block(BitArray data, BitArray? padding = null)
        {
            this.Data = data;
            this.Padding = padding;

			this.WholeBlock = new BitArray(Data.ToByteArray().Concat(Padding?.ToByteArray() ?? Enumerable.Empty<byte>()).ToArray());
		}

        public BlockSize BlockSize
        {
            get
            {
                return new BlockSize(this.Data.Length);
            }
        }

		public BitArray WholeBlock
		{
			get
			{
				return wholeBlock;
			}

			private set
			{
				this.wholeBlock = value;
			}
		}
        public BitArray Data { get; }

        public BitArray? Padding { get; }

		public void ModifyBlock(BitArray wholeBlockBits)
		{
			this.WholeBlock = wholeBlockBits;
		}
    }
}
