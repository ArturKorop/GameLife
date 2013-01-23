using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Domain.Support;

namespace Domain.GameLife
{
    public class Organism
    {
        #region Variable

        private int _age;
        private readonly byte _genome;
        private int _x;
        private int _y;

        #endregion

        #region Init

        public Organism(byte genome,int x, int y)
        {
            _x = x;
            _y = y;
            _age = 0;
            _genome = genome;
        }

        #endregion

        #region Public

        public int Age
        {
            get { return _age; }
        }

        public byte Genome
        {
            get { return _genome; }
        }

        public void Update(IEnumerable<Cell> neighborCells, int countNeighbors, Cell currentCell)
        {
            _age++;

            if (countNeighbors < 2 || countNeighbors > 3)
            {
                currentCell.SetCellStatus(OrganismStatus.Dead);
            }
            else
            {
                currentCell.SetCellStatus(OrganismStatus.Live);
            }
        }

        public PrepareResult Prepare(Collection<Cell> neighborCells)
        {
            var array = new Cell[5,5];
            var count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!(i == j && i == 2))
                    {
                        array[i, j] = neighborCells[count];
                        count++;
                    }
                }
            }
            var tempResult = new Collection<PrepareResult>();
            foreach (var neighborNeighborCell in neighborCells)
            {
                //var count = neighborNeighborCell.Count(item => item.Status == OrganismStatus.Born || item.Status == OrganismStatus.Live || item.Status == OrganismStatus.Create) - 1;
                for (int i = 0; i < 8; i++)
                {
                  //  if (_genome.GetBit(i) && count == i + 1)
                 //   {
                       // tempResult.Add((new PrepareResult()).SetMigration(new Coordinate{ X = _x, Y = _y},new Coordinate{X = }));
                 //   }
                    
                }
            }
            return new PrepareResult();
        }

        #endregion

        #region Private

        #endregion
    }

    
}