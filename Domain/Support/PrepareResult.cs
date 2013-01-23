namespace Domain.Support
{
    public class PrepareResult
    {
        private readonly Migration _migration = new Migration();
        
        public void SetMigration(Coordinate from, Coordinate to)
        {
            _migration.IsMigrartion = true;
            _migration.From = from;
            _migration.To = to;
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
    }
    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}