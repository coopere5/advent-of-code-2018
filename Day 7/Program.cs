using System;
using System.Collections.Generic;
using System.Linq;

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

        var steps = new Tuple<string, string>[input.Length];
        var dependencies = new SortedDictionary<string, string>();

        foreach (var letter in "abcdefghijklmnopqrstuvwxyz".ToUpper().ToArray())
        {
            dependencies.Add(letter.ToString(), "");
        }

        for (int i = 0; i < input.Length; i++)
        {
            string s = input[i];
            string[] split = s.Split(' ');
            string before = split[1];
            string after = split[7];
            steps[i] = new Tuple<string, string>(before, after);
            dependencies[after] += before;
        }

        string completed = string.Empty;

        while (dependencies.Count > 0)
        {
            string next = dependencies.First(d => string.IsNullOrWhiteSpace(d.Value)).Key;
            completed += next;
            dependencies.Remove(next);
            foreach (var key in dependencies.Keys.ToArray())
            {
                dependencies[key] = dependencies[key].Replace(next, "");
            }
        }

        Console.WriteLine(completed);

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
