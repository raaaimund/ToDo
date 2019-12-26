using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;

namespace ToDo.Infrastructure.Persistence
{
    public class ToDoDbContext : DbContext, IToDoDbContext
    {
        public DbSet<ToDoItem> ToDoItem { get; set; }
        public DbSet<User> User { get; set; }

        public ToDoDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
