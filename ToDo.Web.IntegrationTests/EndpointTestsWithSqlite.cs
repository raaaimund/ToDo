using ToDo.Web.IntegrationTests.Factories;
using Xunit;

namespace ToDo.Web.IntegrationTests
{
    public class EndpointTestsWithSqlite : BaseEndpointTests, IClassFixture<WebApplicationFactoryWithSqlite>
    {
        public EndpointTestsWithSqlite(WebApplicationFactoryWithSqlite factory) : base(factory)
        {
        }
    }
}