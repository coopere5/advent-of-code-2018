using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] input = System.IO.File.ReadAllLines(@"input.txt");

        PartOne(input);
        //PartTwo();
    }

    private static void PartOne(string[] input)
    {
        SortedDictionary<DateTime, string> entries = new SortedDictionary<DateTime, string>();

        Dictionary<int, int[]> guards = new Dictionary<int, int[]>();

        Dictionary<int, int> minutes = new Dictionary<int, int>();


        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        foreach (var value in input)
        {
            DateTime timestamp = DateTime.Parse(value.Substring(1, 16));
            string entry = value.Substring(19);
            entries.Add(timestamp, entry);
        }

        int currentGuard = -1;
        int fellAsleep = -1;

        foreach (var entry in entries)
        {
            switch (entry.Value)
            {
                case "wakes up":
                    int wokeUp = entry.Key.Minute;
                    for (int i = fellAsleep; i < wokeUp; i++)
                    {
                        guards[currentGuard][i]++;
                    }
                    break;
                case "falls asleep":
                    fellAsleep = entry.Key.Minute;
                    break;
                default:
                    string[] split = entry.Value.Split(' ');
                    currentGuard = int.Parse(split[1].Substring(1));
                    if (!guards.Keys.Contains(currentGuard))
                    {
                        guards.Add(currentGuard, new int[60]);
                    }
                    break;
            }
        }

        int mostAsleepGuard = -1;
        int mostAsleepTimeGuard = -1;
        foreach (var guard in guards)
        {
            int currentSleep = 0;
            foreach (var value in guard.Value)
            {
                currentSleep += value;
            }
            if (currentSleep > mostAsleepTimeGuard)
            {
                mostAsleepGuard = guard.Key;
                mostAsleepTimeGuard = currentSleep;
            }
        }
        Console.WriteLine("Most asleep guard: " + mostAsleepGuard + " for " + mostAsleepTimeGuard);
        int idx = 0;
        int max = 0;
        int maxIdx = 0;
        foreach (var minute in guards[mostAsleepGuard])
        {
            Console.Write(idx);
            Console.Write(": ");
            Console.WriteLine(minute);
            if (minute > max)
            {
                max = minute;
                maxIdx = idx;
            }
            idx++;
        }

        Console.WriteLine("Max: " + maxIdx + ": " + max);

        sw.Stop();
        Console.WriteLine(sw.Elapsed);

        sw = System.Diagnostics.Stopwatch.StartNew();
        Console.WriteLine("====");
        int part2Guard = -1;
        int part2Minute = -1;
        int part2Time = -1;
        foreach (var guard in guards)
        {
            idx = 0;
            foreach (var value in guard.Value)
            {
                if(value > part2Time)
                {
                    part2Time = value;
                    part2Minute = idx;
                    part2Guard = guard.Key;
                }
                idx++;
            }
        }

        Console.WriteLine("Part 2: Guard: " + part2Guard + " Minute: " + part2Minute);
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }

    private static void PartTwo()
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
}
