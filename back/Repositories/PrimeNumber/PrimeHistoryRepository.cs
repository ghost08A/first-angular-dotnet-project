using System.Collections.Concurrent;

namespace back.Repositories
{
    public class PrimeHistoryRepository : IPrimeHistoryRepository
    {
        private static readonly ConcurrentBag<PrimeHistory> _databaseMock = new();

        public void Save(PrimeHistory history)
        {
            _databaseMock.Add(history);
            Console.WriteLine($"[Database Mock] Saved Prime History -> Input: {history.InputValue}");
        }

        public IEnumerable<PrimeHistory> GetAll()
        {
            return _databaseMock.OrderByDescending(x => x.CreatedAt);
        }
    }
}