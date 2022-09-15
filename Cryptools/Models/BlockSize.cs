namespace Cryptools.Models
{
    public class BlockSize
    {
        private int blockSize;

        public BlockSize(int size)
        {
            this.Size = size;
        }

        public int Size
        {
            get
            {
                return blockSize;
            }
            set
            {
                blockSize =
                    value > 0 && Math.Sqrt(value) == 2
                    ? value
                    : throw new ArgumentOutOfRangeException("Block size must be greater than 0 and a power of 2");
            }
        }
    }
}
