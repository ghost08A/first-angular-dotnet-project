using System.Collections.Concurrent;

namespace back.Repositories
{
    public class AtmHistoryRepository : IAtmHistoryRepository
    {
        private static readonly ConcurrentBag<AtmHistory> _databaseMock = new();

        public void Save(AtmHistory history)
        {
            _databaseMock.Add(history);
            Console.WriteLine($"[Database Mock] Saved ATM History -> Amount: {history.InputAmount}");
        }

        public IEnumerable<AtmHistory> GetAll()
        {
            return _databaseMock.OrderByDescending(x => x.CreatedAt);
        }
    }
}