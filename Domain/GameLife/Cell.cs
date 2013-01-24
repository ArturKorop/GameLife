using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Domain.Support;

namespace Domain.GameLife
{
    public class Cell
    {
        #region Variable

        private readonly int _x;
        private readonly int _y;
        private OrganismStatus _status = OrganismStatus.Empty;
        private Organism _cellOrganism = null;

        #endregion

        #region Initialize

        public Cell(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public Cell(Cell cell)
        {
            _x = cell.X;
            _y = cell.Y;
            _status = cell.Status;
            _cellOrganism = cell.Organism;
        }

        #endregion

        #region Public

        public void SetCellStatus(OrganismStatus status)
        {
            _status = status;
            switch (status)
            {
                case OrganismStatus.Born:
                    OrganismBorn();
                    break;
                case OrganismStatus.Live:
                    OrganismLive();
                    break;
                case OrganismStatus.Dead:
                    OrganismDead();
                    break;
                case OrganismStatus.Empty:
                    CellEmpty();
                    break;
                case OrganismStatus.Create:
                    OrganismCreate();
                    break;
            }
        }

        public PrepareResult Prepare(Collection<Cell> neighborCells)
        {
            if (_cellOrganism != null)
            {
                return _cellOrganism.Prepare(neighborCells);
            }
            return new PrepareResult();
        }

        public void NextStep(IEnumerable<Cell> neighborCells)
        {
            var enumerable = neighborCells as Cell[] ?? neighborCells.ToArray();
            var countNeighbors = enumerable.Count(item => item.Status == OrganismStatus.Born || item.Status == OrganismStatus.Live || item.Status == OrganismStatus.Create);
            if (_cellOrganism == null)
            {
                if (countNeighbors == 3)
                {
                    SetCellStatus(OrganismStatus.Born);
                }
                else if(_status == OrganismStatus.Dead)
                {
                    SetCellStatus(OrganismStatus.Empty);
                }
            }
            else
            {
                _cellOrganism.Update(enumerable, countNeighbors, this);
            }
        }

        public void SetMigration(Organism organism)
        {
            if (_cellOrganism == null)
            {
                _cellOrganism = organism;
            }
            else
            {
                throw new Exception("Migration to full occupied cell");
            }
        }

        public Cell Clone()
        {
            return new Cell(this);

        }

        public OrganismStatus Status
        {
            get { return _status; }
        }

        public Organism Organism
        {
            get
            {
                if (_cellOrganism != null)
                    return _cellOrganism;
                return null;
            }
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }

        #endregion

        #region Private

        private void OrganismCreate()
        {
            var rand = new Random(_x*_y + _x + _y + Environment.TickCount);
            _cellOrganism = new Organism((byte) rand.Next(255), _x, _y);
        }

        private void OrganismBorn()
        {
            var rand = new Random(_x * _y + _x + _y + Environment.TickCount);
            _cellOrganism = new Organism((byte) rand.Next(255), _x, _y);
        }

        private void OrganismLive()
        {
           
        }

        private void OrganismDead()
        {
            _cellOrganism = null;
        }

        private void CellEmpty()
        {
            _cellOrganism = null;
        }

        #endregion
    }
}