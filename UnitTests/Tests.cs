using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using RestSharpExamples;
using RestSharpExamples.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace UnitTests
{
    [TestClass]
    public class Tests
    {
        public static string BuildTestListRepositories()
        {
            return
                "[{\"name\":\"Test - Repository\",\"full_name\":\"CodeMazeBlog / Test - Repository\",\"description\":\"Created repository\",\"html_url\":null,\"private\":false}]";
        }

        public static string BuildCreatedRepository()
        {
            return
                "{\"name\":\"Test - Repository\",\"full_name\":\"CodeMazeBlog / Test - Repository\",\"description\":\"Created repository\",\"html_url\":null,\"private\":false}";
        }

        public static string BuildModifiedRepository()
        {
            return
                "{\"name\":\"Test - Repository\",\"full_name\":\"CodeMazeBlog / Test - Repository\",\"description\":\"Modified repository\",\"html_url\":null,\"private\":false}";
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

        [TestMethod]
        public void GetRepositories_ShouldReturnNonEmptyCollection()
        {
            //Arrange
            var client = MockRestClient<List<Repository>>(BuildTestListRepositories());
            var requestHandler = new RequestHandler(client);
            //Act
            var repos = requestHandler.GetRepositories();
            //Assert
            Assert.AreEqual(true, repos.Data.Any());
        }

        [TestMethod]
        public void CreateRepository_ShouldReturnCreatedStatusCodeAndCreatedData()
        {
            //Arrange
            var client = MockRestClient<Repository>(BuildCreatedRepository(), HttpStatusCode.Created);
            var requestHandler = new RequestHandler(client);
            //Act
            var repos = requestHandler.CreateRepository("Test", "Test");
            //Assert
            Assert.AreEqual(HttpStatusCode.Created, repos.StatusCode);
            Assert.AreEqual("Test - Repository", repos.Data.Name);
        }

        [TestMethod]
        public void EditRepository_ShouldReturnOkAndModifiedData()
        {
            //Arrange
            var client = MockRestClient<Repository>(BuildModifiedRepository());
            var requestHandler = new RequestHandler(client);
            //Act
            var repos = requestHandler.EditRepository("Test", "Test");
            //Assert
            Assert.AreEqual(HttpStatusCode.OK, repos.StatusCode);
            Assert.AreEqual("Modified repository", repos.Data.Description);
        }

        [TestMethod]
        public void DeleteRepository_ShouldReturnNoContentAndNoData()
        {
            //Arrange
            var client = MockRestClient<Repository>("", HttpStatusCode.NoContent);
            var requestHandler = new RequestHandler(client);
            //Act
            var repos = requestHandler.DeleteRepository("Test", "Test");
            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, repos.StatusCode);
            Assert.AreEqual(null, repos.Data);
        }
    }
}
