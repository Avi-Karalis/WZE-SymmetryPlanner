using System.Diagnostics;

namespace WZE_Symmetry_Planner.Utilities {
    public static class CommandHelper {
        public static void RunCommand(string fileName, string arguments) {
            var psi = new ProcessStartInfo {
                FileName = fileName,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi)!;
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            if (!string.IsNullOrWhiteSpace(output))
                Console.WriteLine(output);
            if (!string.IsNullOrWhiteSpace(error))
                Console.WriteLine(error);
        }
    }
}
