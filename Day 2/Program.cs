using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static int two = 0;
    static int three = 0;
    static void Main(string[] args)
    {
        string[] input = System.IO.File.ReadAllLines(@"C:\Users\ecooper\Desktop\AoC\Day 2\input.txt");

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = i + 1; j < input.Length; j++)
            {
                if (ExactlyOneOff(input[i], input[j]))
                {
                    Console.WriteLine(input[i]);
                    Console.WriteLine(input[j]);
                    Console.ReadKey();
                    return;
                }
            }
        }
    }

    private static void ContainsRepeats(string value)
    {
        Dictionary<char, int> dict = new Dictionary<char, int>();

        foreach (var character in value.ToCharArray())
        {
            if (dict.Keys.Contains(character))
            {
                dict[character]++;
            }
            else
            {
                dict.Add(character, 1);
            }
        }

        if (dict.Values.Contains(2))
        {
            two++;
        }

        if (dict.Values.Contains(3))
        {
            three++;
        }
    }

    private static bool ExactlyOneOff(string s1, string s2)
    {
        char[] a1 = s1.ToCharArray();
        char[] a2 = s2.ToCharArray();
        int diff = 0;
        for (int i = 0; i < a1.Length; i++)
        {
            if (a1[i] != a2[i])
            {
                diff++;
                if (diff > 1) return false;
            }
        }
        return true;
    }

    private static void PartOne()
    {
        string[] input = System.IO.File.ReadAllLines(@"C:\Users\ecooper\Desktop\AoC\Day 2\input.txt");

        foreach (var value in input)
        {
            ContainsRepeats(value);
        }

        Console.WriteLine(two * three);
        Console.ReadKey();
    }
}
