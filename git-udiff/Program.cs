using System;
using GitSharp;
using System.IO;

namespace git_udiff
{
    class Program
    {
	static string tempDir;
        static void Main(string[] args)
        {
            using (var repo = new Repository(Environment.CurrentDirectory))
            {
                Console.WriteLine(Environment.CurrentDirectory);
                foreach (var conflict in repo.Status.MergeConflict)
                {
                    Console.WriteLine(conflict);
                }
            }
            SetupTempFolder();
        }

        static void SetupTempFolder()
        {
            var contentDirs = Directory.GetDirectories(Environment.CurrentDirectory, "Content");

	    //TODO: turn this into an error that asks the user what directory they want to look in.
            if (contentDirs.Length != 1)
            {
                throw new Exception("Could not find content directory. Make sure you are in a UE4 project and are at the root directory.");
            }

            tempDir = Directory.CreateDirectory(Path.Combine(contentDirs[0], "UDiff")).FullName;

            Console.WriteLine(tempDir);
        }
    }
}
