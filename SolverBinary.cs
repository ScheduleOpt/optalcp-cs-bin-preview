using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OptalCP.Bin.Preview
{
    /// <summary>
    /// Provides the path to the bundled OptalCP solver binary for this platform.
    /// </summary>
    public static class SolverBinary
    {
        /// <summary>
        /// Get path to the bundled optalcp binary for this platform.
        /// </summary>
        /// <returns>Absolute path to the optalcp executable.</returns>
        /// <exception cref="PlatformNotSupportedException">The current platform is not supported.</exception>
        /// <exception cref="FileNotFoundException">The binary was not found in the package.</exception>
        public static string GetSolverPath()
        {
            string rid = GetRuntimeIdentifier();
            string binaryName = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? "optalcp.exe" : "optalcp";

            // Check application output directory (where .targets copies the binary)
            string basePath = Path.Combine(AppContext.BaseDirectory, binaryName);
            if (File.Exists(basePath))
                return basePath;

            // Check runtimes/ relative to this assembly (NuGet package cache layout:
            // lib/net9.0/OptalCP.Bin.Preview.dll -> ../../runtimes/{rid}/native/)
            string? assemblyDir = Path.GetDirectoryName(typeof(SolverBinary).Assembly.Location);
            if (assemblyDir != null)
            {
                string nugetPath = Path.GetFullPath(
                    Path.Combine(assemblyDir, "..", "..", "runtimes", rid, "native", binaryName));
                if (File.Exists(nugetPath))
                    return nugetPath;

                // Project reference fallback: runtimes/ is in the project directory
                string projectPath = Path.Combine(assemblyDir, "runtimes", rid, "native", binaryName);
                if (File.Exists(projectPath))
                    return projectPath;
            }

            throw new FileNotFoundException(
                $"OptalCP solver binary not found for {rid}. " +
                "The OptalCP.Bin.Preview package may be incomplete or corrupted.");
        }

        private static string GetRuntimeIdentifier()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) &&
                RuntimeInformation.OSArchitecture == Architecture.X64)
                return "linux-x64";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) &&
                RuntimeInformation.OSArchitecture == Architecture.Arm64)
                return "osx-arm64";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) &&
                RuntimeInformation.OSArchitecture == Architecture.X64)
                return "osx-x64";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) &&
                RuntimeInformation.OSArchitecture == Architecture.X64)
                return "win-x64";

            throw new PlatformNotSupportedException(
                $"OptalCP does not support {RuntimeInformation.OSDescription} " +
                $"({RuntimeInformation.OSArchitecture}). " +
                "Supported platforms: linux-x64, osx-arm64, osx-x64, win-x64");
        }
    }
}
