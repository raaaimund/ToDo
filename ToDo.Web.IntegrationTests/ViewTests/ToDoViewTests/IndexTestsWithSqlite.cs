using ToDo.Web.IntegrationTests.Factories;
using Xunit;

namespace ToDo.Web.IntegrationTests.ViewTests.ToDoViewTests
{
    public class IndexTestsWithSqlite : BaseIndexTests, IClassFixture<WebApplicationFactoryWithSqlite>
    {
        public IndexTestsWithSqlite(WebApplicationFactoryWithSqlite factory) : base(factory)
        {
        }
    }
}
