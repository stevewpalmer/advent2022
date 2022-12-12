string[] input = File.ReadAllText("day10.txt").Split(new char[] { ' ', '\n' });

int cycle = 0;
int xreg = 1;
int signalStrength = 0;

foreach (string inst in input) {

    Console.Write((cycle % 40) >= xreg - 1 && (cycle % 40) <= xreg + 1 ? '#' : '.');

    ++cycle;

    if (((cycle - 20) % 40) == 0) {
        signalStrength += cycle * xreg;
    }

    if ((cycle % 40) == 0) {
        Console.WriteLine();
    }

    switch (inst) {
        case "addx":
        case "noop":
            break;

        case string x when int.TryParse(inst, out int operand):
            xreg += operand;
            break;
    }
}

Console.WriteLine($"Puzzle 1 answer : Answer = {signalStrength}");
Console.WriteLine($"Puzzle 2 answer : Answer = Read the letters above");
