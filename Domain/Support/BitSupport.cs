namespace Domain.Support
{
    /// <summary>
    /// Class for support
    /// </summary>
    public static class BitSupport
    {
        /// <summary>
        /// Get bit value of byte data
        /// </summary>
        /// <param name="b">Input byte data</param>
        /// <param name="bitNumber">Number of bit: 1..8</param>
        /// <returns><see cref="bool"/></returns>
        public static bool GetBit(this int b, int bitNumber)
        {
            return (b & (1 << bitNumber - 1)) != 0;
        }
    }
}