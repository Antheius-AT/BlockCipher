// Copyright file="Substitutor.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Modules
{
	using Cryptools.Extensions;
	using Cryptools.Interfaces;
	using Cryptools.Models;
	using System.Collections;
	using System.Threading.Tasks;

	public class Substitutor : ICryptoModule
	{
		private List<BitArray> previousSubstitutions;
		private char[] substitutionTable;

		public Substitutor(char[] substitutionTable)
		{
			this.substitutionTable = substitutionTable;
			previousSubstitutions = new List<BitArray>();
		}

		public Task<RoundResult> Decrypt(Block plainText, BitArray key, int round)
		{
			// Inspired by feistel, everything that is required to reverse the un-reversable operation is to use
			// the fact that XOR is its own reverse
			var previousSubstitution = previousSubstitutions.Last();
			previousSubstitutions.Remove(previousSubstitution);
			plainText.Data.Xor(previousSubstitution);

			return Task.FromResult(new RoundResult(plainText, key));
		}

		public Task<RoundResult> Encrypt(Block plainText, BitArray key, int round)
		{
			var unchangedPlaintextBytes = plainText.Data.ToByteArray();
			var plainTextBytes = plainText.Data.ToByteArray();
			var keyBytes = key.ToByteArray();

			IncorporateKey(keyBytes);

			for (int i = 0; i < plainTextBytes.Length; i++)
			{
				var subChar = substitutionTable[Convert.ToInt32(plainTextBytes[i])];

				plainTextBytes[i] = (byte)subChar;
			}

			// Inspired by Feistel. Use the fact that XOR is its own reverse: B + S(B) + S(B) = B
			plainText.ModifyBlock(new BitArray(plainTextBytes));

			var xorResult = new BitArray(unchangedPlaintextBytes).Xor(plainText.Data);
			previousSubstitutions.Add(plainText.Data);
			
			plainText.ModifyBlock(xorResult);
			
			return Task.FromResult(new RoundResult(plainText, key));
		}

		private void IncorporateKey(byte[] keyBytes)
		{
			foreach (var item in keyBytes)
			{
				var currentSubChar = substitutionTable[item];
				var sum = (int)currentSubChar + Convert.ToInt32(item);

				var newValue = sum < 256
					? sum
					: sum % 256;

				substitutionTable[item] = (char)newValue;
			}
		}
	}
}
