using System.Collections.Generic;
using System.Threading.Tasks;

namespace BettingApp.DAL
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task AddRange(List<T> entity);
        void Delete(T entity);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        void Update(T entity);
    }
}