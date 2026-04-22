using Soenneker.GitHub.Repositories.Readmes.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.GitHub.Repositories.Readmes.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class GitHubRepositoriesReadmesUtilTests : HostedUnitTest
{
    private readonly IGitHubRepositoriesReadmesUtil _util;

    public GitHubRepositoriesReadmesUtilTests(Host host) : base(host)
    {
        _util = Resolve<IGitHubRepositoriesReadmesUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
