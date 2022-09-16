namespace Cryptools.Models
{
    public class Block
    {
        public Block(byte[] data, byte[]? padding = null)
        {
            this.Data = data;
            this.Padding = padding;
        }

        public BlockSize BlockSize
        {
            get
            {
                return new BlockSize(Data.Length + Padding.Length);
            }
        }

        public byte[] Data { get; }

        public byte[]? Padding { get; }
    }
}
