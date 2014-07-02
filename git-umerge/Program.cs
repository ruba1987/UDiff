using System;
using System.IO;

namespace git_umerge
{
    class Program
    {
        static string tempDir;

        static void Main(string[] args)
        {
            Console.WriteLine("Merge");

            Environment.GetEnvironmentVariable("UDiffTempDir");
        }

        static void Teardown()
        {
            Directory.Delete(tempDir, true);
        }
    }
}
