using System.Collections.ObjectModel;

namespace Domain.GameLife
{
    public class Cell
    {
        #region Variable

        private int _x;
        private int _y;
        private OrganismStatus _status = OrganismStatus.Empty;
        private readonly Collection<object> _cellObjects = new Collection<object>();

        #endregion

        #region Initialize

        public Cell(int x, int y)
        {
            _x = x;
            _y = y;
        }

        #endregion

        #region Public

        public void SetOrganismStatus(OrganismStatus status)
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
            }
        }

        public OrganismStatus Status
        {
            get { return _status; }
        }

        public Organism Organism
        {
            get
            {
                if (_cellObjects.Count != 0)
                    return (Organism) _cellObjects[0];
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

        private void OrganismBorn()
        {
            _cellObjects.Add(new Organism());
        }

        private void OrganismLive()
        {
            if(_cellObjects.Count != 0)
            ((Organism)_cellObjects[0]).Update();
        }

        private void OrganismDead()
        {
            _cellObjects.Clear();
        }

        private void CellEmpty()
        {
            _cellObjects.Clear();
        }

        #endregion
    }
}