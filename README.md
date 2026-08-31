[![](https://img.shields.io/nuget/v/soenneker.github.repositories.readmes.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.github.repositories.readmes/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.github.repositories.readmes/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.github.repositories.readmes/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.github.repositories.readmes.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.github.repositories.readmes/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.github.repositories.readmes/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.github.repositories.readmes/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.GitHub.Repositories.Readmes

Create or replace a repository's `README.md` with a GitHub commit on the branch you choose.

## Installation

```bash
dotnet add package Soenneker.GitHub.Repositories.Readmes
```

## Configuration

```json
{
  "GH": {
    "Token": "github-token"
  }
}
```

The token must have repository contents write access.

## Registration

```csharp
services.AddGitHubRepositoriesReadmesUtilAsSingleton();
```

Use `AddGitHubRepositoriesReadmesUtilAsScoped()` for a scoped consumer.

## Usage

```csharp
public sealed class RepositoryReadmeService
{
    private readonly IGitHubRepositoriesReadmesUtil _readmes;

    public RepositoryReadmeService(IGitHubRepositoriesReadmesUtil readmes)
    {
        _readmes = readmes;
    }

    public ValueTask Publish(
        string markdown,
        CancellationToken cancellationToken = default)
    {
        return _readmes.Upsert(
            owner: "soenneker",
            name: "example-repository",
            commitMessage: "Update README",
            content: markdown,
            branch: "main",
            cancellationToken);
    }
}
```

- `Create` fails when `README.md` already exists.
- `Update` reads the file's current SHA from the requested branch, then replaces its contents.
- `Upsert` updates the file when present and creates it only when GitHub reports it missing.

Each successful call creates a commit. The supplied `content` replaces the entire file; it is not merged with the existing Markdown. The branch must already exist.
