using System.Threading.Tasks;
using Xunit;

namespace ToDo.Data.Tests
{
    public class ToDoDbContextTests : TestWithSqlite
    {
        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }
    }
}
