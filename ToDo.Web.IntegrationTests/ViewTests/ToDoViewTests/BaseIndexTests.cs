using System.Net;
using System.Threading.Tasks;
using ToDo.Web.IntegrationTests.Factories;
using ToDo.Web.IntegrationTests.Helpers;
using Xunit;

namespace ToDo.Web.IntegrationTests.ViewTests.ToDoViewTests
{
    public abstract class BaseIndexTests
    {
        protected BaseWebApplicationFactory<TestStartup> Factory { get; }

        protected BaseIndexTests(BaseWebApplicationFactory<TestStartup> factory) =>
            Factory = factory;

        [Fact]
        public async Task DisplaysAllToDoItems()
        {
            var client = Factory.CreateClient();

            var indexView = await client.GetAsync("/ToDo");

            Assert.Equal(HttpStatusCode.OK, indexView.StatusCode);
            var indexViewHtml = await HtmlHelpers.GetDocumentAsync(indexView);
            var todoItems = indexViewHtml.QuerySelectorAll(".todo-item");
            Assert.Equal(2, todoItems.Length);
        }
    }
}