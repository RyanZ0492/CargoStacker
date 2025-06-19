using Models.Interfaces;

namespace Models
{
    public abstract class Container : IContainer
    {
        // Properties are read-only after construction.
        public int Weight { get; private set; }
        public bool IsValuable { get; private set; }

        // Constructor checks that the container weight is between 4000kg and 30000kg.
        protected Container(int weight, bool isValuable)
        {
            if (weight < 4000 || weight > 30000)
                throw new ArgumentException($"Container weight must be between 4000kg and 30000kg. Given: {weight}kg.");

            Weight = weight;
            IsValuable = isValuable;
        }

        // Derived classes must implement how they define stacking behavior.
        public abstract bool CanStackOnTop(IContainer lower);
    }
}