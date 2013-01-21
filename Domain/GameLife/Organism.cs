namespace Domain.GameLife
{
    public class Organism
    {
        private int _age;

        public Organism()
        {
            _age = 0;
        }

        public int Age
        {
            get { return _age; }
        }

        public void Update()
        {
            _age++;
        }
    }
}