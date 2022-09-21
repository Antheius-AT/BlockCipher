// Copyright file="RoundResult.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Models
{
	using System.Collections;

	public class RoundResult
	{
		public RoundResult(Block block, BitArray currentKey)
		{
			this.Block = block;
			this.CurrentKey = currentKey;
		}

		public Block Block { get; }

		public BitArray CurrentKey { get; }
	}
}
