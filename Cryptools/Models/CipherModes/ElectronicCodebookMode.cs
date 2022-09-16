// Copyright file="ElectronicCodebookMode.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Models.CipherModes
{
	using Cryptools.Interfaces;

	public class ElectronicCodebookMode : ICipherModeVisitable
	{
		public ElectronicCodebookMode(Block block)
		{
			this.Block = block;
		}

		public Block Block { get; }

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
