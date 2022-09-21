// Copyright file="BitwiseAssociator.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Modules
{
	using Cryptools.Interfaces;
	using Cryptools.Models;
	using System.Collections;
	using System.Threading.Tasks;

	public class BitwiseAssociator : ICryptoModule
	{
		public Task<Block> Decrypt(Block plainText, BitArray key)
		{
			plainText.ModifyBlock(plainText.WholeBlock.Xor(key));

			return Task.FromResult(plainText);
		}

		public Task<Block> Encrypt(Block plainText, BitArray key)
		{
			plainText.ModifyBlock(plainText.WholeBlock.Xor(key));

			return Task.FromResult(plainText);
		}
	}
}
