using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.GameLife
{
    public class ModelGameLife
    {
        private int _widthField = 10;
        private int _heightField = 10;
        private readonly Cell[,] _arrayCell;

        #region Initialize

        public ModelGameLife()
        {
            _arrayCell = new Cell[_widthField, _heightField];
            InitGameLifeEngine();
        }

        public ModelGameLife(int widthField, int heightField)
        {
            _widthField = widthField;
            _heightField = heightField;
            _arrayCell = new Cell[_widthField, _heightField];
            InitGameLifeEngine();
        }

        public void Update()
        {
            GameLifeEngineNextStep();
        }
        
        private void InitGameLifeEngine()
        {
            var rand = new Random();
            for (int i = 0; i < _widthField; i++)
            {
                for (int j = 0; j < _heightField; j++)
                {
                    _arrayCell[i,j] = new Cell(i,j);
                    var t = rand.Next(2);
                    if(t > 0.5)
                        _arrayCell[i,j].SetOrganismStatus(OrganismStatus.Born);
                }
            }
        }

        private void GameLifeEngineNextStep()
        {
            for (int i = 0; i < _widthField; i++)
            {
                for (int j = 0; j < _heightField; j++)
                {
                    var tempCountNeighborLiveOrganism = GetCountNeighborLiveOrganism(_arrayCell[i, j]);
                    if (tempCountNeighborLiveOrganism < 2 || tempCountNeighborLiveOrganism > 3)
                    {
                        _arrayCell[i, j].SetOrganismStatus(OrganismStatus.Dead);
                    }
                    else if (tempCountNeighborLiveOrganism == 3)
                    {
                        _arrayCell[i, j].SetOrganismStatus(OrganismStatus.Born);
                    }
                    else
                    {
                        _arrayCell[i, j].SetOrganismStatus(OrganismStatus.Live);
                    }
                }
            }
        }

        private int GetCountNeighborLiveOrganism(Cell cell)
        {
            var tempNeighbor = GetNeighborCell(cell);
            return tempNeighbor.Count(item => item.Status == OrganismStatus.Born || item.Status == OrganismStatus.Live);
        }

        private IEnumerable<Cell> GetNeighborCell(Cell cell)
        {
            var list = new List<Cell>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i != j)
                    {
                        list.Add(GetCell(cell.X - 1 + i, cell.Y - 1 + j));
                    }
                }
            }
            return list;
        }

        public Cell GetCell(int i, int j)
        {
            var iTemp = 0;
            var jTemp = 0;

            if (iTemp < 0)
            {
                iTemp = _widthField - iTemp;
            }
            else if (iTemp > _widthField - 1)
            {
                iTemp = (_widthField - 1)%iTemp;
            }
            else
            {
                iTemp = i;
            }
            if (jTemp < 0)
            {
                jTemp = _heightField - jTemp;
            }
            else if (jTemp > _heightField - 1)
            {
                jTemp = (_heightField - 1) % jTemp;
            }
            else
            {
                jTemp = j;
            }

            return _arrayCell[iTemp, jTemp];
        }

        #endregion

        #region Public

        public Cell[,] Array
        {
            get { return _arrayCell; }
        }

        public int WidthField
        {
            get { return _widthField; }
            set { _widthField = value; }
        }

        public int HeightField
        {
            get { return _heightField; }
            set { _heightField = value; }
        }

        #endregion

    }
}