// Copyright file="EncryptionInformation.cs" Fachhochschule Technikum Wien (FHTW).
// Licensed under the MIT license.
// Author "Gregor Faiman"

namespace Cryptools.Models
{
	public class EncryptionInformation
    {
        public EncryptionInformation(SymmetricKey symmetricKey)
        {
            this.SymmetricKey = symmetricKey ?? throw new ArgumentNullException(nameof(symmetricKey), "Key must not be null");
        }

        public SymmetricKey SymmetricKey { get; set; }
    }
}
