using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.GitHub.Repositories.Readmes.Abstract;

/// <summary>
/// A utility library for GitHub repository readme related operations
/// </summary>
/// <summary>
/// Provides utilities for managing <c>README.md</c> files in GitHub repositories, including create, update, and upsert operations.
/// </summary>
public interface IGitHubRepositoriesReadmesUtil
{
    /// <summary>
    /// Creates a new <c>README.md</c> file in the specified GitHub repository.
    /// </summary>
    /// <param name="owner">The owner of the repository (user or organization).</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="commitMessage">The commit message to associate with the creation.</param>
    /// <param name="content">The markdown content of the README file.</param>
    /// <param name="branch">The branch to commit the README to. Defaults to <c>main</c>.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Create(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing <c>README.md</c> file in the specified GitHub repository.
    /// </summary>
    /// <param name="owner">The owner of the repository (user or organization).</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="commitMessage">The commit message to associate with the update.</param>
    /// <param name="content">The new markdown content of the README file.</param>
    /// <param name="branch">The branch to commit the update to. Defaults to <c>main</c>.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Update(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the existing <c>README.md</c> file if it exists; otherwise, creates a new one.
    /// </summary>
    /// <param name="owner">The owner of the repository (user or organization).</param>
    /// <param name="name">The name of the repository.</param>
    /// <param name="commitMessage">The commit message to associate with the operation.</param>
    /// <param name="content">The markdown content of the README file.</param>
    /// <param name="branch">The branch to commit the operation to. Defaults to <c>main</c>.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Upsert(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default);
}