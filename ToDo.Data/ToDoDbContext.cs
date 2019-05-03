using Microsoft.EntityFrameworkCore;
using ToDo.Dto;

namespace ToDo.Data
{
    public class ToDoDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItem { get; set; }
        public DbSet<User> User { get; set; }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);
        }
    }
}
