using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace ToDo.Data.Tests
{
    public class TestWithSqlite : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly ToDoDbContext DbContext;

        public TestWithSqlite()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<ToDoDbContext>()
                   .UseSqlite(_connection)
                   .Options;
            DbContext = new ToDoDbContext(options);
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
