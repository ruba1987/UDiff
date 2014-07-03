using System;
using System.IO;
using System.Linq;
using LibGit2Sharp;

namespace git_umerge
{
    class Program
    {
        static string tempDir;

        const string mergeHelp = "--mergehelp";

        static void Main(string[] args)
        {
            Setup();

            ParseArgs(args);

            using (var repo = new Repository(Environment.CurrentDirectory))
            {
                foreach (var conflict in repo.Index.Conflicts.Where(x => x.Ours.Path.EndsWith(".uasset")))
                {
                    string fixedFile;
                    try
                    {
                        fixedFile = Directory.GetFiles(tempDir).Single(x => x.Contains(Path.GetFileNameWithoutExtension(conflict.Ours.Path)));
                    }
                    catch
                    {
                        Console.WriteLine(string.Format("Conflicted file {0} did not match any files detected by udiff. Skipping the file. \r\n for more information on this run git umerge {1}", conflict.Ours.Path, mergeHelp));
                        continue;
                    }

                    Console.WriteLine(string.Format("Using {0} as the merged file for {1}", fixedFile, conflict.Ours.Path));
                    File.Delete(conflict.Ours.Path);
                    File.Move(fixedFile, conflict.Ours.Path);
                    repo.Index.Stage(conflict.Ours.Path);
                }
            }

            Teardown();
        }

        private static void ParseArgs(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == mergeHelp)
                {
                    Console.WriteLine(File.ReadAllText(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "helpfiles", "mergehelp.txt")));
                    Environment.Exit(0);
                }
            }
        }

        static void Teardown()
        {
            Directory.Delete(tempDir, true);
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
        }
    }
}
