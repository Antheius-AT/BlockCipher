// Copyright file="BitExtensions.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

using System.Collections;

namespace Cryptools.Extensions
{
	public static class BitExtensions
	{
		public static BitArray ShiftLeftCustom(this BitArray src, int amount)
		{
			for (int i = 1; i <= amount; i++)
			{
				src = src.ShiftLeftOnce();
			}

			return src;
		}

		public static BitArray ShiftRightCustom(this BitArray src, int amount)
		{
			for (int i = 1; i <= amount; i++)
			{
				src = src.ShiftRightOnce();
			}

			return src;
		}

		public static BitArray Switch(this BitArray src, int firstIndex, int secondIndex)
		{
			var currentValue = src[firstIndex];

			src[firstIndex] = src[secondIndex];
			src[secondIndex] = currentValue;

			return src;
		}

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

		private static BitArray ShiftLeftOnce(this BitArray src)
		{
			int shiftIndex;
			var lastElement = src[src.Length - 1];

			for (int i = 0; i < src.Length; i++)
			{
				shiftIndex = i - 1 >= 0
					? i - 1
					: src.Length - 1;

				src[shiftIndex] = i == src.Length - 1 ? lastElement : src[i];
			}

			return src;
		}

		private static BitArray ShiftRightOnce(this BitArray src)
		{
			int shiftIndex;
			var firstElement = src[0];

			for (int i = src.Length - 1; i >= 0; i--)
			{
				shiftIndex = i + 1 < src.Length
					? i + 1
					: 0;

				src[shiftIndex] = i == 0 ? firstElement : src[i];
			}

			return src;
		}
	}
}
