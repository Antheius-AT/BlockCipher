// Copyright file="ICryptoModule.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Interfaces
{
	using Cryptools.Models;
	using System.Collections;

	public interface ICryptoModule
	{
		Task<RoundResult> Encrypt(Block plainText, BitArray key, int round);
		Task<RoundResult> Decrypt(Block cipherText, BitArray key, int round);
	}
}
