using System;
using Stars.Container;

namespace Process
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var task = (new StarsProcess()).RunAsync(args);
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}