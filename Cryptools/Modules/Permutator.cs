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
			int keyIndex;
			var keyBytes = key.ToByteArray();

			for (int i = plainText.Data.Length - 1; i >= 0; i--)
			{
				keyIndex = i;

				if (keyIndex > keyBytes.Length - 1)
				{
					keyIndex = 0;
				}

				var permIndex = Convert.ToInt32(keyBytes[keyIndex]);

				if (permIndex > plainText.Data.Length - 1)
				{
					continue;
				}

				plainText.ModifyBlock(plainText.Data.Switch(i, permIndex));
			}

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

			if (round % 2 == 0)
			{
				key.ShiftRightCustom(2);
			}
			else
			{
				key.ShiftRightCustom(1);
			}

			return Task.FromResult(new RoundResult(plainText, key));
		}

		public Task<RoundResult> Encrypt(Block plainText, BitArray key, int round)
		{
			// Abwechselnd 1 und 2 Stellen shiften und manuell 1 oder 2 Stellen nachbessern die sonst verloren gehn.
			if (round % 2 == 0)
			{
				key.ShiftLeftCustom(2);
			}
			else
			{
				key.ShiftLeftCustom(1);
			}

			// Idee: ich schaue mir den Key an und loope durch, überall dort wo ein Byte Wert gleich einem integer index Wert in der permutation Tabelle ist
			if (permutationTable.Length != plainText.Data.Length)
			{
				throw new ArgumentException(nameof(plainText), $"Block size did not match table size of {permutationTable.Length}");
			}
			
			for (int i = 0; i < permutationTable.Length; i++)
			{
				plainText.ModifyBlock(plainText.Data.Switch(i, permutationTable[i]));
			}

			int keyIndex;
			var keyBytes = key.ToByteArray();

			for (int i = 0; i < plainText.Data.Length; i++)
			{
				keyIndex = i;

				if (keyIndex > keyBytes.Length - 1)
				{
					keyIndex = 0;
				}

				var permIndex = Convert.ToInt32(keyBytes[keyIndex]);

				if (permIndex > plainText.Data.Length - 1)
				{
					continue;
				}

				plainText.ModifyBlock(plainText.Data.Switch(i, permIndex));
			}

			return Task.FromResult(new RoundResult(plainText, key));
		}
	}
}
