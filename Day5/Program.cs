using System.Text.RegularExpressions;

List<Stack<char>> stacks1 = new();
List<Stack<char>> stacks2 = new();

string [] input = File.ReadAllLines("day5.txt");

int endOfMap = Array.FindIndex(input, l => l.Length == 0);

for (int line = endOfMap - 1; line >= 0; line--) {

    int stackNumber = 0;
    for (int p = 0; p < input[line].Length; p += 4) {

        char ch = input[line][p + 1];
        if (ch != ' ') {
            if (stackNumber >= stacks1.Count) {
                stacks1.Add(new Stack<char>());
                stacks2.Add(new Stack<char>());
            }
            stacks1[stackNumber].Push(ch);
            stacks2[stackNumber].Push(ch);
        }
        ++stackNumber;
    }
}

for (int line = endOfMap + 1; line < input.Length; line++) {

    Match matches = Regex.Match(input[line], @"move (\d+) from (\d+) to (\d+)");
    if (matches.Success) {

        int count = Convert.ToInt32(matches.Groups[1].Value);
        int source = Convert.ToInt32(matches.Groups[2].Value) - 1;
        int dest = Convert.ToInt32(matches.Groups[3].Value) - 1;

        for (int c = 0; c < count; c++) {
            stacks1[dest].Push(stacks1[source].Pop());
        }

        List<char> poppedStack = new();
        for (int c = 0; c < count; c++) {
            poppedStack.Add(stacks2[source].Pop());
        }
        for (int c = count - 1; c >= 0; c--) {
            stacks2[dest].Push(poppedStack[c]);
        }
    }
}

string topCrates1 = string.Join("", stacks1.Select(s => s.Peek()).ToArray());
string topCrates2 = string.Join("", stacks2.Select(s => s.Peek()).ToArray());

Console.WriteLine($"Puzzle 1 answer : Answer = {topCrates1}");
Console.WriteLine($"Puzzle 2 answer : Answer = {topCrates2}");
