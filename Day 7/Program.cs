using System;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] input = System.IO.File.ReadAllLines(@"input.txt");

        PartOne(input);
        //PartTwo(input);
    }

    private static void PartOne(string[] input)
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }

    private static void PartTwo(string[] input)
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
}
