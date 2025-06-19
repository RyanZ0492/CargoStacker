using Models.Interfaces;

namespace Models
{
    public abstract class Container : IContainer
    {
        public int Weight { get; private set; }
        public bool IsValuable { get; private set; }

        protected Container(int weight, bool isValuable)
        {
            if (weight < 4000 || weight > 30000)
                throw new ArgumentException($"Container weight must be between 4000kg and 30000kg. Given: {weight}kg.");

            Weight = weight;
            IsValuable = isValuable;
        }

        public abstract bool CanStackOnTop(IContainer lower);
    }
}