using Models.Interfaces;
using Models.Results;

public interface IStackingService
{
    StackingResult ValidateStacking(IContainer lower, IContainer upper);
}
