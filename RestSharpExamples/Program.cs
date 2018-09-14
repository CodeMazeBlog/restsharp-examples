
using System;
using RestSharpExamples.Model;

namespace RestSharpExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RestSharp examples.");
            Console.WriteLine();

            var flurlRequestHandler = new RequestHandler();

            var repositories = flurlRequestHandler.GetRepositories();

            //Console.WriteLine($"Number of repositories {repositories.Count}");
            //Console.WriteLine();
            //foreach (var repository in repositories)
            //{
            //    Console.WriteLine("------------------------------------------------------------------------------------------------");
            //    WriteInColor("Repository", ConsoleColor.Green);
            //    WriteResult(repository);
            //}
            Console.WriteLine();
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
