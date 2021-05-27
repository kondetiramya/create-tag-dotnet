using Octokit;
using System;

namespace create_replace_tag_dotnet
{
    class Program
    {
        public async static void Main(string[] args)
        {
            var owner = Environment.GetEnvironmentVariable("GITHUB_ACTOR");
            var repo = Environment.GetEnvironmentVariable("GITHUB_REPOSITORY");
            var sha = Environment.GetEnvironmentVariable("GITHUB_SHA");

            var token = args[0];
            var tag = args[1];
            var reference = $"tags/{tag}";
            var clientCredentials = new Credentials(token);
            
            var client = new GitHubClient(new ProductHeaderValue(repo));
            client.Credentials = clientCredentials;

            var existingReference = await client.Git.Reference.Get(owner, repo, reference);
            if(existingReference == null)
            {
                Console.WriteLine("Creating new reference");
                var newReference = await client.Git.Reference.Create(owner, repo, new NewReference($"refs/{reference}", sha));
                if (newReference == null)
                {
                    throw new Exception("Could not create tag");
                }
            }
        }
    }
}
