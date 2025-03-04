using System.Text.RegularExpressions;
using Sensor = (int sx, int sy, int md);

const int area = 4000000;

int min = int.MaxValue;
int max = int.MinValue;
List<Sensor> sensors = [];
foreach (string line in File.ReadAllLines("puzzle.txt")) {
    Match match = Regex.Match(line, @"Sensor at x=(\-*\d+), y=(\-*\d+): closest beacon is at x=(-*\d+), y=(\-*\d+)");
    int[] values = match.Groups.Values.Skip(1).Select(g => int.Parse(g.Value)).ToArray();
    (int sx, int sy, int bx, int by) = (values[0], values[1], values[2], values[3]);
    int md = Math.Abs(bx - sx) + Math.Abs(by - sy);
    sensors.Add((sx, sy, md));
    int diff = Math.Abs(sy - area / 2);
    if (diff < md) {
        min = Math.Min(min, sx - (md - diff));
        max = Math.Max(max, sx + (md - diff));
    }
}
long answer1 = max - min;
long answer2 = 0;
foreach (Sensor s in sensors) {
    for (int mdx = 0; mdx < s.md + 1 && answer2 == 0; mdx++) {
        int mdy = s.md + 1 - mdx;
        foreach ((int dx, int dy) in ((int, int)[]) [(-1, -1), (-1, 1), (1, -1), (1, 1)]) {
            long x = s.sx + mdx * dx;
            long y = s.sy + mdy * dy;
            if (x is >= 0 and <= area && y is >= 0 and <= area) {
                if (!sensors.Any(s1 => Math.Abs(x - s1.sx) + Math.Abs(y - s1.sy) < s1.md)) {
                    answer2 = x * 4000000 + y;
                    break;
                }
            }
        }
    }
    if (answer2 > 0) {
        break;
    }
}
Console.WriteLine($"Part 1 answer: {answer1}");
Console.WriteLine($"Part 2 answer: {answer2}");