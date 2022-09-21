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
			var currentKey = key;

			for (int i = 0; i < cryptoModules.Count(); i++)
			{
				var currentResult = await cryptoModules.ElementAt(i).Encrypt(currentInputBlock, currentKey, i);
				currentInputBlock = currentResult.Block;
				currentKey = currentResult.CurrentKey;
			}

			return currentInputBlock;
		}

		public async Task<Block> DecryptBlock(Block plainText, BitArray key)
		{
			var currentInputBlock = plainText;
			var currentKey = key;

			for (int i = cryptoModules.Count() - 1; i >= 0; i--)
			{
				var currentResult = await cryptoModules.ElementAt(i).Decrypt(currentInputBlock, currentKey, i);
				currentInputBlock = currentResult.Block;
				currentKey = currentResult.CurrentKey;
			}

			return currentInputBlock;
		}
	}
}
