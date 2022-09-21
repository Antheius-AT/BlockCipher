﻿// Copyright file="Substitutor.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Modules
{
	using Cryptools.Interfaces;
	using Cryptools.Models;
	using System.Collections;
	using System.Threading.Tasks;

	public class Substitutor : ICryptoModule
	{
		private BitArray substitutionTable;

		public Substitutor()
		{
		}

		public Task<RoundResult> Decrypt(Block plainText, BitArray key, int round)
		{
			throw new NotImplementedException();
		}

		public Task<RoundResult> Encrypt(Block plainText, BitArray key, int round)
		{
			throw new NotImplementedException();
		}
	}
}
