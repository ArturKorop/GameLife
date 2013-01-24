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
            var dictionary = new Dictionary<Cell,int>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var temp = array[2 + i, 2 + j];
                    if (temp != null && temp.Organism == null)
                    {
                        var countOrganism = 0;
                        for (int k = -1; k < 2; k++)
                        {
                            for (int l = -1; l < 2; l++)
                            {
                                var temp2 = array[2 + i + k, 2 + j + l];
                                if (temp2 != null && temp2.Organism != null)
                                {
                                    countOrganism++;
                                }
                            }
                        }
                        if (temp.Organism != null)
                        {
                            countOrganism--;
                        }
                        dictionary.Add(temp,countOrganism);
                    }
                }
            }
            var migrationMaybe = new Collection<Cell>();
            for (int i = 1; i < 9; i++)
            {
                if (Genome.GetBit(i))
                {
                    foreach (var item in dictionary)
                    {
                        if (item.Value == (i))
                        {
                            migrationMaybe.Add(item.Key);
                        }
                    }
                }
            }
            var result = new PrepareResult();
            if (migrationMaybe.Count > 0)
            {
                var rand = new Random(Environment.TickCount + migrationMaybe.Count());
                var migration = migrationMaybe[rand.Next(0, migrationMaybe.Count)];
                var from = new Coordinate {X = _x, Y = _y};
                var to = new Coordinate {X = migration.X, Y = migration.Y};
                _x = to.X;
                _y = to.Y;
                result.SetMigration(from, to, this);
            }
            return result;
        }

        #endregion

        #region Private

        #endregion
    }

    
}