using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Domain.Support;

namespace Domain.GameLife
{
    /// <summary>
    /// Class that provides a description of organism
    /// </summary>
    public class Organism
    {
        #region Variable

        /// <summary>
        ///  Age of organism
        /// </summary>
        private int _age;

        /// <summary>
        /// Genome of organism
        /// </summary>
        private readonly int _genome;

        /// <summary>
        /// Coordinate x of organism
        /// </summary>
        private int _x;

        /// <summary>
        /// Coordinate y of organism
        /// </summary>
        private int _y;
        /// <summary>
        /// Is was migration
        /// </summary>
        private bool _migrant;

        #endregion

        #region Init

        /// <summary>
        /// Constructor of organism
        /// </summary>
        /// <param name="genome">Genome: 0..511</param>
        /// <param name="x">Coordinate x</param>
        /// <param name="y">Coordinate y</param>
        public Organism(int genome, int x, int y)
        {
            _x = x;
            _y = y;
            _age = 0;
            _genome = genome;
        }

        #endregion

        #region Public

        /// <summary>
        /// Age of organism
        /// </summary>
        public int Age
        {
            get { return _age; }
        }

        /// <summary>
        /// Genome of organism
        /// </summary>
        public int Genome
        {
            get { return _genome; }
        }

        /// <summary>
        /// Is was migration
        /// </summary>
        public bool Migrant
        {
            get { return _migrant; }
        }

        /// <summary>
        /// Calculate next step of organism life
        /// </summary>
        /// <param name="neighborCells">Nearest neighbor <see cref="Cell"/> of organism - 8</param>
        /// <param name="countNeighbors">Count of nearest life organism</param>
        /// <param name="currentCell">Current cell</param>
        public void Update(IEnumerable<Cell> neighborCells, int countNeighbors, Cell currentCell)
        {
            _age++;
            _migrant = false;
            if (countNeighbors < 2 || countNeighbors > 3)
            {
                currentCell.SetCellStatus(OrganismStatus.Dead);
            }
            else
            {
                currentCell.SetCellStatus(OrganismStatus.Live);
            }
        }

        /// <summary>
        /// Prepare to next step
        /// </summary>
        /// <param name="neighborCells">Nearest neighbor <see cref="Cell"/> of organism - 24</param>
        /// <returns><see cref="PrepareResult"/></returns>
        public PrepareResult Prepare(Collection<Cell> neighborCells)
        {
            var result = new PrepareResult();
            if (!_migrant)
            {
                var array = new Cell[5, 5];
                var migrationMaybe = new Collection<Cell>();
                var dictionary = new Dictionary<Cell, int>();

                CreateArray5X5(neighborCells, array);
                if (IsNeedMigration(array))
                {
                    CreateDictionaryOfCountNeighbor(array, dictionary);
                    CreateAvaliableMigration(dictionary, migrationMaybe);


                    if (migrationMaybe.Count > 0)
                    {
                        var rand = new Random(Environment.TickCount + migrationMaybe.Count());
                        var migration = migrationMaybe[rand.Next(0, migrationMaybe.Count)];
                        var from = new Coordinate {X = _x, Y = _y};
                        var to = new Coordinate {X = migration.X, Y = migration.Y};
                        _x = to.X;
                        _y = to.Y;
                        _migrant = true;
                        result.SetMigration(from, to, this);
                    }
                }
            }
            return result;
        }

        #endregion

        #region Private

        /// <summary>
        /// Check is migration is need
        /// </summary>
        /// <param name="array">Array of neighboe cell</param>
        /// <returns><see cref="bool"/></returns>
        private bool IsNeedMigration(Cell[,] array)
        {
            var countNeighbor = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (!((i == j) && (i == 0)) && array[2 + i, 2 + j].Organism != null)
                    {
                        countNeighbor++;
                    }
                }
            }
            for (int i = 1; i < 10; i++)
            {
                if (_genome.GetBit(i))
                {
                    if (countNeighbor == i - 1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Create collection of avaliable migration path
        /// </summary>
        /// <param name="dictionary">Input data</param>
        /// <param name="migrationMaybe">Output data</param>
        private void CreateAvaliableMigration(Dictionary<Cell, int> dictionary, Collection<Cell> migrationMaybe)
        {
            for (int i = 1; i < 10; i++)
            {
                if (_genome.GetBit(i))
                {
                    foreach (var item in dictionary)
                    {
                        if (item.Value == (i - 1))
                        {
                            migrationMaybe.Add(item.Key);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Create collection of neighbor cell with count of they neighbor
        /// </summary>
        /// <param name="array">Array of all neighbor</param>
        /// <param name="dictionary">output data</param>
        private void CreateDictionaryOfCountNeighbor(Cell[,] array, Dictionary<Cell, int> dictionary)
        {
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
                        dictionary.Add(temp, countOrganism);
                    }
                }
            }
        }

        /// <summary>
        /// Convert collection of neigbor to array 5 x 5
        /// </summary>
        /// <param name="neighborCells">Collection of neighbors</param>
        /// <param name="array">Output data</param>
        private void CreateArray5X5(Collection<Cell> neighborCells, Cell[,] array)
        {
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
        }

        #endregion
    }
}