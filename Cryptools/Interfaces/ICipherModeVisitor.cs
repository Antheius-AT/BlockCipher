// Copyright file="ICipherModeVisitor.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Interfaces
{
	using Cryptools.Models;
	using Cryptools.Models.CipherModes;
	using System.Collections;

	public interface ICipherModeVisitor
	{
		Task Visit(CipherBlockChainMode mode);

		Task Visit(CipherFeedbackMode mode);

		Task Visit(ElectronicCodebookMode mode);

		Task Visit(OutputFeedbackMode mode);

		void SetCryptoParams(BitArray key);

		RoundResult GetResult();
	}
}
