using Day04;
using Microsoft.Win32.SafeHandles;

List<string> inputList = File.ReadAllLines("./input.txt").ToList();
//Turn the strings into something usefull
List<Card> cards = new List<Card>();
foreach(string line in inputList){
    int cardID = int.Parse(line.Split(": ")[0].Replace("Card ", ""));
    string workingString = line.Split(": ")[1];
    string cardString = workingString.Split(" | ")[0].Trim().Replace("  ", " ");
    string winningString = workingString.Split(" | ")[1].Trim().Replace("  ", " ");
    List<int> cardNumbers = new List<int>();
    List<int> winningNumbers = new List<int>();
    foreach(string s in cardString.Split(" ").ToList()){
        int number = int.Parse(s);
        cardNumbers.Add(number);
    }
    foreach(string s in winningString.Split(" ").ToList()){
        int number = int.Parse(s);
        winningNumbers.Add(number);
    }
    cards.Add(new Card(cardID, cardNumbers, winningNumbers));
}

int PartOneSum = 0;
int numberOfWinningCards = 0;
for(int i = 0; i < cards.Count; i++){
    int cardScore = FindCardScore(cards[i], i);
    if(cards[i].IsWinningCard){
        numberOfWinningCards++;
    }
    PartOneSum += cardScore;
}

Console.WriteLine($"Part One: {PartOneSum}");

//////Solving Part Two
Dictionary<int, List<Card>> CardDictionary = new Dictionary<int, List<Card>>();
//Propogate the directory with the first cards
foreach(Card card in cards){
    List<Card> cardDictList = [card];
    CardDictionary.Add(card.CardID, cardDictList);
}

//Run through the winning cards
int currentCardID = 0; //Card ID is this + 1
foreach(KeyValuePair<int, List<Card>> CardList in CardDictionary){
    // Console.WriteLine($"Checking Card {CardList.Key}:");
    foreach(Card c in CardList.Value){
        if(c.IsWinningCard){
            for(int i = 0; i < c.NumberOfMatchingNumbers; i++){
                Card cardToWin = cards[currentCardID + i + 1];
                CardDictionary[cardToWin.CardID].Add(cardToWin);
                // Console.WriteLine($"You won ID: {cardToWin.CardID}");
            }
        }
    }
    currentCardID++;
}

//Sum the cards
int PartTwoSum = 0;
foreach(KeyValuePair<int, List<Card>> CardList in CardDictionary){
    foreach(Card c in CardList.Value){
        PartTwoSum++;
    }
}
Console.WriteLine($"Part Two: {PartTwoSum}");

int FindCardScore(Card card, int cardListID){
    int sum = 0;
    int winningNumbers = 0;
    foreach(int cardNumber in card.CardNumbers){
        if(card.WinningNumbers.Contains(cardNumber)){
            winningNumbers++;
        }
    }
    if(winningNumbers > 0){
        cards[cardListID].IsWinningCard = true;
        cards[cardListID].NumberOfMatchingNumbers = winningNumbers;
        sum = 1;
        for(int i = 1; i < winningNumbers; i++){
            sum *= 2;
        }
    }
    return sum;
}