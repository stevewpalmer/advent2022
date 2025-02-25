string input = File.ReadAllText("puzzle.txt");

int FindMarker(int packetSize) {

    for (int index = packetSize; index < input.Length; index++) {
        string packet = input.Substring(index - packetSize, packetSize);
        if (new HashSet<char>(packet).Count == packetSize) {
            return index;
        }
    }
    return -1;
}

Console.WriteLine($"Part 1 answer : {FindMarker(4)}");
Console.WriteLine($"Part 2 answer : {FindMarker(14)}");
