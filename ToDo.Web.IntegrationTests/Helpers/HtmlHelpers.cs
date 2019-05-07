using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Io;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ToDo.Web.IntegrationTests.Helpers
{
    /// <summary>
    /// Original: https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/test/integration-tests/samples/2.x/IntegrationTestsSample/tests/RazorPagesProject.Tests/Helpers/HtmlHelpers.cs
    /// </summary>
    public static class HtmlHelpers
    {
        public static async Task<IHtmlDocument> GetDocumentAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            var document = await BrowsingContext.New()
                .OpenAsync(ResponseFactory, CancellationToken.None);
            return (IHtmlDocument)document;

            void ResponseFactory(VirtualResponse htmlResponse)
            {
                htmlResponse
                    .Address(response.RequestMessage.RequestUri)
                    .Status(response.StatusCode);

                MapHeaders(response.Headers);
                MapHeaders(response.Content.Headers);

                htmlResponse.Content(content);

                void MapHeaders(HttpHeaders headers)
                {
                    foreach (var header in headers)
                    {
                        foreach (var value in header.Value)
                        {
                            htmlResponse.Header(header.Key, value);
                        }
                    }
                }
            }
        }
    }
}
