using ToDo.Web.IntegrationTests.Factories;
using Xunit;

namespace ToDo.Web.IntegrationTests
{
    public class EndpointTestsWithInMemory : BaseEndpointTests, IClassFixture<WebApplicationFactoryWithInMemory>
    {
        public EndpointTestsWithInMemory(WebApplicationFactoryWithInMemory factory) : base(factory)
        {
        }
    }
}