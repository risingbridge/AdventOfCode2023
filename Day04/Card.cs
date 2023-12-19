namespace Day04;

public class Card{
    public Card(int id, List<int> cardNumbers, List<int> winningNumbers)
    {
        CardID = id;
        CardNumbers = cardNumbers;
        WinningNumbers = winningNumbers;
    }
    public int CardID { get; set; }
    public List<int> CardNumbers { get; set; } = [];
    public List<int> WinningNumbers { get; set; } = [];
    public bool IsWinningCard { get; set; } = false;
    public int NumberOfMatchingNumbers { get; set; } = 0;
}