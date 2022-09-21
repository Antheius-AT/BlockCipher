// Copyright file="Encryptor.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Actors
{
	using Cryptools.Extensions;
	using Cryptools.Interfaces;
	using Cryptools.Models;
	using Cryptools.Models.CipherModes;
	using System.Collections;

	public class Encryptor : ICipherModeVisitor
    {
		private BitArray? key;
		private CryptoNetwork network;
		private RoundResult operationResult;

		public Encryptor(CryptoNetwork network)
		{
			this.network = network;
		}

		public async Task Visit(CipherBlockChainMode mode)
		{
			throw new NotImplementedException();
		}

		public async Task Visit(CipherFeedbackMode mode)
		{
			throw new NotImplementedException();
		}

		public async Task Visit(ElectronicCodebookMode mode)
		{
			if (key == null)
			{
				throw new InvalidOperationException("Key must not be null to encrypt data");
			}

			var encryptedBlock = await network.EncryptBlock(mode.Block, key);
			operationResult = encryptedBlock;
		}

		public async Task Visit(OutputFeedbackMode mode)
		{
			throw new NotImplementedException();
		}

		public RoundResult GetResult()
		{
			return this.operationResult;
		}

		public void SetCryptoParams(BitArray key)
		{
			this.key = key;
		}
	}
}
