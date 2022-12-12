int[][] input = File.ReadAllLines("day8.txt").Select(l => l.ToCharArray().Select(p => p - '0').ToArray()).ToArray();
int maxColumn = input[0].Length;
int maxRow = input.Length;

bool Left(int x, int y) => x == 0 || !input[y][..x].Any(p => p >= input[y][x]);
bool Right(int x, int y) => x == maxColumn - 1 || !input[y][(x + 1)..maxColumn].Any(p => p >= input[y][x]);
bool Above(int x, int y) => y == 0 || !input[..y].Select(p => p[x]).Any(q => q >= input[y][x]);
bool Below(int x, int y) => y == maxRow - 1 || !input[(y + 1)..maxRow].Select(p => p[x]).Any(q => q >= input[y][x]);

int ScenicCount(IEnumerable<int> array, int m) {
    int count = 0;
    foreach (int x in array) {
        ++count;
        if (x >= m) {
            break;
        }
    }
    return count;
}

int LeftScenic(int x, int y) => ScenicCount(input[y][..x].Reverse(), input[y][x]);
int RightScenic(int x, int y) => ScenicCount(input[y][(x + 1)..maxColumn], input[y][x]);
int AboveScenic(int x, int y) => ScenicCount(input[..y].Reverse().Select(p => p[x]), input[y][x]);
int BelowScenic(int x, int y) => ScenicCount(input[(y + 1)..maxRow].Select(p => p[x]), input[y][x]);

int countOfVisible = 0;
int maxScenicScore = 0;

for (int row = 0; row < maxRow; row++) {
    for (int column = 0; column < maxColumn; column++) {

        if (Left(column, row) || Right(column, row) || Above(column, row) || Below(column, row)) {
            ++countOfVisible;
        }

        maxScenicScore = Math.Max(LeftScenic(column, row) *
                                  RightScenic(column, row) *
                                  AboveScenic(column, row) *
                                  BelowScenic(column, row), maxScenicScore);
    }
}

Console.WriteLine($"Puzzle 1 answer : Answer = {countOfVisible}");
Console.WriteLine($"Puzzle 2 answer : Answer = {maxScenicScore}");