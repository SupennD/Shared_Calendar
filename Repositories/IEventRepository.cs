using Entities;

namespace Repositories
{
    public interface IEventRepository
    {
        Task<Event> AddAsync(Event eventi); 
        Task UpdateAsync(Event eventi);
        Task DeleteAsync(int id);
        Task<Event> GetSingleAsync(int id);
        Task<Event> GetSingleByNameAsync(string name);
        IQueryable<Event> GetMany();
    }
}