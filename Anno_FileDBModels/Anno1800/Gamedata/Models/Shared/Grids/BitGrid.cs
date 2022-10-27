namespace Anno_FileDBModels.Anno1800.Gamedata.Models.Shared.Grids
{
    public class BitGrid
    {
        public BitGrid()
        {
            x = 0;
            y = 0;

            bits = new byte[0];
        }

        public BitGrid(int size) : this(size, size)
        {

        }

        public BitGrid(int xSize, int ySize)
        {
            x = xSize;
            y = ySize;

            bits = new byte[xSize * ySize / 8];
        }

        public int x { get; set; }
        public int y { get; set; }

        public byte[] bits { get; set; }

        public bool GetBit(int indexX, int indexY)
        {
            //First Index Correct byte in bits[]
            //Then mask the correct bit in that byte
            //
            return (bits[(indexY * x + indexX) / 8] & (byte)(1 << (indexY * x + indexX) % 8)) > 0;
        }

        public void SetBit(int indexX, int indexY, bool value)
        {
            byte[] trueMasks = new byte[8] { 0b0000_0001, 0b0000_0010, 0b0000_0100, 0b0000_1000, 0b0001_0000, 0b0010_0000, 0b0100_0000, 0b1000_0000 };
            byte[] falseMasks = new byte[8] { 0b1111_1110, 0b1111_1101, 0b1111_1011, 0b1111_0111, 0b1110_1111, 0b1101_1111, 0b1011_1111, 0b0111_1111 };

            if (value == true)
            {
                bits[(indexY * x + indexX) / 8] = (byte)(bits[(indexY * x + indexX) / 8] | trueMasks[(indexY * x + indexX) % 8]);
            }
            else
            {
                bits[(indexY * x + indexX) / 8] = (byte)(bits[(indexY * x + indexX) / 8] & falseMasks[(indexY * x + indexX) % 8]);
            }
        }
    }
}
