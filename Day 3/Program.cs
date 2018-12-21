using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        string[] input = System.IO.File.ReadAllLines(@"input.txt");
        Claim[] claims = new Claim[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            claims[i] = new Claim(input[i]);
        }

        //PartOne(claims);
        PartTwo(claims);
    }

    private static void PartOne(Claim[] claims)
    {
        Dictionary<Tuple<int, int>, int> dict = new Dictionary<Tuple<int, int>, int>();
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        foreach (var claim in claims)
        {
            for (int i = claim.Left; i < claim.Left + claim.Width; i++)
            {
                for (int j = claim.Top; j < claim.Top + claim.Height; j++)
                {
                    Tuple<int, int> coord = new Tuple<int,int>(i, j);
                    if (dict.Keys.Contains(coord)) dict[coord]++;
                    else dict.Add(coord, 1);
                }
            }
        }

        Console.WriteLine(dict.Values.Where(x => x > 1).Count()); //112418

        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }

    private static void PartTwo(Claim[] claims){
        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

        bool collided;
        for (int i = 0; i < claims.Length; i++)
        {
            collided = false;
            for(int j = 0; j < claims.Length; j++)
            {
                if(i!=j && claims[i].Collision(claims[j]))
                {
                    collided = true;
                    Console.WriteLine(claims[i].ID + " collided with " + claims[j].ID);

                    break;
                }
            }
            if(!collided)
            {
                Console.WriteLine(claims[i].ID);
                sw.Stop();
                Console.WriteLine(sw.Elapsed);
                return;
            }
        }
    }
}

internal class Claim
{
    public int ID { get; set; }
    public int Left { get; set; }
    public int Top { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Claim(string input)
    {
        string[] parsed = input.Split('#', ' ', '@', ',', ':', 'x');
        parsed = parsed.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        ID = int.Parse(parsed[0]);
        Left = int.Parse(parsed[1]);
        Top = int.Parse(parsed[2]);
        Width = int.Parse(parsed[3]);
        Height = int.Parse(parsed[4]);
    }

    public int Area { get { return Width * Height; } }
    public int Right { get { return Left + Width; } }
    public int Bottom { get { return Top + Height; } }

    public bool Collision(Claim c2)
    {
        int xL = Math.Max(this.Left, c2.Left);
        int xR = Math.Min(this.Right, c2.Right);

        if(xR <= xL) return false;

        int yT = Math.Max(this.Top, c2.Top);
        int yB = Math.Min(this.Bottom, c2.Bottom);

        if(yB<=yT) return false;

        return true;
    }
}
