using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Attaching...");
        Console.ReadKey();
        string[] input = System.IO.File.ReadAllLines(@"input.txt");

        //PartOne(input);
        PartTwo(input);
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

    private static void PartTwo(string[] input) //the answer was 1053, which is off by one from what I got (1054)
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        var dependencies = new SortedDictionary<string, string>();

        // foreach (var letter in "abcdef".ToUpper().ToArray()) //test
        // {
        //     dependencies.Add(letter.ToString(), "");
        // }

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

        const int numWorkers = 5;
        const int baseTime = 60;
        var workers = new List<Worker>();
        for (int i = 0; i < numWorkers; i++)
        {
            workers.Add(new Worker(i));
        }

        while (complete.Length < 26)
        {
            foreach (var worker in workers)
            {
                if (worker.TimeLeft <= 0)
                {
                    if (!string.IsNullOrEmpty(worker.CurrentTask))
                    {
                        Console.Write(worker.TimeLeft + " ");
                        Console.WriteLine("Completed " + worker.CurrentTask + " at " + tick);
                        complete += worker.CurrentTask;
                        foreach (var key in dependencies.Keys.ToArray())
                        {
                            dependencies[key] = dependencies[key].Replace(worker.CurrentTask, "");
                        }
                        worker.CurrentTask = "";
                    }

                    string next = dependencies.FirstOrDefault(d => string.IsNullOrWhiteSpace(d.Value)).Key ?? "";
                    int time = next.Length > 0 ? next.ToCharArray()[0] - 64 + baseTime : -1;
                    worker.CurrentTask = next;
                    worker.TimeLeft = time;
                    dependencies.Remove(worker.CurrentTask);

                    if (!string.IsNullOrEmpty(next)) Console.WriteLine("Started: " + next + " at " + tick + " time: " + time);
                }
                worker.TimeLeft--;
            }
            tick++;
        }
        Console.WriteLine(complete);
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
}

internal class Worker
{
    public int ID { get; private set; }
    public int TimeLeft { get; set; }
    public string CurrentTask { get; set; }

    public Worker(int id)
    {
        ID = id;
        CurrentTask = "";
        TimeLeft = -1;
    }
}
