using RestSharp;
using RestSharpExamples.Model;
using System.Collections.Generic;

namespace RestSharpExamples
{
    public interface IRequestHandler
    {
        IRestResponse<List<Repository>> GetRepositories();
        IRestResponse<Repository> CreateRepository(string user, string repository);
        IRestResponse<Repository> EditRepository(string user, string repository);
        IRestResponse<Repository> DeleteRepository(string user, string repository);
    }
}
