// Copyright file="ICipherModeVisitor.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Interfaces
{
	using Cryptools.Models.CipherModes;

	public interface ICipherModeVisitor
	{
		void Visit(CipherBlockChainMode mode);
		
		void Visit(CipherFeedbackMode mode);

		void Visit(ElectronicCodebookMode mode);

		void Visit(OutputFeedbackMode mode);
	}
}
