using System.Drawing;

List<List<Point>> paths = File.ReadAllLines("day14.txt")
    .Select(l => l.Split(" -> ")
    .Select(p => p.Split(','))
    .Select(x => new Point(Convert.ToInt32(x[0]), Convert.ToInt32(x[1])))
    .ToList())
    .ToList();

HashSet<Point> map = new();
int maxY = 0;

foreach (List<Point> path in paths) {
    for (int i = 1; i < path.Count; i++) {
        (Point p1, Point p2) = (path[i - 1], path[i]);

        for (int y = Math.Min(p1.Y, p2.Y); y <= Math.Max(p1.Y, p2.Y); y++) {

            for (int x = Math.Min(p1.X, p2.X); x <= Math.Max(p1.X, p2.X); x++) {
                map.Add(new Point(x, y));
            }
        }
        maxY = Math.Max(maxY, Math.Max(p1.Y, p2.Y));
    }
}

Point s = new(500, 0);
int floor = maxY + 2;

int count = 0;
int count1 = 0;
int count2 = 0;
while (count1 == 0 || count2 == 0) {

    if (s.Y > maxY && count1 == 0) {
        count1 = count;
    }
    Point sp = s with { Y = s.Y + 1 };
    if (!map.TryGetValue(sp, out Point _) && sp.Y != floor) {
        s = sp;
        continue;
    }
    sp = new(s.X - 1, s.Y + 1);
    if (!map.TryGetValue(sp, out Point _) && sp.Y != floor) {
        s = sp;
        continue;
    }
    sp = new(s.X + 1, s.Y + 1);
    if (!map.TryGetValue(sp, out Point _) && sp.Y != floor) {
        s = sp;
        continue;
    }
    map.Add(s);
    ++count;
    if (s == new Point(500, 0)) {
        count2 = count;
    }
    s = new(500, 0);
}

Console.WriteLine($"Puzzle 1 answer : {count1} units of sand.");
Console.WriteLine($"Puzzle 2 answer : {count2} units of sand.");
