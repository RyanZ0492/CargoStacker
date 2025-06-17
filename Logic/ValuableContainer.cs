using Models.Interfaces;

namespace Models
{
    public class ValuableContainer : Container
    {
        public ValuableContainer(int weight) : base(weight, true) { }

public override bool CanStackOnTop(IContainer lower)
{
    return false; // Nothing can be stacked on valuable cargo
}

    }

}
