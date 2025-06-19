using Models.Interfaces;

namespace Models
{
    public class RegularContainer : Container
    {
        public RegularContainer(int weight) : base(weight, false) { }

        public override bool CanStackOnTop(IContainer lower)
        {
            if (lower.IsValuable)
                return false;

            return (lower.Weight + this.Weight) <= 120000;
        }
    }
}
