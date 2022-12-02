int totalScore = 0;
int totalStrategyScore = 0;

const int ROCK = 1;
const int PAPER = 2;
const int SCISSORS = 3;

Dictionary<int, int> map = new() {
    { ROCK, PAPER },
    { PAPER, SCISSORS },
    { SCISSORS, ROCK }
};

int Beats(int theirPlay) => map[theirPlay];

int Loses(int theirPlay) => map.ToDictionary(x => x.Value, x => x.Key)[theirPlay];

int CalculatePlay(int theirPlay, int yourPlay) {
    if (yourPlay == theirPlay) {
        return yourPlay + 3;
    }
    if (yourPlay == Beats(theirPlay)) {
        return yourPlay + 6;
    }
    return yourPlay;
}

foreach (string line in File.ReadAllLines("day2.txt")) {

    string[] plays = line.Split(' ');
    int theirPlay = 0;

    switch (plays[0]) {
        case "A": theirPlay = ROCK; break;
        case "B": theirPlay = PAPER; break;
        case "C": theirPlay = SCISSORS; break;
    }
    switch (plays[1]) {
        case "X":
            totalScore += CalculatePlay(theirPlay, ROCK);
            totalStrategyScore += CalculatePlay(theirPlay, Loses(theirPlay));
            break;
        case "Y":
            totalScore += CalculatePlay(theirPlay, PAPER);
            totalStrategyScore += CalculatePlay(theirPlay, theirPlay);
            break;
        case "Z":
            totalScore += CalculatePlay(theirPlay, SCISSORS);
            totalStrategyScore += CalculatePlay(theirPlay, Beats(theirPlay));
            break;
    }
}

Console.WriteLine($"Puzzle 1 answer : Total score = {totalScore}");
Console.WriteLine($"Puzzle 2 answer : Total score = {totalStrategyScore}");
