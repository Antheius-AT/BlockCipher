// Copyright file="CipherFeedbackMode.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Models.CipherModes
{
	using Cryptools.Interfaces;

	public class CipherFeedbackMode : ICipherModeVisitable
	{
		public void Accept(ICipherModeVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
