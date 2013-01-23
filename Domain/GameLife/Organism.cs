using System;
using System.Collections;
using Domain.Support;

namespace Domain.GameLife
{
    public class Organism
    {
        #region Variable

        private int _age;
        private readonly byte _genome;

        #endregion

        #region Init

        public Organism(byte genome)
        {
            _age = 0;
            _genome = genome;
        }

        #endregion

        #region Public

        public int Age
        {
            get { return _age; }
        }

        public void Update()
        {
            _age++;
        }

        #endregion

        #region Private

        /*private void CreateGenomeWithByte(byte b, ref byte[] genome)
        {
            var bitArray = new BitArray(new[] {b});
            for (int i = 0; i < 8; i++)
            {
                genome[i] = bitArray[i] ? (byte) 1 : (byte) 0;
            }
        }*/

        #endregion
    }

    
}