

namespace DemoAPI.Common.Interfaces
{
    public interface IUnitOfWork
    {
        public IGenericRepository<T> Repository<T>() where T : BaseEntity;

        

        public Task<int> CompleteAsync();

    }
}
