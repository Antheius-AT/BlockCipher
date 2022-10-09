// Copyright file="ICryptographicKey.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Interfaces
{
    public interface ICryptographicKey
    {
        byte[] KeyBytes { get; }
    }
}
