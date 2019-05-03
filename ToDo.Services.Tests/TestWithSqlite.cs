using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using ToDo.Data;

namespace ToDo.Services.Tests
{
    public class TestWithSqlite : IDisposable
    {
        private const string ConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly ToDoDbContext Context;

        public TestWithSqlite()
        {
            _connection = new SqliteConnection(ConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<ToDoDbContext>()
                .UseSqlite(_connection)
                .Options;
            Context = new ToDoDbContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}