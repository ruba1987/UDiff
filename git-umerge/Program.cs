using System;
using System.IO;
using System.Linq;
using LibGit2Sharp;

namespace git_umerge
{
    class Program
    {
        static string tempDir;

        const string mergeInfo = "--mergehelp";

        static void Main(string[] args)
        {
            ParseArgs(args);

            Console.WriteLine("Merge");

            tempDir = Environment.GetEnvironmentVariable("UDiffTempDir");

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
                        Console.WriteLine(string.Format("Conflicted file {0} did not match any files detected by udiff. Skipping the file. \r\n for more information on this run git umerge {1}", conflict.Ours.Path, mergeInfo));
                        continue;
                    }

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
                if (args[i] == mergeInfo)
                {

                }
            }
        }

        static void Teardown()
        {
            Directory.Delete(tempDir, true);
        }
    }
}
