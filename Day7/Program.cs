using System.Collections.Generic;
using System.Linq;

Stack<int> directorySizes = new();
Stack<int> traverse = new();

foreach (string line in File.ReadAllLines("day7.txt")) {

    string[] parts = line.Split(' ');
    if (parts[0] == "$" && parts[1] == "cd") {
        if (parts[2] == "..") {
            int fileSize1 = traverse.Pop();
            directorySizes.Push(fileSize1);
            traverse.Push(traverse.Pop() + fileSize1);
        } else {
            traverse.Push(0);
        }
    } else {
        if (int.TryParse(parts[0], out int fileSize2)) {
            traverse.Push(traverse.Pop() + fileSize2);
        }
    }
}

int totalDiskSize = 0;
while (traverse.Count > 0) {
    totalDiskSize += traverse.Pop();
    directorySizes.Push(totalDiskSize);
}

Console.WriteLine($"Puzzle 1 answer : Answer = {directorySizes.Where(d => d <= 100000).Sum()}");
Console.WriteLine($"Puzzle 2 answer : Answer = {directorySizes.Where(d => d >= totalDiskSize - 40000000).Min()}");
