# OptalCP.Bin.Preview

Preview edition binaries for the OptalCP constraint programming solver.

## Installation

### 1. Create a GitHub Personal Access Token

This package is public, but GitHub Packages requires authentication even for
public NuGet packages. Go to [github.com/settings/tokens](https://github.com/settings/tokens)
and create a **classic** token (not fine-grained) with the `read:packages` scope.
Copy the token value.

See [GitHub's NuGet registry documentation](https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry) for details.

### 2. Configure the NuGet source (one-time setup)

```bash
dotnet nuget add source "https://nuget.pkg.github.com/ScheduleOpt/index.json" \
  --name ScheduleOpt \
  --username YOUR_GITHUB_USERNAME \
  --password YOUR_GITHUB_TOKEN \
  --store-password-in-clear-text
```

Replace `YOUR_GITHUB_USERNAME` with your GitHub username and `YOUR_GITHUB_TOKEN`
with the token from step 1.

> **Note:** `--store-password-in-clear-text` is required on Linux and macOS where
> the .NET credential encryption is not available. The token is stored in
> `~/.nuget/NuGet/NuGet.Config`.

### 3. Install the package

```bash
dotnet add package OptalCP.Bin.Preview
```

This will automatically install the `OptalCP` API package as a dependency.

## What is the Preview Edition?

The Preview edition of OptalCP is a limited version that:
- Solves problems normally
- Reports only objective values (no solution details)
- Is useful for evaluation and testing

For full functionality, install `OptalCP.Bin` (full edition) or `OptalCP.Bin.Academic` (academic edition).
Contact Optal for licensing options (academic licenses are free).

## Usage

Once installed, the `OptalCP` package will automatically find and use this binary:

```csharp
using OptalCP;

var model = new Model();
// ... build your model ...

var result = model.Solve();
Console.WriteLine($"Objective: {result.Objective}");
```

## Upgrading to Full Edition

To upgrade to the full edition, replace the package reference:

```bash
dotnet remove package OptalCP.Bin.Preview
dotnet add package OptalCP.Bin
```

The full edition will automatically take priority over the preview edition.

## Supported Platforms

- Linux (x86_64)
- macOS (ARM64)
- Windows (x64)

## License

See LICENSE.md file in the package for license terms.
