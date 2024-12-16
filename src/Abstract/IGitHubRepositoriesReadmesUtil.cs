using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.GitHub.Repositories.Readmes.Abstract;

/// <summary>
/// A utility library for GitHub repository readme related operations
/// </summary>
public interface IGitHubRepositoriesReadmesUtil
{
    /// <summary>
    /// Creates a README.md file in the specified GitHub repository.
    /// </summary>
    /// <param name="owner">The owner of the repository where the README.md file will be created.</param>
    /// <param name="name">The name of the repository where the README.md file will be created.</param>
    /// <param name="commitMessage">The commit message for the file creation.</param>
    /// <param name="content">The content to be placed in the README.md file.</param>
    /// <param name="branch">The branch where the README.md file will be created.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask Create(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the README.md file in the specified GitHub repository.
    /// </summary>
    /// <param name="owner">The owner of the repository where the README.md file will be updated.</param>
    /// <param name="name">The name of the repository where the README.md file will be updated.</param>
    /// <param name="commitMessage">The commit message for the file update.</param>
    /// <param name="content">The new content for the README.md file.</param>
    /// <param name="branch">The branch where the README.md file will be updated.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask Update(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default);

    /// <summary>
    /// Upserts the README.md file in the specified GitHub repository.
    /// If the file exists, it will be updated; otherwise, it will be created.
    /// </summary>
    /// <param name="owner">The owner of the repository where the README.md file will be upserted.</param>
    /// <param name="name">The name of the repository where the README.md file will be upserted.</param>
    /// <param name="commitMessage">The commit message for the file creation or update.</param>
    /// <param name="content">The content to be placed in the README.md file.</param>
    /// <param name="branch">The branch where the README.md file will be upserted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask Upsert(string owner, string name, string commitMessage, string content, string branch = "main", CancellationToken cancellationToken = default);
}