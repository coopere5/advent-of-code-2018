using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        string input = System.IO.File.ReadAllText(@"input.txt");

        //PartOne(input);
        PartTwo(input);
    }

    private static void PartOne(string input)
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
        Console.WriteLine(input.Length);
        List<char> array = input.ToCharArray().ToList();
        for (int i = 0; i < array.Count - 1; i++)
        {
            char a = array[i];
            char b = array[i + 1];

            if (a.ToString().ToUpper() == b.ToString().ToUpper() && a != b)
            {
                array.RemoveAt(i + 1);
                array.RemoveAt(i);
                i -= 2;
                if(i<0)i=0;
            }
        }
        Console.WriteLine(array.Count);
        //Console.WriteLine("'"+string.Join("", array.ToArray())+"'");
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }

    private static void PartTwo(string input)
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
        string original = input;
        int minimum = int.MaxValue;
        foreach(var letter in "abcdefghijklmnopqrstuvqxyz".ToCharArray())
        {
            input = original;
            input = input.Replace(letter.ToString(), "");
            input = input.Replace(letter.ToString().ToUpper(), "");

            List<char> array = input.ToCharArray().ToList();
            for (int i = 0; i < array.Count - 1; i++)
            {
                char a = array[i];
                char b = array[i + 1];

                if (a.ToString().ToUpper() == b.ToString().ToUpper() && a != b)
                {
                    array.RemoveAt(i + 1);
                    array.RemoveAt(i);
                    i -= 2;
                    if(i<0)i=0;
                }
            }
            Console.WriteLine(array.Count);

            if(array.Count < minimum) minimum = array.Count;
        }
        Console.WriteLine("Minimum: " + minimum);
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
}
