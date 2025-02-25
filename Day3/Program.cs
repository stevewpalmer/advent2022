int totalPriority = 0;
int totalGroups = 0;

string[] groups = new string[3];
int groupIndex = 0;

char Common2(string first, string second) =>
    first.ToCharArray().Intersect(second.ToCharArray()).First();

char Common3(string first, string second, string third) =>
    first.ToCharArray().Intersect(second.ToCharArray()).Intersect(third.ToCharArray()).First();

int Priority(char ch) => char.IsLower(ch) ? ch - 'a' + 1 : ch - 'A' + 27;

foreach (string line in File.ReadAllLines("puzzle.txt")) {

    string rucksack1 = line.Substring(0, line.Length / 2);
    string rucksack2 = line.Substring(rucksack1.Length);

    totalPriority += Priority(Common2(rucksack1, rucksack2));

    groups[groupIndex++] = line;
    if (groupIndex == 3) {
        totalGroups += Priority(Common3(groups[0], groups[1], groups[2]));
        groupIndex = 0;
    }
}

Console.WriteLine($"Part 1 answer : {totalPriority}");
Console.WriteLine($"Part 2 answer : {totalGroups}");
