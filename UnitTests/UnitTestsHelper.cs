using Moq;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace UnitTests
{
    public static class UnitTestsHelper
    {
        public static string BuildTestListRepositories()
        {
            return
                "[{\"name\":\"Test-Repository\",\"full_name\":\"CodeMazeBlog/Test-Repository\",\"description\":\"Created repository\",\"html_url\":null,\"private\":false}]";
        }

        public static string BuildCreatedRepository()
        {
            return
                "{\"name\":\"Test-Repository\",\"full_name\":\"CodeMazeBlog/Test-Repository\",\"description\":\"Created repository\",\"html_url\":null,\"private\":false}";
        }

        public static string BuildModifiedRepository()
        {
            return
                "{\"name\":\"Test-Repository\",\"full_name\":\"CodeMazeBlog/Test-Repository\",\"description\":\"Modified repository\",\"html_url\":null,\"private\":false}";
        }

        public static IRestClient MockRestClient<T>(string json, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
            where T : new()
        {
            var data = JsonConvert.DeserializeObject<T>(json);
            var response = new Mock<IRestResponse<T>>();
            response.Setup(_ => _.StatusCode).Returns(httpStatusCode);
            response.Setup(_ => _.Data).Returns(data);

            var mockIRestClient = new Mock<IRestClient>();
            mockIRestClient
                .Setup(x => x.Execute<T>(It.IsAny<IRestRequest>()))
                .Returns(response.Object);

            return mockIRestClient.Object;
        }
    }
}
