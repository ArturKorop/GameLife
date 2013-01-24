using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Domain.Support;

namespace Domain.GameLife
{
    /// <summary>
    /// Class that provides description of cell in field
    /// </summary>
    public class Cell
    {
        #region Variable
        /// <summary>
        /// Coordinate X of cell
        /// </summary>
        private readonly int _x;
        /// <summary>
        /// Coordinate Y of cell
        /// </summary>
        private readonly int _y;
        /// <summary>
        /// Status of cell
        /// </summary>
        private OrganismStatus _status = OrganismStatus.Empty;
        /// <summary>
        /// Organism of cell
        /// </summary>
        private Organism _cellOrganism = null;

        #endregion

        #region Initialize
        /// <summary>
        /// Constructor of <see cref="Cell"/>
        /// </summary>
        /// <param name="x">Coordinate X</param>
        /// <param name="y">Coordinate Y</param>
        public Cell(int x, int y)
        {
            _x = x;
            _y = y;
        }
        /// <summary>
        /// Constructor for clonning
        /// </summary>
        /// <param name="cell"><see cref="Cell"/> for clonning</param>
        public Cell(Cell cell)
        {
            _x = cell.X;
            _y = cell.Y;
            _status = cell.Status;
            _cellOrganism = cell.Organism;
        }

        #endregion

        #region Public
        /// <summary>
        /// Setter of cell status
        /// </summary>
        /// <param name="status"><see cref="OrganismStatus"/></param>
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
        /// <summary>
        /// Prepare for next step of life
        /// </summary>
        /// <param name="neighborCells">Collection of neighbor cells</param>
        /// <returns><see cref="PrepareResult"/></returns>
        public PrepareResult Prepare(Collection<Cell> neighborCells)
        {
            if (_cellOrganism != null)
            {
                return _cellOrganism.Prepare(neighborCells);
            }
            return new PrepareResult();
        }
        /// <summary>
        /// Calculate next step of life
        /// </summary>
        /// <param name="neighborCells">Collection of neighbor cells</param>
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
        /// <summary>
        /// Set migration of organism
        /// </summary>
        /// <param name="organism"><see cref="Organism"/></param>
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
        /// <summary>
        /// Clonning this <see cref="Cell"/>
        /// </summary>
        /// <returns>new <see cref="Cell"/></returns>
        public Cell Clone()
        {
            return new Cell(this);
        }
        /// <summary>
        /// Property status of this cell
        /// </summary>
        public OrganismStatus Status
        {
            get { return _status; }
        }
        /// <summary>
        /// Property organism, if exist, of this cell
        /// </summary>
        public Organism Organism
        {
            get
            {
                if (_cellOrganism != null)
                    return _cellOrganism;
                return null;
            }
        }
        /// <summary>
        /// Property coordinate X
        /// </summary>
        public int X
        {
            get { return _x; }
        }
        /// <summary>
        /// Property coordinate Y
        /// </summary>
        public int Y
        {
            get { return _y; }
        }

        #endregion

        #region Private
        /// <summary>
        /// Create new organism
        /// </summary>
        private void OrganismCreate()
        {
            var rand = new Random(_x*_y + _x + _y + Environment.TickCount);
            _cellOrganism = new Organism((byte) rand.Next(255), _x, _y);
        }
        /// <summary>
        /// Organism born
        /// </summary>
        private void OrganismBorn()
        {
            var rand = new Random(_x * _y + _x + _y + Environment.TickCount);
            _cellOrganism = new Organism((byte) rand.Next(255), _x, _y);
        }
        /// <summary>
        /// Organism live
        /// </summary>
        private void OrganismLive()
        {
           
        }
        /// <summary>
        /// Organism dead
        /// </summary>
        private void OrganismDead()
        {
            _cellOrganism = null;
        }
        /// <summary>
        /// Cell is empty for organism
        /// </summary>
        private void CellEmpty()
        {
            _cellOrganism = null;
        }

        #endregion
    }
}