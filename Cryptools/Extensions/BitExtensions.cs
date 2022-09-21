// Copyright file="BitExtensions.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptools.Extensions
{
	public static class BitExtensions
	{
		public static byte[] ToByteArray(this BitArray src)
		{
			if (src.Length == 0 || src.Length % 8 != 0)
			{
				throw new ArgumentException(nameof(src), "Bit array must be a factor of 8 in length to fully transform it into an array of bytes");
			}

			var byteList = new List<byte>();
			var bitList = new List<bool>();

			for (int i = 0; i <= src.Length; i++)
			{
				if (i != 0 && i % 8 == 0)
				{
					byteList.Add(ToSingleByte(new BitArray(bitList.ToArray())));
					bitList.Clear();
				}

				if (i == src.Length)
				{
					continue;
				}

				bitList.Add(src[i]);
			}

			return byteList.ToArray();
		}

		public static byte ToSingleByte(this BitArray src)
		{
			if (src.Length != 8)
			{
				throw new ArgumentException(nameof(src), "Bit array must be of length 8 to convert to single byte");
			}

			byte[] bytes = new byte[1];
			src.CopyTo(bytes, 0);

			return bytes.First();
		}
	}
}
