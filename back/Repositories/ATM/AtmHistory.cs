namespace back.Repositories
{
    // โมเดลสำหรับเก็บประวัติ ATM โดยเฉพาะ 
    public class AtmHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required int InputAmount { get; set; }
        public required int ThousandCount { get; set; }
        public required int FiveHundredCount { get; set; }
        public required int OneHundredCount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}