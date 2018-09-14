using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using RestSharpExamples.Constants;
using RestSharpExamples.Model;

namespace RestSharpExamples
{
    public class RequestHandler : IRequestHandler
    {
        private static string _githubUsername;
        private static string _githubPassword;
        private static string _githubToken;

        public RequestHandler()
        {
            //keeping sensitive information in environment variables
            //never, ever leave secret information in the code or it might end up in some public repo
            _githubUsername = Environment.GetEnvironmentVariable("GITHUB_USERNAME");
            _githubPassword = Environment.GetEnvironmentVariable("GITHUB_PASS");
            _githubToken = Environment.GetEnvironmentVariable("GITHUB_TOKEN");
        }

        public IRestResponse GetRepositories()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(RequestConstants.BaseUrl),
                Authenticator =   new HttpBasicAuthenticator(_githubUsername, _githubPassword)
            };

            var request = new RestRequest { Resource = "user/repos" };
            request.AddHeader(RequestConstants.UserAgent, RequestConstants.UserAgentValue);

            IRestResponse response = client.Execute<List<Repository>>(request);

            return response;
        }

        public Task<Repository> CreateRepository(string user, string repository)
        {
            throw new NotImplementedException();
        }

        public Task<Repository> EditRepository(string user, string repository)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> DeleteRepository(string user, string repository)
        {
            throw new NotImplementedException();
        }
    }
}
