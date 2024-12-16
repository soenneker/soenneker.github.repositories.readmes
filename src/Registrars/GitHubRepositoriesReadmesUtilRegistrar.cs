using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.GitHub.Client.Registrars;
using Soenneker.GitHub.Repositories.Readmes.Abstract;

namespace Soenneker.GitHub.Repositories.Readmes.Registrars;

/// <summary>
/// A utility library for GitHub repository readme related operations
/// </summary>
public static class GitHubRepositoriesReadmesUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IGitHubRepositoriesReadmesUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddGitHubRepositoriesReadmesUtilAsSingleton(this IServiceCollection services)
    {
        services.AddGitHubClientUtilAsSingleton();
        services.TryAddSingleton<IGitHubRepositoriesReadmesUtil, GitHubRepositoriesReadmesUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IGitHubRepositoriesReadmesUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddGitHubRepositoriesReadmesUtilAsScoped(this IServiceCollection services)
    {
        services.AddGitHubClientUtilAsSingleton();
        services.TryAddScoped<IGitHubRepositoriesReadmesUtil, GitHubRepositoriesReadmesUtil>();

        return services;
    }
}