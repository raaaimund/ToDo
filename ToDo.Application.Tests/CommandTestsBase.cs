using System;
using ToDo.Infrastructure.Persistence;

namespace ToDo.Application.Tests
{
    public abstract class CommandTestsBase : IDisposable
    {
        public CommandTestsBase()
        {
            Context = ToDoDbContextFactory.Create();
        }

        public ToDoDbContext Context { get; }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                }

                disposedValue = true;
            }
        }

        ~CommandTestsBase()
        {
          Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
