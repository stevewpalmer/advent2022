int elfCalories = 0;
List<int> calories = new();

foreach (string line in File.ReadAllLines("puzzle.txt")) {
    if (string.IsNullOrEmpty(line)) {
        calories.Add(elfCalories);
        elfCalories = 0;
    } else {
        elfCalories += Convert.ToInt32(line);
    }
}
calories.Add(elfCalories);

Console.WriteLine($"Part 1 answer : {calories.MaxBy(e => e)}");
Console.WriteLine($"Part 2 answer : {calories.OrderByDescending(e => e).Take(3).Sum()}");
