using Models.Interfaces;

public interface IStackingService
{
    bool ValidateStacking(IContainer lower, IContainer upper);
}
