using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.GitHub.Repositories.Readmes.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.GitHub.Repositories.Readmes.Tests;

[Collection("Collection")]
public class GitHubRepositoriesReadmesUtilTests : FixturedUnitTest
{
    private readonly IGitHubRepositoriesReadmesUtil _util;

    public GitHubRepositoriesReadmesUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IGitHubRepositoriesReadmesUtil>(true);
    }
}
