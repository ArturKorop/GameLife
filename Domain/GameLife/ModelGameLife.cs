using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.GameLife
{
    /// <summary>
    /// Class, that provides description of Game "Life"
    /// </summary>
    public class ModelGameLife
    {
        #region Variable
        /// <summary>
        /// Width of field
        /// </summary>
        private int _widthField = 10;
        /// <summary>
        /// Height of field
        /// </summary>
        private int _heightField = 10;
        /// <summary>
        /// Array of cells on field in game
        /// </summary>
        private Cell[,] _arrayCell;

        #endregion

        #region Initialize

        /// <summary>
        /// Constructor without parameters of <see cref="ModelGameLife"/>
        /// </summary>
        public ModelGameLife()
        {
            _arrayCell = new Cell[_widthField, _heightField];
            InitGameLifeEngine();
        }
        /// <summary>
        /// Constructor with parameters of <see cref="ModelGameLife"/>
        /// </summary>
        /// <param name="widthField">Width of field</param>
        /// <param name="heightField">Height of field</param>
        public ModelGameLife(int widthField, int heightField)
        {
            _widthField = widthField;
            _heightField = heightField;
            _arrayCell = new Cell[_widthField, _heightField];
            InitGameLifeEngine();
        }

        #endregion

        #region Public

        /// <summary>
        /// Update next step of game
        /// </summary>
        public void Update()
        {
            GameLifeEngineNextStep();
        }
        /// <summary>
        /// Array of cells on field in game
        /// </summary>
        public Cell[,] Array
        {
            get { return _arrayCell; }
        }
        /// <summary>
        /// Width of field
        /// </summary>
        public int WidthField
        {
            get { return _widthField; }
            set { _widthField = value; }
        }
        /// <summary>
        /// Height of field
        /// </summary>
        public int HeightField
        {
            get { return _heightField; }
            set { _heightField = value; }
        }

        #endregion

        #region Private
        /// <summary>
        /// Set begin status of game
        /// </summary>
        private void InitGameLifeEngine()
        {
            var rand = new Random();
            for (int i = 0; i < _widthField; i++)
            {
                for (int j = 0; j < _heightField; j++)
                {
                    _arrayCell[i, j] = new Cell(i, j);
                    var t = rand.Next(2);
                    if (t < 1)
                        _arrayCell[i, j].SetOrganismStatus(OrganismStatus.Born);

                }
            }
        }
        /// <summary>
        /// Claculate next step of game
        /// </summary>
        private void GameLifeEngineNextStep()
        {
            var tempArray = new Cell[_widthField,_heightField];
            InitArrayCell(ref tempArray);
            for (int i = 0; i < _widthField; i++)
            {
                for (int j = 0; j < _heightField; j++)
                {
                    var tempCountNeighborLiveOrganism = GetCountNeighborLiveOrganism(_arrayCell[i, j]);
                    tempArray[i, j] = _arrayCell[i, j];
                    if (tempCountNeighborLiveOrganism < 2 || tempCountNeighborLiveOrganism > 3)
                    {
                        if (_arrayCell[i, j].Status == OrganismStatus.Empty || _arrayCell[i, j].Status == OrganismStatus.Dead)
                        {
                            tempArray[i, j].SetOrganismStatus(OrganismStatus.Empty);
                        }
                        else
                        {
                            tempArray[i, j].SetOrganismStatus(OrganismStatus.Dead);
                        }
                    }
                    else if (tempCountNeighborLiveOrganism == 3)
                    {
                        if (_arrayCell[i, j].Status == OrganismStatus.Born || _arrayCell[i, j].Status == OrganismStatus.Live)
                        {
                            tempArray[i, j].SetOrganismStatus(OrganismStatus.Live);
                        }
                        else
                        {
                            tempArray[i, j].SetOrganismStatus(OrganismStatus.Born);
                        }
                    }
                    else
                    {
                        if (_arrayCell[i, j].Status == OrganismStatus.Born ||
                            _arrayCell[i, j].Status == OrganismStatus.Live)
                        {
                            tempArray[i, j].SetOrganismStatus(OrganismStatus.Live);
                        }
                        else
                        {
                            tempArray[i, j].SetOrganismStatus(OrganismStatus.Empty);
                        }
                    }
                }
            }
            _arrayCell = tempArray;
        }
        /// <summary>
        /// Calculate live and born neighbor of cell
        /// </summary>
        /// <param name="cell">Current cell</param>
        /// <returns>Count of neighbor</returns>
        private int GetCountNeighborLiveOrganism(Cell cell)
        {
            var tempNeighbor = GetNeighborCell(cell);
            return tempNeighbor.Count(item => item.Status == OrganismStatus.Born || item.Status == OrganismStatus.Live);
        }
        /// <summary>
        /// Create <see cref="IEnumerable{T}"/> of neighbor cell for current cell
        /// </summary>
        /// <param name="cell">Current cell</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Cell"/></returns>
        private IEnumerable<Cell> GetNeighborCell(Cell cell)
        {
            var list = new List<Cell>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!(i == j && i == 1))
                    {
                        list.Add(GetCell(cell.X - 1 + i, cell.Y - 1 + j));
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// Return <see cref="Cell"/> for coordinats
        /// </summary>
        /// <param name="i">X</param>
        /// <param name="j">Y</param>
        /// <returns><see cref="Cell"/></returns>
        public Cell GetCell(int i, int j)
        {
            var iTemp = i;
            var jTemp = j;

            if (iTemp < 0)
            {
                iTemp = _widthField + iTemp;
            }
            else if (iTemp > _widthField - 1)
            {
                iTemp = _widthField % iTemp;
            }
            else
            {
                iTemp = i;
            }
            if (jTemp < 0)
            {
                jTemp = _heightField + jTemp;
            }
            else if (jTemp > _heightField - 1)
            {
                jTemp = _heightField % jTemp;
            }
            else
            {
                jTemp = j;
            }

            return _arrayCell[iTemp, jTemp];
        }
        /// <summary>
        /// Create array of empty <see cref="Cell"/>
        /// </summary>
        /// <param name="array"></param>
        private void InitArrayCell(ref Cell[,] array)
        {
            for (int i = 0; i < _widthField; i++)
            {
                for (int j = 0; j < _heightField; j++)
                {
                    array[i,j] = new Cell(i,j);
                }
            }
        }

        #endregion

    }
}