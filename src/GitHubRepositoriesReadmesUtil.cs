using Microsoft.Extensions.Logging;
using Octokit;
using Soenneker.GitHub.Client.Abstract;
using Soenneker.GitHub.Repositories.Readmes.Abstract;
using System.Threading.Tasks;
using System.Threading;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.GitHub.Repositories.Readmes;

/// <inheritdoc cref="IGitHubRepositoriesReadmesUtil"/>
public class GitHubRepositoriesReadmesUtil : IGitHubRepositoriesReadmesUtil
{
    private readonly ILogger<GitHubRepositoriesReadmesUtil> _logger;
    private readonly IGitHubClientUtil _gitHubClientUtil;

    public GitHubRepositoriesReadmesUtil(ILogger<GitHubRepositoriesReadmesUtil> logger, IGitHubClientUtil gitHubClientUtil)
    {
        _logger = logger;
        _gitHubClientUtil = gitHubClientUtil;
    }

    public async ValueTask Create(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating README.md for GitHub repository ({owner}/{name})...", owner, name);

        await (await _gitHubClientUtil.Get(cancellationToken).NoSync()).Repository.Content.CreateFile(owner, name, "README.md",
            new CreateFileRequest(commitMessage, content, branch)).NoSync();
    }

    public async ValueTask Update(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating README.md for GitHub repository ({owner}/{name})...", owner, name);

        await (await _gitHubClientUtil.Get(cancellationToken).NoSync()).Repository.Content.UpdateFile(owner, name, "README.md",
            new UpdateFileRequest(commitMessage, content, branch)).NoSync();
    }

    public async ValueTask Upsert(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Upserting README.md for GitHub repository ({owner}/{name})...", owner, name);

        try
        {
            await Update(owner, name, commitMessage, content, branch, cancellationToken).NoSync();
        }
        catch (NotFoundException)
        {
            _logger.BeginScope("Existing README.md was not found...");

            await Create(owner, name, commitMessage, content, branch, cancellationToken).NoSync();
        }
    }
}