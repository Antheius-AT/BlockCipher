// Copyright file="SymmetricKey.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Models
{
    using Cryptools.Interfaces;

    public class SymmetricKey : ICryptographicKey
    {
        public SymmetricKey(byte[] keyBytes)
        {
            this.KeyBytes = keyBytes;
        }

        public int KeySize => KeyBytes.Length;

        public byte[] KeyBytes { get; set; }
    }
}