using RestSharpExamples.Model;
using System;
using System.Collections.Generic;
using RestSharp;

namespace RestSharpExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RestSharp examples.");
            Console.WriteLine();

            var requestHandler = new RequestHandler();

            var repositories = requestHandler.GetRepositories().Data;

            Console.WriteLine($"Number of repositories {repositories.Count}");
            Console.WriteLine();
            foreach (var repository in repositories)
            {
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                WriteInColor("Repository", ConsoleColor.Green);
                WriteResult(repository);
            }

            Console.WriteLine();

            try
            {
                var newRepository = requestHandler.CreateRepository("CodeMazeBlog", "Test-Repository").Data;
                WriteInColor("Repository created", ConsoleColor.Green);
                WriteResult(newRepository);
                Console.WriteLine();
            }
            catch (AggregateException e)
            {
                Console.WriteLine(e.Message);
            }

            var editedRepository = requestHandler.EditRepository("CodeMazeBlog", "Test-Repository").Data;
            WriteInColor("Repository edited", ConsoleColor.Green);
            WriteResult(editedRepository);
            Console.WriteLine();

            var deleteRepoResult = requestHandler.DeleteRepository("CodeMazeBlog", "Test-Repository");
            WriteInColor($"Repository deleted, status code: {deleteRepoResult.StatusDescription}", ConsoleColor.Green);
            Console.WriteLine();

            Console.ReadKey();
        }

        private static void WriteInColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void WriteResult(Repository repository)
        {
            Console.WriteLine($"Name: {repository.Name}");
            Console.WriteLine($"Full name: {repository.FullName}");
            Console.WriteLine($"Description: {repository.Description ?? "None"}");
            Console.WriteLine($"Url: {repository.Url}");
            Console.WriteLine($"Private: {repository.Private}");
        }
    }
}
