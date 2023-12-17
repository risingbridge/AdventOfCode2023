using Day02;
using System.Numerics;

List<string> inputList = File.ReadAllLines("./input.txt").ToList();
List<Game> Games = new List<Game>();

foreach (string line in inputList)
{
    //Get GameID
    int GameID = int.Parse(line.Split(":")[0].Replace("Game ", ""));
    string gameString = line.Split(": ")[1];
    Game game = new Game(GameID, gameString);
    Games.Add(game);
}

//Solve part one
int redCheck = 12;
int greenCheck = 13;
int blueCheck = 14;
int partOneSum = 0;

foreach (Game item in Games)
{
    bool gamePossible = true;
    //Console.WriteLine($"Testing Game {item.GameID}:");
    for (int i = 0; i < item.Sets.Count; i++)
    {
        bool setPossible = false;
        if(redCheck >= item.Sets[i].Red && blueCheck >= item.Sets[i].Blue && greenCheck >= item.Sets[i].Green)
        {
            setPossible = true;
        }
        else
        {
            gamePossible = false;
        }
        //Console.WriteLine($"Testing set number {i + 1}: Red: {item.Sets[i].Red}, Green: {item.Sets[i].Green}, Blue: {item.Sets[i].Blue}. Set possible: {setPossible}");
    }
    //Console.WriteLine($"Game Possible: {gamePossible}\n");
    if (gamePossible)
    {
        partOneSum += item.GameID;
    }
}

Console.WriteLine($"Part One: {partOneSum}");


//Solve part two
int partTwoSum = 0;
foreach (Game game in Games)
{
    int minRed = 0;
    int minGreen = 0;
    int minBlue = 0;
    for (int i = 0; i < game.Sets.Count; i++)
    {
        if (game.Sets[i].Red > minRed)
        {
            minRed = game.Sets[i].Red;
        }
        if (game.Sets[i].Green > minGreen)
        {
            minGreen = game.Sets[i].Green;
        }
        if (game.Sets[i].Blue > minBlue)
        {
            minBlue = game.Sets[i].Blue;
        }
    }
    int power = minRed * minGreen * minBlue;
    partTwoSum += power;
    //Console.WriteLine($"Game {game.GameID}; Red: {minRed} Green: {minGreen} Blue: {minBlue} Power: {power}");
}

Console.WriteLine($"Part Two: {partTwoSum}");