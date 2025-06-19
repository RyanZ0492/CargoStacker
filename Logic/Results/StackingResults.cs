namespace Models.Results
{
    public class StackingResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public int TotalWeight { get; private set; }

        private StackingResult(bool success, string message, int totalWeight)
        {
            Success = success;
            Message = message;
            TotalWeight = totalWeight;
        }

        public static StackingResult SuccessResult(int totalWeight)
        {
            return new StackingResult(true, "Stacking successful.", totalWeight);
        }

        public static StackingResult FailureResult(string errorMessage, int totalWeight)
        {
            return new StackingResult(false, errorMessage, totalWeight);
        }
    }
}
