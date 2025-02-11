using Entities;

namespace Repositories;

public interface IGroupRepository
{
    Task<Group> AddAsync(Group group);
    Task UpdateAsync(Group group);
    Task DeleteAsync(int id);
    Task<Group> GetSingleAsync(int id);
    Task<Group> GetSingleByNameAsync(string name);
    IQueryable<Group> GetMany();
}