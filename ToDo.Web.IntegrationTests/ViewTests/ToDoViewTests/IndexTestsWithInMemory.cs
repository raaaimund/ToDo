using ToDo.Web.IntegrationTests.Factories;
using Xunit;

namespace ToDo.Web.IntegrationTests.ViewTests.ToDoViewTests
{
    public class IndexTestsWithInMemory : BaseIndexTests, IClassFixture<WebApplicationFactoryWithInMemory>
    {
        public IndexTestsWithInMemory(WebApplicationFactoryWithInMemory factory) : base(factory)
        {
        }
    }
}
