using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Entities;

namespace ToDo.Application.Common.Interfaces
{
    public interface IToDoDbContext : IDisposable, IAsyncDisposable
    {
        DbSet<Domain.Entities.ToDoItem> ToDoItem { get; set; }
        DbSet<User> User { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
