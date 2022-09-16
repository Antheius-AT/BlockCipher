// Copyright file="ICipherModeVisitor.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Interfaces
{
	using Cryptools.Models.CipherModes;

	public interface ICipherModeVisitor
	{
		Task Visit(CipherBlockChainMode mode);

		Task Visit(CipherFeedbackMode mode);

		Task Visit(ElectronicCodebookMode mode);

		Task Visit(OutputFeedbackMode mode);
	}
}
