using System;
using System.Collections.Generic;

internal class Program
{
    private static void Main()
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        int total = 0;
        var frequencies = new HashSet<int> { 0 };

        var input = System.IO.File.ReadAllLines(@"input.txt");
        while (true)
        {
            foreach (string item in input)
            {
                int delta = int.Parse(item);
                total += delta;
                if (frequencies.Add(total)) continue;
                sw.Stop();
                Console.WriteLine("Repeat frequency: " + total);
                Console.WriteLine(sw.Elapsed);
                Console.ReadKey();
                return;
            }
        }
    }

    private void OldVersion()
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        int total = 0;
        var frequencies = new List<int> { 0 };

        var input = System.IO.File.ReadAllLines(@"input.txt");
        while (true)
        {
            foreach (string item in input)
            {
                int delta = int.Parse(item);
                total += delta;
                if (frequencies.Contains(total))
                {
                    sw.Stop();
                    Console.WriteLine("Repeat frequency: " + total);
                    Console.WriteLine(sw.Elapsed);
                    Console.ReadKey();
                    return;
                }

                frequencies.Add(total);
            }
        }
    }
}
