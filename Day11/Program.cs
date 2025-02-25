using System.Text.RegularExpressions;

namespace Day11;

public class Monkey {
    public int divisible;
    public int falseMonkey;
    public long inspected;
    public string[] operation = [];
    public Queue<long> startingItems = new();
    public int trueMonkey;
}

internal static class Day11 {
    private static bool MatchNumber(string r, string s, out int m) {
        m = 0;
        if (s.Contains(r)) {
            m = Convert.ToInt32(new Regex(@"\d+").Matches(s).Select(x => x.Value).First());
            return true;
        }
        return false;
    }

    private static bool MatchText(string r, string s, string p, out string[] m) {
        m = !s.Contains(r) ? [] : new Regex(p).Matches(s)[0].Groups.Values.Skip(1).Select(x => x.ToString()).ToArray();
        return m.Length > 0;
    }

    private static bool MatchString(string r, string s, string p, out long[] m) {
        m = !s.Contains(r) ? [] : new Regex(p).Matches(s).Select(x => long.Parse(x.Value)).ToArray();
        return m.Length > 0;
    }

    private static long Calculate(int totalRounds) {

        Dictionary<int, Monkey> monkies = new();
        Monkey monkey = new();

        Array.ForEach(File.ReadAllLines("puzzle.txt"), line => {

            switch (line) {
                case { } when MatchNumber("Monkey", line, out int _number):
                    monkey = new Monkey();
                    monkies[_number] = monkey;
                    break;

                case { } when MatchString("Starting items", line, @"\d+", out long[] _startingItems):
                    monkey.startingItems = new Queue<long>(_startingItems);
                    break;

                case { } when MatchText("Operation", line, @"old (\*|\+) (old|\d+)", out string[] _operation):
                    monkey.operation = _operation;
                    break;

                case { } when MatchNumber("Test: divisible by", line, out int _divisible):
                    monkey.divisible = _divisible;
                    break;

                case { } when MatchNumber("If true", line, out int _trueMonkey):
                    monkey.trueMonkey = _trueMonkey;
                    break;

                case { } when MatchNumber("If false", line, out int _falseMonkey):
                    monkey.falseMonkey = _falseMonkey;
                    break;
            }
        });

        for (int round = 1; round <= totalRounds; round++) {

            foreach (int key in monkies.Keys) {

                monkey = monkies[key];
                while (monkey.startingItems.TryDequeue(out long worryLevel)) {

                    ++monkey.inspected;

                    long operand = monkey.operation[1] == "old" ? worryLevel : long.Parse(monkey.operation[1]);
                    if (monkey.operation[0] == "*") {
                        worryLevel *= operand;
                    }
                    else if (monkey.operation[0] == "+") {
                        worryLevel += operand;
                    }

                    if (totalRounds == 20) {
                        worryLevel /= 3;
                    }
                    else {
                        worryLevel %= 9699690;
                    }

                    int targetMonkey = worryLevel % monkey.divisible == 0 ? monkey.trueMonkey : monkey.falseMonkey;
                    monkies[targetMonkey].startingItems.Enqueue(worryLevel);
                }
            }
        }

        Monkey[] topItems = monkies.Values.OrderByDescending(m => m.inspected).Take(2).ToArray();
        return topItems[0].inspected * topItems[1].inspected;
    }

    public static void Main() {

        Console.WriteLine($"Part 1 answer : {Calculate(20)}");
        Console.WriteLine($"Part 2 answer : {Calculate(10000)}");
    }
}