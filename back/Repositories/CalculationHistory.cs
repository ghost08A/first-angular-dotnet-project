namespace back.Repositories
{
    public class CalculationHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string FeatureName { get; set; }
        public required string InputValue { get; set; }
        public required string ResultValue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}