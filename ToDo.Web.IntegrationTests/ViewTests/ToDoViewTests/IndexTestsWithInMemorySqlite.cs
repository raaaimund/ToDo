using ToDo.Web.IntegrationTests.Factories;
using Xunit;

namespace ToDo.Web.IntegrationTests.ViewTests.ToDoViewTests
{
    public class IndexTestsWithInMemorySqlite : BaseIndexTests, IClassFixture<WebApplicationFactoryWithInMemorySqlite>
    {
        public IndexTestsWithInMemorySqlite(WebApplicationFactoryWithInMemorySqlite factory) : base(factory)
        {
        }
    }
}
