# OptalCP.Bin.Preview

Preview edition binaries for the OptalCP constraint programming solver.

## Installation

Configure the ScheduleOpt NuGet source (one-time setup):

```bash
dotnet nuget add source "https://nuget.pkg.github.com/ScheduleOpt/index.json" \
  --name ScheduleOpt --username YOUR_GITHUB_USERNAME --password YOUR_GITHUB_TOKEN
```

Install the package:

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
Contact ScheduleOpt for licensing options (academic licenses are free).

## Usage

Once installed, the `OptalCP` package will automatically find and use this binary:

```csharp
using OptalCP;

var model = new Model();
// ... build your model ...

var result = await Solver.SolveAsync(model);
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
- macOS (ARM64, x86_64)
- Windows (x64)

## License

See LICENSE.md file in the package for license terms.
