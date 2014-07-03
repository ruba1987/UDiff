using System;
using System.IO;
using LibGit2Sharp;
using System.Linq;

namespace git_udiff
{
    class Program
    {
        static string tempDir;
        static void Main(string[] args)
        {
            Setup();
            using (var repo = new Repository(Environment.CurrentDirectory))
            {
                if (repo.Index.Conflicts.Any())
                    Console.WriteLine("Found UASSET conflicts. Building folder for diff");

                foreach (var conflict in repo.Index.Conflicts.Where(x => x.Ours.Path.EndsWith(".uasset")))
                {
                    Console.WriteLine("Setting up diff files for " + conflict.Ours.Path);
                    var a = repo.Lookup<Blob>(conflict.Ours.Id);
                    var b = repo.Lookup<Blob>(conflict.Theirs.Id);

                    using (FileStream fileStream = File.Create(Path.Combine(tempDir, Path.GetFileNameWithoutExtension(conflict.Ours.Path) + "_a" + Path.GetExtension(conflict.Ours.Path))))
                    {
                        a.GetContentStream().CopyTo(fileStream);
                    }

                    using (FileStream fileStream = File.Create(Path.Combine(tempDir, Path.GetFileNameWithoutExtension(conflict.Theirs.Path) + "_b" + Path.GetExtension(conflict.Theirs.Path))))
                    {
                        b.GetContentStream().CopyTo(fileStream);
                    }
                }
            }
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
