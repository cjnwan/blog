namespace Domain.Repository
{
    public interface IUnitOfWork
    {
        bool IsCommitted { get; }

        int Commit();

        void Rollback();
    }
}