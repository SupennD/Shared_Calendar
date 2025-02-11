using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcRepository
{
    public class CalendarDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseSqlite(@"Data Source = ../EFCRepository/calendar.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // one to many with creator and groups
            modelBuilder.Entity<Group>()
                .HasOne(c => c.Creator)
                .WithMany(g => g.GroupsCreated)
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many with user and events
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Creator)
                .WithMany(u => u.EventsCreated)
                .HasForeignKey(e => e.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many with Group and events
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Group)
                .WithMany(g => g.Events)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-Many with groups and sers
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Members)
                .WithMany(u => u.GroupsJoined)
                .UsingEntity(j => j.ToTable("GroupMembers"));
        }
    }
}