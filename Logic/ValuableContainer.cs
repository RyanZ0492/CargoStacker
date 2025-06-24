using Models.Interfaces;

namespace Models
{
    public class ValuableContainer : Container
    {
        public ValuableContainer(int weight) : base(weight, true) { }
        public override bool CanStackOnTop(IContainer lower)
        {
            if (lower.IsValuable)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}
