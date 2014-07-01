using System;

namespace git_udiff
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("I work with args");
            foreach (var s in args)
            {
                Console.WriteLine(s);
            }
        }
    }
}
