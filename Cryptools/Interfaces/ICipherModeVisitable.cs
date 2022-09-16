﻿// Copyright file="ICipherModeVisitable.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Interfaces
{
	public interface ICipherModeVisitable
	{
		void Accept(ICipherModeVisitor visitor);
	}
}
