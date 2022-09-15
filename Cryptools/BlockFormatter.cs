namespace Cryptools
{
    public class BlockFormatter
    {
        private int blockSize;

        public BlockFormatter(int blockSize)
        {
            this.BlockSize = blockSize;
        }

        public int BlockSize
        {
            get
            {
                return blockSize;
            }
            set 
            { 
                blockSize = 
                    (value > 0 && Math.Sqrt(value) == 2)
                    ? value 
                    : throw new ArgumentOutOfRangeException("Block size must be greater than 0 and a power of 2"); 
            }
        }

        public void Transform()
    }
}
