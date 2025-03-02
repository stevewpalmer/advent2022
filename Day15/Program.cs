using System.Text.RegularExpressions;

int min = int.MaxValue;
int max = int.MinValue;
foreach (string line in File.ReadAllLines("puzzle.txt")) {
    Match match = Regex.Match(line, @"Sensor at x=(\-*\d+), y=(\-*\d+): closest beacon is at x=(-*\d+), y=(\-*\d+)");
    int[] values = match.Groups.Values.Skip(1).Select(g => int.Parse(g.Value)).ToArray();
    (int sx, int sy, int bx, int by) = (values[0], values[1], values[2], values[3]);
    int md = Math.Abs(bx - sx) + Math.Abs(by - sy);
    int diff = Math.Abs(sy - 2000000);
    if (diff < md) {
        min = Math.Min(min, sx - (md - diff));
        max = Math.Max(max, sx + (md - diff));
    }
}

long answer1 = max - min;
Console.WriteLine($"Part 1 answer: {answer1}");