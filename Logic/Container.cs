using Models.Interfaces;


namespace Models
{
    public abstract class Container : IContainer
    {
        public int Weight { get; private set; }
        public bool IsValuable { get; private set; }

        protected Container(int weight, bool isValuable)
        {
            Weight = weight;
            IsValuable = isValuable;
        }

        public abstract bool CanStackOnTop(IContainer lower);
    }

}
