﻿int containCount = 0;
int overlapCount = 0;

foreach (string row in File.ReadAllLines("puzzle.txt")) {

    string[] pairs = row.Split(',');
    int[] range1 = pairs[0].Split('-').Select(e => Convert.ToInt32(e)).ToArray();
    int[] range2 = pairs[1].Split('-').Select(e => Convert.ToInt32(e)).ToArray();

    if (range1[0] >= range2[0] && range1[1] <= range2[1] ||
        range2[0] >= range1[0] && range2[1] <= range1[1]) {
        ++containCount;
    }

    if (range1[0] <= range2[0] && range1[1] >= range2[0] ||
        range2[0] <= range1[0] && range2[1] >= range1[0]) {
        ++overlapCount;
    }
}

Console.WriteLine($"Part 1 answer : {containCount}");
Console.WriteLine($"Part 2 answer : {overlapCount}");
