int elfCalories = 0;
List<int> calories = new();

foreach (string line in File.ReadAllLines("day1.txt")) {
    if (string.IsNullOrEmpty(line)) {
        calories.Add(elfCalories);
        elfCalories = 0;
    } else {
        elfCalories += Convert.ToInt32(line);
    }
}
calories.Add(elfCalories);

Console.WriteLine($"Puzzle 1 answer : Total calories = {calories.MaxBy(e => e)}");
Console.WriteLine($"Puzzle 2 answer : Sum of top 3 calories = {calories.OrderByDescending(e => e).Take(3).Sum()}");
