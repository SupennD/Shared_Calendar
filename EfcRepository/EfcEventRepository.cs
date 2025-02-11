using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Repositories;

namespace EfcRepository;

public class EfcEventRepository : IEventRepository
{
    private readonly CalendarDbContext ctx;

    public EfcEventRepository(CalendarDbContext ctx)
    {
        this.ctx = ctx;
    }
    public async Task<Event> AddAsync(Event eventi)
    {
        EntityEntry<Event> entityEntry = await ctx.Events.AddAsync(eventi);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(Event eventi)
    {
        if (!(await ctx.Events.AnyAsync(p => p.Id == eventi.Id)))
        {
            throw new InvalidOperationException($"Event with id {eventi.Id} not found");

        }
        ctx.Events.Update(eventi);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await ctx.Events.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new InvalidOperationException($"Event with id {id} not found");

        }
        ctx.Events.Update(existing);
        await ctx.SaveChangesAsync();   
    }

    public async Task<Event> GetSingleAsync(int id)
    {
        var existing = await ctx.Events.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new InvalidOperationException($"Event with id {id} not found");

        }

        return existing;
    }

    public async Task<Event> GetSingleByNameAsync(string name)
    {
        var existing = await ctx.Events.SingleOrDefaultAsync(p => p.Name == name);
        if (existing == null)
        {
            throw new InvalidOperationException($"Event with Name: {name} not found");

        }

        return existing;
    }

    public IQueryable<Event> GetMany()
    {
        return ctx.Events;
    }
}