using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;

namespace EfcRepository;

public class EfcGroupRepository : IGroupRepository
{
    private readonly CalendarDbContext ctx;

    public EfcGroupRepository(CalendarDbContext ctx)
    {
        this.ctx = ctx;
    }
    public async Task<Group> AddAsync(Group group)
    {
        EntityEntry<Group> entityEntry = await ctx.Groups.AddAsync(group);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Group group)
    {
        if (!(await ctx.Groups.AnyAsync(p => p.Id == group.Id)))
        {
            throw new InvalidOperationException($"Group with id {group.Id} not found");

        }
        ctx.Groups.Update(group);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await ctx.Groups.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new InvalidOperationException($"Group with id {id} not found");

        }
        ctx.Groups.Update(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<Group> GetSingleAsync(int id)
    {
        var existing = await ctx.Groups.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new InvalidOperationException($"Group with id {id} not found");

        }

        return existing;
    }

    public async Task<Group> GetSingleByNameAsync(string name)
    {
        var existing = await ctx.Groups.SingleOrDefaultAsync(p => p.Name == name);
        if (existing == null)
        {
            throw new InvalidOperationException($"Group with Name: {name} not found");

        }

        return existing;
    }

    public IQueryable<Group> GetMany()
    {
        return ctx.Groups;
    }
}