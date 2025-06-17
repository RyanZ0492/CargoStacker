using Models.Interfaces;


namespace CargoStacker.Services
{
        public class StackingService : IStackingService
        {
        public bool ValidateStacking(IContainer lower, IContainer upper)
        {
            // Prevent stacking if the lower container is valuable
            if (lower.IsValuable) return false;

            return upper.CanStackOnTop(lower);
        }
    }
}