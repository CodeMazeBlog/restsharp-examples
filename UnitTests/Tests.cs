using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        [TestMethod]
        public void GetRepositories_ShouldReturnNonEmptyCollection()
        {
            //Arrange
            var client = UnitTestsHelper.MockRestClient<List<Repository>>(UnitTestsHelper.BuildTestListRepositories());
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
            var client = UnitTestsHelper.MockRestClient<Repository>(UnitTestsHelper.BuildCreatedRepository(), HttpStatusCode.Created);
            var requestHandler = new RequestHandler(client);
            //Act
            var repos = requestHandler.CreateRepository("Test", "Test");
            //Assert
            Assert.AreEqual(HttpStatusCode.Created, repos.StatusCode);
            Assert.AreEqual("Test-Repository", repos.Data.Name);
        }

        [TestMethod]
        public void EditRepository_ShouldReturnOkAndModifiedData()
        {
            //Arrange
            var client = UnitTestsHelper.MockRestClient<Repository>(UnitTestsHelper.BuildModifiedRepository());
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
            var client = UnitTestsHelper.MockRestClient<Repository>("", HttpStatusCode.NoContent);
            var requestHandler = new RequestHandler(client);
            //Act
            var repos = requestHandler.DeleteRepository("Test", "Test");
            //Assert
            Assert.AreEqual(HttpStatusCode.NoContent, repos.StatusCode);
            Assert.AreEqual(null, repos.Data);
        }
    }
}
