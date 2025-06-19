using Models.Interfaces;
using Services.Results;

public interface IStackingService
{
    StackingResult ValidateStacking(IContainer lower, IContainer upper);
}
