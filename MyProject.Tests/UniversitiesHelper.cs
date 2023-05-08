
using System.Net.Http;
using System.Threading.Tasks;

namespace MyProject.Tests
{
    public class UniversitiesHelper
    {
        private const string ApiBaseUrl = "http://universities.hipolabs.com";
        private const string ApiBaseUrlSearch = "http://universities.hipolabs.com/search?";

        private HttpClient client;

        public UniversitiesHelper()
        {
            client = new HttpClient();
        }

        public async Task CheckBaseUrl()
        {
            await CheckEndpoint(ApiBaseUrl);
        }

        public async Task CheckValidEndpoint(string endpoint)
        {
            await CheckEndpoint(endpoint, false);
        }

        public async Task CheckInvalidEndpoint(string endpoint)
        {
            await CheckEndpoint(endpoint, true);
        }

        private async Task CheckEndpoint(string endpoint, bool isEmpty = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var response = await client.SendAsync(request);
            Console.WriteLine($"Request: {request}");
            Console.WriteLine($"Response: {response}");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();

            if (isEmpty)
            {
                Assert.That(responseContent, Is.EqualTo("[]"), "The body was not empty");
            }
            else
            {
                Assert.That(responseContent, Is.Not.Null.Or.Empty, "The body was unexpectedly empty");
                Assert.That(responseContent, Is.Not.EqualTo("[]"), "The body was unexpectedly empty");
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}