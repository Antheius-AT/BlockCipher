// Copyright file="Decryptor.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Actors
{
	using Cryptools.Extensions;
	using Cryptools.Interfaces;
	using Cryptools.Models;
	using Cryptools.Models.CipherModes;
	using System.Collections;
	using System.Threading.Tasks;

	public class Decryptor : ICipherModeVisitor
	{
		private readonly CryptoNetwork network;
		private SymmetricKey? key;
		private Block operationResult;

		public Decryptor(CryptoNetwork network)
		{
			this.network = network;
		}

		public Block GetResult()
		{
			return operationResult;
		}

		public void SetCryptoParams(SymmetricKey key)
		{
			this.key = key;
		}

		public Task Visit(CipherBlockChainMode mode)
		{
			throw new NotImplementedException();
		}

		public Task Visit(CipherFeedbackMode mode)
		{
			throw new NotImplementedException();
		}

		public async Task Visit(ElectronicCodebookMode mode)
		{
			if (key == null)
			{
				throw new InvalidOperationException("Key must not be null to encrypt data");
			}

			var keyBits = new BitArray(key.KeyBytes);

			var decryptedBlock = await network.DecryptBlock(mode.Block, keyBits);
			operationResult = decryptedBlock;
		}

		public Task Visit(OutputFeedbackMode mode)
		{
			throw new NotImplementedException();
		}
	}
}
