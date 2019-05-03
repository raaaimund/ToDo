using ToDo.Web.IntegrationTests.Factories;
using Xunit;

namespace ToDo.Web.IntegrationTests
{
    public class EndpointTestsWithInMemorySqlite : BaseEndpointTests, IClassFixture<WebApplicationFactoryWithInMemorySqlite>
    {
        public EndpointTestsWithInMemorySqlite(WebApplicationFactoryWithInMemorySqlite factory) : base(factory)
        {
        }
    }
}