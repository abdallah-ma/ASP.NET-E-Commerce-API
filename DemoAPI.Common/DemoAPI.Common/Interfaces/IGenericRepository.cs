

namespace DemoAPI.Common.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        public Task<T?> GetAsync(int id);

        public  Task<T?> GetAsyncWithSpec(ISpecification<T> spec);


        public Task<IReadOnlyList<T>> GetAllAsync();

        public Task<IReadOnlyList<T>> GetAllAsyncWithSpec(ISpecification<T> spec);

        public Task<int> UpdateAsync(T entity);

        public Task<int> AddAsync(T Entity);

        public Task<int> DeleteAsync(T Entity);

        public Task<int> CountAsync(ISpecification<T> spec);

        Task Add(T entity);


        void Update(T entity);
        

        void Delete(T entity);

    }
}
