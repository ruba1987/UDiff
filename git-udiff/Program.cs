using System;
using GitSharp;

namespace git_udiff
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new Repository(Environment.CurrentDirectory);
            var i = new Index(repo);


            Console.WriteLine(Environment.CurrentDirectory);
            foreach (var conflict in repo.Status.MergeConflict)
            {
                Console.WriteLine(conflict);
            }
        }
    }
}
