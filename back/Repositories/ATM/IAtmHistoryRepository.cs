namespace back.Repositories
{
    public interface IAtmHistoryRepository
    {
        void Save(AtmHistory history);
        IEnumerable<AtmHistory> GetAll();
    }
}