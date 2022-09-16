// Copyright file="CipherBlockChainMode.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Models.CipherModes
{
	using System.Threading.Tasks;
	using Cryptools.Interfaces;

	public class CipherBlockChainMode : ICipherModeVisitable
	{
		public async Task Accept(ICipherModeVisitor visitor)
		{
			await visitor.Visit(this);
		}

		public byte[] GetResult()
		{
			throw new NotImplementedException();
		}
	}
}
