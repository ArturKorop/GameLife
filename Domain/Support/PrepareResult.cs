using Domain.GameLife;

namespace Domain.Support
{
    /// <summary>
    /// Class, that provides a result of prepare to next step of life
    /// </summary>
    public class PrepareResult
    {
        /// <summary>
        /// <see cref="Migration"/>
        /// </summary>
        private readonly Migration _migration = new Migration();
        /// <summary>
        /// Create new <see cref="Migration"/>
        /// </summary>
        /// <param name="from">Coordinate of old <see cref="Cell"/></param>
        /// <param name="to">Coordinate of new <see cref="Cell"/></param>
        /// <param name="organism">Organism, that was migrate</param>
        public void SetMigration(Coordinate from, Coordinate to, Organism organism)
        {
            _migration.IsMigrartion = true;
            _migration.From = from;
            _migration.To = to;
            _migration.Organism = organism;
        }
        /// <summary>
        /// Properties of <see cref="Migration"/>
        /// </summary>
        public Migration Migration
        {
            get { return _migration; }
        }

    }
    /// <summary>
    /// Class, that provides a migration
    /// </summary>
    public class Migration
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Migration()
        {
            IsMigrartion = false;
        }
        /// <summary>
        /// Is migration was?
        /// </summary>
        public bool IsMigrartion { get; set; }
        /// <summary>
        /// <see cref="Coordinate"/> of old cell
        /// </summary>
        public Coordinate From { get; set; }
        /// <summary>
        /// <see cref="Coordinate"/> of new cell
        /// </summary>
        public Coordinate To { get; set; }
        /// <summary>
        /// <see cref="Organism"/> that was migrate
        /// </summary>
        public Organism Organism { get; set; }
    }
    /// <summary>
    /// Class, that procvides a description of coordinate on field
    /// </summary>
    public struct Coordinate
    {
        /// <summary>
        /// Coordinate x
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Coordinate y
        /// </summary>
        public int Y { get; set; }
    }
}