// Copyright file="Permutator.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Modules
{
	using Cryptools.Interfaces;
	using Cryptools.Models;
	using System;
	using System.Collections;
	using System.Threading.Tasks;

	public class Permutator : ICryptoModule
	{
		private int[] permutationTable;

		public Permutator(int[] permutationTable)
		{
			this.permutationTable = permutationTable;
		}

		public Task<Block> Decrypt(Block plainText, BitArray key)
		{
			throw new NotImplementedException();
		}

		public Task<Block> Encrypt(Block plainText, BitArray key)
		{
			throw new NotImplementedException();
		}
	}
}
