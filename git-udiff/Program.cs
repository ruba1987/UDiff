using System;
using System.IO;
using LibGit2Sharp.Core;
using LibGit2Sharp;

namespace git_udiff
{
    class Program
    {
        static string tempDir;
        static void Main(string[] args)
        {

            using (var repo = new Repository(Environment.CurrentDirectory))
            {
                foreach (var item in repo.Index.RetrieveStatus())
                {
                    if (item.State.HasFlag(FileStatus.Removed) && item.State.HasFlag(FileStatus.Untracked))
                    {
                        Console.WriteLine(item.FilePath);
                        Console.WriteLine(item.HeadToIndexRenameDetails.
                    }
                }
            }

            Setup();

        }

        static void Setup()
        {
            var contentDirs = Directory.GetDirectories(Environment.CurrentDirectory, "Content");

            //TODO: turn this into an error that asks the user what directory they want to look in.
            if (contentDirs.Length != 1)
            {
                throw new Exception("Could not find content directory. Make sure you are in a UE4 project and are at the root directory.");
            }

            tempDir = Directory.CreateDirectory(Path.Combine(contentDirs[0], "UDiff")).FullName;

            Environment.SetEnvironmentVariable("UDiffTempDir", tempDir);
        }
    }
}
