string input = File.ReadAllText("day6.txt");

int FindMarker(int packetSize) {

    for (int index = packetSize; index < input.Length; index++) {
        string packet = input.Substring(index - packetSize, packetSize);
        if (new HashSet<char>(packet).Count == packetSize) {
            return index;
        }
    }
    return -1;
}

Console.WriteLine($"Puzzle 1 answer : Answer = {FindMarker(4)}");
Console.WriteLine($"Puzzle 2 answer : Answer = {FindMarker(14)}");
