using System.Drawing;

string[] input = File.ReadAllLines("puzzle.txt");

int ModelKnots(int count) {

    HashSet<Point> map = new();
    Point[] knots = new Point[count];
    foreach (string line in input) {
        string[] code = line.Split();

        int xDirection = 0;
        int yDirection = 0;
        switch (code[0]) {
            case "R": xDirection = 1; break;
            case "L": xDirection = -1; break;
            case "U": yDirection = -1; break;
            case "D": yDirection = 1; break;
        }

        int steps = Convert.ToInt32(code[1]);
        while (steps-- > 0) {

            knots[0].Offset(xDirection, yDirection);

            int knotIndex = 0;
            while (++knotIndex < count) {

                int dx = knots[knotIndex - 1].X - knots[knotIndex].X;
                int dy = knots[knotIndex - 1].Y - knots[knotIndex].Y;

                if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1) {
                    knots[knotIndex].X += Math.Sign(dx);
                    knots[knotIndex].Y += Math.Sign(dy);
                }
            }
            map.Add(knots[count - 1]);
        }
    }
    return map.Count;
}

Console.WriteLine($"Part 1 answer : {ModelKnots(2)}");
Console.WriteLine($"Part 2 answer : {ModelKnots(10)}");
