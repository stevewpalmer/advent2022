using System.Text.Json;
using System.Text.Json.Nodes;

int ComparePackets(JsonElement p1, JsonElement p2) {

    switch (p1.ValueKind) {
        case JsonValueKind.Number when p2.ValueKind == JsonValueKind.Number:
            return p1.GetInt32() - p2.GetInt32();
        case JsonValueKind.Array when p2.ValueKind == JsonValueKind.Number:
            return ComparePackets(p1, JsonSerializer.SerializeToElement(new JsonArray(p2.GetInt32())));
        case JsonValueKind.Number when p2.ValueKind == JsonValueKind.Array:
            return ComparePackets(JsonSerializer.SerializeToElement(new JsonArray(p1.GetInt32())), p2);
        case JsonValueKind.Array when p2.ValueKind == JsonValueKind.Array: {
                for (int i = 0; i < p1.GetArrayLength() && i < p2.GetArrayLength(); i++) {

                    int result = ComparePackets(p1[i], p2[i]);
                    if (result == 0) {
                        continue;
                    }
                    return result;
                }
                return p1.GetArrayLength() - p2.GetArrayLength();
            }
        default:
            return 0;
    }
}

int CompareAllPackets() {

    List<(JsonElement, JsonElement)> input = File.ReadAllText("day13.txt")
        .Split("\n\n")
        .Select(pair => pair.Split('\n'))
        .Select(pair => (Left: JsonDocument.Parse(pair[0]).RootElement, Right: JsonDocument.Parse(pair[1]).RootElement))
        .ToList();

    int index = 1;
    int sumIndexes = 0;
    foreach ((JsonElement p1, JsonElement p2) in input) {
        sumIndexes += ComparePackets(p1, p2) < 0 ? index : 0;
        index++;
    }
    return sumIndexes;
}

int OrderPackets() {

    JsonElement divider1 = JsonDocument.Parse("[[2]]").RootElement;
    JsonElement divider2 = JsonDocument.Parse("[[6]]").RootElement;

    List<JsonElement> input = File.ReadAllText("day13.txt")
        .Split("\n")
        .Where(line => !string.IsNullOrEmpty(line))
        .Select(line => JsonDocument.Parse(line).RootElement)
        .Append(divider1)
        .Append(divider2)
        .OrderBy(p => p, Comparer<JsonElement>.Create(ComparePackets))
        .ToList();

    return (input.IndexOf(divider1) + 1) * (input.IndexOf(divider2) + 1);
}

Console.WriteLine($"Puzzle 1 answer : Answer = {CompareAllPackets()}");
Console.WriteLine($"Puzzle 2 answer : Answer = {OrderPackets()}");