namespace Cryptools
{
    using Cryptools.Interfaces;

    public class SymmetricKey : ICryptographicKey
    {
        public SymmetricKey(byte[] keyBytes)
        {
            this.KeyBytes = keyBytes;
        }

        public byte[] KeyBytes { get; set; }
    }
}