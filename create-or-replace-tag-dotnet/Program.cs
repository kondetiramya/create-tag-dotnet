using Octokit;
using System;
using System.Threading.Tasks;

namespace create_replace_tag_dotnet
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var owner = Environment.GetEnvironmentVariable("GITHUB_ACTOR");
            var repo = Environment.GetEnvironmentVariable("GITHUB_REPOSITORY");
            var sha = Environment.GetEnvironmentVariable("GITHUB_SHA");

            var token = args[0];
            var tag = args[1];
            Console.WriteLine($"Tag Name: {tag}");
            var reference = $"tags/{tag}";
            var clientCredentials = new Credentials(token);

            var client = new GitHubClient(new ProductHeaderValue(repo.Split("/")[1]));
            client.Credentials = clientCredentials;

            Reference existingReference = null;
            try
            {
                existingReference = await client.Git.Reference.Get(owner, repo, reference);
            }
            catch (NotFoundException ex)
            {
                Console.WriteLine($"Could not find image: {ex.Message}");
            }

            if (existingReference == null)
            {
                Console.WriteLine("Creating new reference");
                try
                {
                    var newReference = await client.Git.Reference.Create(owner, repo, new NewReference($"refs/{reference}", sha));
                }catch(Exception ex)
                {
                    Console.WriteLine($"Could not create tag: {ex.StackTrace}");
                }
            }
        }
    }
}
