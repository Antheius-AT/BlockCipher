// Copyright file="CryptoNetwork.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Actors
{
	using Cryptools.Interfaces;
	using Cryptools.Models;
	using System.Collections;

	public class CryptoNetwork
	{
		private readonly IEnumerable<ICryptoModule> cryptoModules;

		public CryptoNetwork(IEnumerable<ICryptoModule> cryptoModules)
		{
			this.cryptoModules = cryptoModules;
		}

		public async Task<Block> EncryptBlock(Block plaintext, BitArray key)
		{
			var currentInputBlock = plaintext;

			foreach (var item in cryptoModules)
			{
				currentInputBlock = await item.Encrypt(currentInputBlock, key);
			}

			return currentInputBlock;
		}

		public async Task<Block> DecryptBlock(Block plainText, BitArray key)
		{
			var currentInputBlock = plainText;

			foreach (var item in cryptoModules)
			{
				currentInputBlock = await item.Decrypt(currentInputBlock, key);
			}

			return currentInputBlock;
		}
	}
}
