using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Web.IntegrationTests.Factories;
using ToDo.Web.IntegrationTests.Helpers;
using Xunit;

namespace ToDo.Web.IntegrationTests
{
    public abstract class BaseEndpointTests
    {
        protected BaseWebApplicationFactory<TestStartup> Factory { get; }

        protected BaseEndpointTests(BaseWebApplicationFactory<TestStartup> factory) =>
            Factory = factory;

        public static readonly IEnumerable<object[]> Endpoints = new List<object[]>()
        {
            new object[] {"/"},
            new object[] {"/Home"},
            new object[] {"/Home/Error"},
            new object[] {"/ToDo"},
            new object[] {"/ToDo/Create"},
            new object[] {$"/ToDo/Update/{TestDataSeeder.FirstItemId}"},
            new object[] {$"/ToDo/Delete/{TestDataSeeder.FirstItemId}"},
        };

        [Theory]
        [MemberData(nameof(Endpoints))]
        public async Task GetEndpointsReturnSuccessAndCorrectContentType(string url)
        {
            const string expectedContentType = "text/html; charset=utf-8";
            var client = Factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal(expectedContentType,
                response.Content.Headers.ContentType.ToString());
        }
    }
}