using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.GitHub.ClientUtil.Registrars;
using Soenneker.GitHub.Repositories.Readmes.Abstract;

namespace Soenneker.GitHub.Repositories.Readmes.Registrars;

/// <summary>
/// Registers services that create and replace GitHub repository README files.
/// </summary>
public static class GitHubRepositoriesReadmesUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IGitHubRepositoriesReadmesUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddGitHubRepositoriesReadmesUtilAsSingleton(this IServiceCollection services)
    {
        services.AddGitHubOpenApiClientUtilAsSingleton().TryAddSingleton<IGitHubRepositoriesReadmesUtil, GitHubRepositoriesReadmesUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IGitHubRepositoriesReadmesUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddGitHubRepositoriesReadmesUtilAsScoped(this IServiceCollection services)
    {
        services.AddGitHubOpenApiClientUtilAsSingleton().TryAddScoped<IGitHubRepositoriesReadmesUtil, GitHubRepositoriesReadmesUtil>();

        return services;
    }
}
