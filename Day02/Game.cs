using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02;
public class Game
{
    public Game(int gameID, string gameString)
    {
        GameID = gameID;
        List<string> sets = gameString.Split("; ").ToList();
        foreach (string item in sets)
        {
            List<string> pickStrings = item.Split(", ").ToList();
            Pick pick = new Pick(0);
            foreach (string pickString in pickStrings)
            {
                int count = int.Parse(pickString.Split(" ")[0].ToString());
                string Color = pickString.Split(" ")[1].ToString();
                switch (Color)
                {
                    case "red":
                        pick.Red = count;
                        break;
                    case "green":
                        pick.Green = count;
                        break;
                    case "blue":
                        pick.Blue = count;
                        break;
                    default:
                        break;
                }
            }
            Sets.Add(pick);
        }
    }

    public int GameID { get; set; }
    public List<Pick> Sets { get; set; } = new List<Pick>();
}
