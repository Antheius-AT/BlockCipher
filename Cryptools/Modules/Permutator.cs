// Copyright file="Permutator.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Modules
{
	using Cryptools.Extensions;
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

		public Task<RoundResult> Decrypt(Block plainText, BitArray key, int round)
		{
			key = key.LeftShift(round);

			if (permutationTable.Length != plainText.Data.Length)
			{
				throw new ArgumentException(nameof(plainText), $"Block size did not match table size of {permutationTable.Length}");
			}

			for (int i = permutationTable.Length - 1; i >= 0; i--)
			{
				var currentValue = plainText.Data[i];

				plainText.Data[i] = plainText.Data[permutationTable[i]];
				plainText.Data[permutationTable[i]] = currentValue;
			}

			return Task.FromResult(new RoundResult(plainText, key));
		}

		public Task<RoundResult> Encrypt(Block plainText, BitArray key, int round)
		{
			key = key.RightShift(round);

			if (permutationTable.Length != plainText.Data.Length)
			{
				throw new ArgumentException(nameof(plainText), $"Block size did not match table size of {permutationTable.Length}");
			}
			
			for (int i = 0; i < permutationTable.Length; i++)
			{
				var currentValue = plainText.Data[i];

				plainText.Data[i] = plainText.Data[permutationTable[i]];
				plainText.Data[permutationTable[i]] = currentValue;
			}

			return Task.FromResult(new RoundResult(plainText, key));
		}
	}
}
