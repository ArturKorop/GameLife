namespace Domain.Support
{
    public static class BitSupport
    {
        public static bool GetBit(this byte b, int bitNumber)
        {
            return (b & (1 << bitNumber - 1)) != 0;
        }
    }
}