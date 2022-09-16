// Copyright file="OutputFeedbackMode.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Models.CipherModes
{
	using Cryptools.Interfaces;

	public class OutputFeedbackMode : ICipherModeVisitable
	{
		public void Accept(ICipherModeVisitor visitor)
		{
			visitor.Visit(this);
		}
	}
}
