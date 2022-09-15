using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
