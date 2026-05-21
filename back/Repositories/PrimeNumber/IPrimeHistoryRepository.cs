namespace back.Repositories
{
    // โมเดลสำหรับการ
    public interface IPrimeHistoryRepository
    {
        void Save(PrimeHistory history);
        IEnumerable<PrimeHistory> GetAll();
    }
}