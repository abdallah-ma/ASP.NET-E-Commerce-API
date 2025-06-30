using DemoAPI.Common.Interfaces;
using System.Collections;


namespace DemoAPI.Common
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {

        private Hashtable _Repos = new Hashtable();
        private BaseDbContext _DbContext;

        public UnitOfWork(BaseDbContext dbContext) { 
           _DbContext = dbContext;
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var Repo = new GenericRepository<T>(_DbContext);

            if (! _Repos.ContainsKey(typeof(T).Name))
            {
                _Repos.Add(typeof(T).Name, Repo);
            }
            
            return Repo;
        }

        public async ValueTask DisposeAsync()
        {
            if (_DbContext != null)
            {
                await _DbContext.DisposeAsync();
            }
        }

        public async Task<int> CompleteAsync()
        {
            return await _DbContext.SaveChangesAsync();
        }

    }
}
