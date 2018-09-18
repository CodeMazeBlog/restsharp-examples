using RestSharp;
using RestSharp.Authenticators;
using RestSharpExamples.Constants;
using RestSharpExamples.Model;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RestSharpExamples
{
    public class RequestHandler : IRequestHandler
    {
        private static IRestClient _restClient;

        public RequestHandler()
        {
            //keeping sensitive information in environment variables
            //never, ever leave secret information in the code or it might end up in some public repo
            var githubUsername = Environment.GetEnvironmentVariable("GITHUB_USERNAME");
            var githubPassword = Environment.GetEnvironmentVariable("GITHUB_PASS");
            var githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

            _restClient = new RestClient
            {
                BaseUrl = new Uri(RequestConstants.BaseUrl),
                Authenticator = new HttpBasicAuthenticator(githubUsername, githubPassword)
            };
        }

        public RequestHandler(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public IRestResponse<List<Repository>> GetRepositories()
        {
            var request = new RestRequest { Resource = "user/repos" };
            request.AddHeader(RequestConstants.UserAgent, RequestConstants.UserAgentValue);

            var response = _restClient.Execute<List<Repository>>(request);

            return response;
        }

        public IRestResponse<Repository> CreateRepository(string user, string repository)
        {
            var repo = new Repository
            {
                Name = repository,
                FullName = $"{user}/{repository}",
                Description = "Generic description",
                Private = false
            };

            var request = new RestRequest { Resource = "user/repos" };
            request.AddHeader(RequestConstants.UserAgent, RequestConstants.UserAgentValue);
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(repo), ParameterType.RequestBody);
            request.Method = Method.POST;

            var response = _restClient.Execute<Repository>(request);

            return response;
        }

        public IRestResponse<Repository> EditRepository(string user, string repository)
        {
            var repo = new Repository
            {
                Name = repository,
                FullName = $"{user}/{repository}",
                Description = "Modified repository",
                Private = false
            };

            var request = new RestRequest { Resource = $"repos/{user}/{repository}" };
            request.AddHeader(RequestConstants.UserAgent, RequestConstants.UserAgentValue);
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(repo), ParameterType.RequestBody);
            request.Method = Method.PATCH;

            var response = _restClient.Execute<Repository>(request);

            return response;
        }

        public IRestResponse<Repository> DeleteRepository(string user, string repository)
        {
            var request = new RestRequest { Resource = $"repos/{user}/{repository}" };
            request.AddHeader(RequestConstants.UserAgent, RequestConstants.UserAgentValue);
            request.Method = Method.DELETE;

            var response = _restClient.Execute<Repository>(request);

            return response;
        }
    }
}
