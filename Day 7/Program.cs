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
            dependencies[after] += before;
        }

        int tick = 0;
        string complete = string.Empty;

        const int workers = 5;
        const int baseTime = 60;
        var workers = new List<Worker>();
        for (int i = 0; i < 5; i++)
        {
            workers.Add(new Worker(i));
        }

        while (complete.Length < 26)
        {
            foreach (var worker in workers)
            {
                if (worker.TimeLeft>=0)
                {
                    complete += worker.CurrentTask;
                    string next = dependencies.FirstOrDefault(d => string.IsNullOrWhiteSpace(d.Value)).Key ?? "";
                    int time = next.Length > 0 ? next.ToCharArray()[0] - 64 + baseTime : -1;
                    worker.CurrentTask = next;
                    worker.TimeLeft = time;
                }
                worker.TimeLeft--;
            }
            tick++;
        }


        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
}

internal class Worker
{
    public readonly int ID { get; }
    public int TimeLeft { get; set; }
    public string CurrentTask { get; set; }

    Worker(int id)
    {
        ID = id;
        CurrentTask = "";
        TimeLeft = -1;
    }
}
