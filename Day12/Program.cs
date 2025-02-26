using System.Drawing;

char[][] maze = File.ReadAllLines("puzzle.txt").Select(l => l.ToCharArray()).ToArray();
int h = maze.Length;
int w = maze[0].Length;
Point start = new(0, 0);
Point end = new(0, 0);
List<Point> allStarts = [];
for (int y = 0; y < h; y++) {
    for (int x = 0; x < w; x++) {
        if (maze[y][x] == 'S') {
            start = new Point(x, y);
            maze[y][x] = 'a';
        }
        if (maze[y][x] == 'E') {
            end = new Point(x, y);
            maze[y][x] = 'z';
        }
        if (maze[y][x] == 'a') {
            allStarts.Add(new Point(x, y));
        }
    }
}

Console.WriteLine($"Part 1 answer: {Walk([start])}");
Console.WriteLine($"Part 2 answer: {Walk(allStarts)}");
return;

long Walk(IEnumerable<Point> startPositions) {
    Queue<(Point, int)> queue = new();
    HashSet<Point> visited = [];
    foreach (Point startPosition in startPositions) {
        queue.Enqueue((startPosition, 0));
    }
    int best = int.MaxValue;
    while (queue.TryDequeue(out var element)) {
        (Point pt, int steps) = element;
        if (pt == end) {
            best = Math.Min(steps, best);
        }
        foreach ((int dx, int dy) in ((int, int)[])[(0, -1), (1, 0), (0, 1), (-1, 0)]) {
            Point step = new(pt.X + dx, pt.Y + dy);
            if (step.Y >= 0 && step.Y < h && step.X >= 0 && step.X < w && maze[step.Y][step.X] - maze[pt.Y][pt.X] <= 1 && !visited.Contains(step)) {
                queue.Enqueue((step, steps + 1));
                visited.Add(step);
            }
        }
    }
    return best;
}