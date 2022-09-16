// Copyright file="Encryptor.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Actors
{
	using Cryptools.Interfaces;
	using Cryptools.Models.CipherModes;

	public class Encryptor : ICipherModeVisitor
    {
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
			throw new NotImplementedException();
		}

		public async Task Visit(OutputFeedbackMode mode)
		{
			throw new NotImplementedException();
		}
	}
}
