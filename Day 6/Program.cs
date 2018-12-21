using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] input = System.IO.File.ReadAllLines(@"input.txt");

        //PartOne(input);
        PartTwo(input);
    }

    private static void PartOne(string[] input)
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        //parse input into point tuples
        Tuple<int, int>[] inputPoints = new Tuple<int, int>[input.Length];
        Dictionary<Tuple<int, int>, int> outputPoints = new Dictionary<Tuple<int, int>, int>();
        int left = int.MaxValue;
        int right = -1;
        int top = -1;
        int bottom = int.MaxValue;
        for (int i = 0; i < input.Length; i++)
        {
            string value = input[i];
            var split = value.Split(',');
            var x = int.Parse(split[0].Trim());
            var y = int.Parse(split[1].Trim());
            var point = new Tuple<int, int>(x, y);
            left = Math.Min(point.Item1, left);
            right = Math.Max(point.Item1, right);
            top = Math.Max(point.Item2, top);
            bottom = Math.Min(point.Item2, bottom);
            inputPoints[i] = point;
        }

        Console.Write(left + " ");
        Console.Write(right + " ");
        Console.Write(top + " ");
        Console.WriteLine(bottom);

        int minDistance;
        int minPoint;
        for (int y = top; y >= bottom; y--)
        {
            for (int x = left; x <= right; x++)
            {
                minDistance = int.MaxValue;
                minPoint = -1;
                Tuple<int, int> outputPoint = new Tuple<int, int>(x, y);
                for (int i = 0; i < inputPoints.Length; i++)
                {
                    Tuple<int, int> inputPoint = inputPoints[i];
                    var distance = Distance(inputPoint, outputPoint);
                    if (distance == minDistance)
                    {
                        minPoint = -1;
                        break;
                    }
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minPoint = i;
                    }
                }
                outputPoints.Add(outputPoint, minPoint);
            }
        }

        int maxArea = -1;
        int maxPoint = -1;
        for (int i = 0; i <= input.Length; i++)
        {
            var area = outputPoints.Count(p => p.Value == i);
            if (area > maxArea)
            {
                maxArea = area;
                maxPoint = i;
            }
        }

        Console.WriteLine(maxPoint);
        Console.WriteLine(maxArea);

        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }

    private static void PartTwo(string[] input)
    {
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        //parse input into point tuples
        Tuple<int, int>[] inputPoints = new Tuple<int, int>[input.Length];
        int left = int.MaxValue;
        int right = -1;
        int top = -1;
        int bottom = int.MaxValue;
        for (int i = 0; i < input.Length; i++)
        {
            string value = input[i];
            var split = value.Split(',');
            var x = int.Parse(split[0].Trim());
            var y = int.Parse(split[1].Trim());
            var point = new Tuple<int, int>(x, y);
            left = Math.Min(point.Item1, left);
            right = Math.Max(point.Item1, right);
            top = Math.Max(point.Item2, top);
            bottom = Math.Min(point.Item2, bottom);
            inputPoints[i] = point;
        }

        Console.Write(left + " ");
        Console.Write(right + " ");
        Console.Write(top + " ");
        Console.WriteLine(bottom);

        bool found;
        int distance;
        int area = 0;
        for (int y = top; y >= bottom; y--)
        {
            for (int x = left; x <= right; x++)
            {
                Tuple<int, int> outputPoint = new Tuple<int, int>(x, y);
                distance = 0;
                for (int i = 0; i < inputPoints.Length; i++)
                {
                    Tuple<int, int> inputPoint = inputPoints[i];
                    distance += Distance(inputPoint, outputPoint);
                }
                if (distance < 10000)
                {
                    area++;
                }
            }
        }

        Console.WriteLine(area);
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }

    private static int Distance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    private static int Distance(Tuple<int, int> point1, Tuple<int, int> point2)
    {
        return Distance(point1.Item1, point1.Item2, point2.Item1, point2.Item2);
    }
}
