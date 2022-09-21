// Copyright file="ICryptoModule.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Interfaces
{
	using Cryptools.Models;
	using System.Collections;

	public interface ICryptoModule
	{
		Task<Block> Encrypt(Block plainText, BitArray key);
		Task<Block> Decrypt(Block plainText, BitArray key);
	}
}
