using Microsoft.Extensions.Logging;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using Soenneker.GitHub.ClientUtil.Abstract;
using Soenneker.GitHub.OpenApiClient.Repos.Item.Item.Contents.Item;
using Soenneker.GitHub.Repositories.Readmes.Abstract;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.GitHub.OpenApiClient;
using Soenneker.GitHub.OpenApiClient.Models;

namespace Soenneker.GitHub.Repositories.Readmes;

/// <inheritdoc cref="IGitHubRepositoriesReadmesUtil" />
public sealed class GitHubRepositoriesReadmesUtil : IGitHubRepositoriesReadmesUtil
{
    private readonly ILogger<GitHubRepositoriesReadmesUtil> _logger;
    private readonly IGitHubOpenApiClientUtil _gitHubOpenApiClientUtil;

    public GitHubRepositoriesReadmesUtil(ILogger<GitHubRepositoriesReadmesUtil> logger, IGitHubOpenApiClientUtil gitHubOpenApiClientUtil)
    {
        _logger = logger;
        _gitHubOpenApiClientUtil = gitHubOpenApiClientUtil;
    }

    public async ValueTask Create(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating README.md for GitHub repository ({owner}/{name})...", owner, name);

        GitHubOpenApiClient client = await _gitHubOpenApiClientUtil.Get(cancellationToken).NoSync();

        var requestBody = new ReposCreateOrUpdateFileContentsRequest
        {
            Message = commitMessage,
            Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content)),
            Branch = branch
        };

        await client.Repos[owner][name].Contents["README.md"].PutAsync(requestBody, cancellationToken: cancellationToken).NoSync();
    }

    public async ValueTask Update(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating README.md for GitHub repository ({owner}/{name})...", owner, name);

        GitHubOpenApiClient client = await _gitHubOpenApiClientUtil.Get(cancellationToken).NoSync();

        // Get the current file to get its SHA
        ReposGetContent200Response? response = await client.Repos[owner][name].Contents["README.md"].GetAsync(
            body: new WithPathGetRequestBody(),
            requestConfiguration => requestConfiguration.QueryParameters.Ref = branch,
            cancellationToken).NoSync();

        if (response?.ContentFile == null)
            throw new FileNotFoundException($"README.md was not found in {owner}/{name} on branch {branch}");

        var requestBody = new ReposCreateOrUpdateFileContentsRequest
        {
            Message = commitMessage,
            Content = Convert.ToBase64String(Encoding.UTF8.GetBytes(content)),
            Branch = branch,
            Sha = response.ContentFile.Sha
        };

        await client.Repos[owner][name].Contents["README.md"].PutAsync(requestBody, cancellationToken: cancellationToken);
    }

    public async ValueTask Upsert(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Upserting README.md for GitHub repository ({owner}/{name})...", owner, name);

        try
        {
            await Update(owner, name, commitMessage, content, branch, cancellationToken).NoSync();
        }
        catch (BasicError ex) when (ex.ResponseStatusCode == 404)
        {
            _logger.LogInformation("Existing README.md was not found, creating new one...");
            await Create(owner, name, commitMessage, content, branch, cancellationToken).NoSync();
        }
        catch (FileNotFoundException)
        {
            _logger.LogInformation("Existing README.md was not found, creating new one...");
            await Create(owner, name, commitMessage, content, branch, cancellationToken).NoSync();
        }
    }
}
