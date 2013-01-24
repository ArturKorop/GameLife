using Domain.GameLife;

namespace Domain.Support
{
    public class PrepareResult
    {
        private readonly Migration _migration = new Migration();
        
        public void SetMigration(Coordinate from, Coordinate to, Organism organism)
        {
            _migration.IsMigrartion = true;
            _migration.From = from;
            _migration.To = to;
            _migration.Organism = organism;
        }

        public Migration Migration
        {
            get { return _migration; }
        }

    }

    public class Migration
    {
        public Migration()
        {
            IsMigrartion = false;
        }

        public bool IsMigrartion { get; set; }
        public Coordinate From { get; set; }
        public Coordinate To { get; set; }
        public Organism Organism { get; set; }
    }
    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}