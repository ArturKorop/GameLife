using System.Collections.ObjectModel;

namespace Domain.GameLife
{
    public class Cell
    {
        private int _x;
        private int _y;
        private OrganismStatus _status = OrganismStatus.Empty;
        private readonly Collection<object> _cellObjects = new Collection<object>();

        public Cell(int x, int y)
        {
            _x = x;
            _y = y;
        }

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

        private void OrganismBorn()
        {
            _cellObjects.Add(new Organism());
        }
        private void OrganismLive()
        {

        }
        private void OrganismDead()
        {
            _cellObjects.Clear();
        }
        private void CellEmpty()
        {
            _cellObjects.Clear();
        }

        public int X
        {
            get { return _x; }
        }
        public int Y
        {
            get { return _y; }
        }
    }
}