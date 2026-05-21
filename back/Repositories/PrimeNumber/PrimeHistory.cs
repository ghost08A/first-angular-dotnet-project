namespace back.Repositories
{    public class PrimeHistory
    {
        //โมเดลสำหรับเก็บประวัติ Prime Number โดยเฉพาะ
        public Guid Id { get; set; } = Guid.NewGuid();
        public required int InputValue { get; set; }
        public required int PreviousPrime { get; set; }
        public required int NextPrime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}