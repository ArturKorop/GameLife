using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Script.Serialization;
using Domain.Support;

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
        /// <summary>
        /// Age of model
        /// </summary>
        private int _age;

        #endregion

        #region Initialize

        /// <summary>
        /// Constructor without parameters of <see cref="ModelGameLife"/>
        /// </summary>
        public ModelGameLife()
        {
            _arrayCell = new Cell[_widthField, _heightField];
            _age = 0;
            InitGameLifeEngine();
        }
        /// <summary>
        /// Constructor with parameters of <see cref="ModelGameLife"/>
        /// </summary>
        /// <param name="widthField">Width of field</param>
        /// <param name="heightField">Height of field</param>
        public ModelGameLife(int widthField, int heightField)
        {
            if (widthField != 0 || heightField != 0)
            {
                _age = 0;
                _widthField = widthField;
                _heightField = heightField;
                _arrayCell = new Cell[_widthField,_heightField];
                InitGameLifeEngine();
            }
            else
            {
                _age = 0;
                _widthField = 10;
                _heightField = 10;
                _arrayCell = new Cell[_widthField, _heightField];
                InitGameLifeEngineTest();
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Update next step of game
        /// </summary>
        public void Update()
        {
            _age++;
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
        /// <summary>
        /// Age of model
        /// </summary>
        public int Age
        {
            get { return _age; }
        }

        /// <summary>
        /// Convert object of this class to JSON
        /// </summary>
        /// <returns>String</returns>
        public string ToJSON()
        {
            var serializer = new JavaScriptSerializer();
            var temp = serializer.Serialize(this);
            return temp;
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
                        _arrayCell[i, j].SetCellStatus(OrganismStatus.Create);

                }
            }
        }
        /// <summary>
        /// Set begin status of game for testing
        /// </summary>
        private void InitGameLifeEngineTest()
        {
            for (int i = 0; i < _widthField; i++)
            {
                for (int j = 0; j < _heightField; j++)
                {
                    _arrayCell[i, j] = new Cell(i, j);
                }
            }
            _arrayCell[2, 2].SetCellStatus(OrganismStatus.Create);
            _arrayCell[2, 3].SetCellStatus(OrganismStatus.Create);
            _arrayCell[2, 4].SetCellStatus(OrganismStatus.Create);

            _arrayCell[5, 5].SetCellStatus(OrganismStatus.Create);
            _arrayCell[5, 6].SetCellStatus(OrganismStatus.Create);
            _arrayCell[5, 7].SetCellStatus(OrganismStatus.Create);
            _arrayCell[4, 7].SetCellStatus(OrganismStatus.Create);

            _arrayCell[9, 7].SetCellStatus(OrganismStatus.Create);
            _arrayCell[9, 8].SetCellStatus(OrganismStatus.Create);
            _arrayCell[0, 7].SetCellStatus(OrganismStatus.Create);
            _arrayCell[0, 8].SetCellStatus(OrganismStatus.Create);
        }
        /// <summary>
        /// Claculate next step of game
        /// </summary>
        private void GameLifeEngineNextStep()
        {
            var tempArray = new Cell[_widthField,_heightField];
            //InitArrayCell(ref tempArray);
            for (int i = 0; i < _widthField; i++)
            {
                for (int j = 0; j < _heightField; j++)
                {
                    //var prepareResult = _arrayCell[i, j].Prepare(GetNeighborCell(_arrayCell[i, j]));
                    var neighbor8 = GetNeighborCell(_arrayCell[i, j], 8);
                   // var neighbor24 = GetNeighborCell(_arrayCell[i, j], 24);

                    var temp = _arrayCell[i, j].Clone();
                    _arrayCell[i, j].NextStep(neighbor8);
                    tempArray[i, j] = _arrayCell[i, j];
                    _arrayCell[i, j] = temp;
                }
            }
            _arrayCell = tempArray;
        }
        /// <summary>
        /// Claculate prepare to next step of game
        /// </summary>
        private void GameLifeEnginePrepare()
        {
            //InitArrayCell(ref tempArray);
            for (int i = 0; i < _widthField; i++)
            {
                for (int j = 0; j < _heightField; j++)
                {
                    var neighbor24 = GetNeighborCell(_arrayCell[i, j], 24);
                    var result = _arrayCell[i, j].Prepare(neighbor24);
                    if (result.Migration.IsMigrartion)
                    {
                        _arrayCell[result.Migration.From.X,result.Migration.From.Y].SetCellStatus(OrganismStatus.Empty);
                        _arrayCell[result.Migration.To.X,result.Migration.To.Y].SetMigration(result.Migration.Organism);
                    }
                }
            }
        }
        /// <summary>
        /// Calculate live and born neighbor of cell
        /// </summary>
        /// <param name="cell">Current cell</param>
        /// <returns>Count of neighbor</returns>
        private int GetCountNeighborLiveOrganism(Cell cell)
        {
            var tempNeighbor = GetNeighborCell(cell, 8);
            return tempNeighbor.Count(item => item.Status == OrganismStatus.Born || item.Status == OrganismStatus.Live);
        }

        /// <summary>
        /// Create <see cref="IEnumerable{T}"/> of 8 or 24 neighbor cell for current cell
        /// </summary>
        /// <param name="cell">Current cell</param>
        /// <param name="neighborCount">Count of neighbor cell: 8, 24, 48</param>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="Cell"/></returns>
        private Collection<Cell> GetNeighborCell(Cell cell, int neighborCount)
        {
            int sizeOfNeighborArea;
            switch (neighborCount)
            {
                case 8:
                    sizeOfNeighborArea = 3;
                    break;
                case 24:
                    sizeOfNeighborArea = 5;
                    break;
                default:
                    sizeOfNeighborArea = 3;
                    break;
            }
            int different = (sizeOfNeighborArea - 1)/2;

            var list = new Collection<Cell>();
            for (int i = 0; i < sizeOfNeighborArea; i++)
            {
                for (int j = 0; j < sizeOfNeighborArea; j++)
                {
                    if (!(i == j && i == different))
                    {
                        list.Add(GetCell(cell.X - different + i, cell.Y - different + j));
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
                iTemp = iTemp % (_widthField);
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
                jTemp = jTemp % (_heightField - 1);
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