using System.Collections.Specialized;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Day03;

List<string> inputList = File.ReadAllLines("./input.txt").ToList();
int width = inputList[0].Length;
int height = inputList.Count;
char[,] inputMap = new char[width,height];
for(int y = 0; y < inputList.Count; y++){
    for(int x = 0; x < width; x++){
        char c = inputList[y][x];
        inputMap[x,y] = c;
    }
}

List<PartNumber> numbers = new List<PartNumber>();
List<Symbol> symbols = new List<Symbol>();

for (int y = 0; y < width; y++){
    for(int x = 0; x < height; x++){
        // Console.Write(inputMap[x,y]);
        if(char.IsNumber(inputMap[x,y])){
            //Combine the numbers and add here
            PartNumber number = FindWholeNumber(new Vector2(x,y));
            // Console.WriteLine($"Found number {number.Number}");
            if(!CheckIfNumberAlreadyFound(number, numbers)){
                numbers.Add(number);
            }
        }else if(inputMap[x,y] != '.'){
            //Adds a symbol to the symbol list
            symbols.Add(new Symbol(new Vector2(x,y), inputMap[x,y]));
        }

    }
    // Console.WriteLine();
}

//Find out which numbers that touches the symbols
List<PartNumber> numbersToSum = new List<PartNumber>();
foreach(PartNumber n in numbers){
    foreach(Vector2 pos in n.Positions){
        if(IsAdjecentToSymbol(pos)){
            numbersToSum.Add(n);
            break;
        }
    }
}
//Sums the numbers and returns part one
int PartOneSum = 0;
foreach(PartNumber n in numbersToSum){
    PartOneSum += n.Number;
}
Console.WriteLine($"Part One: {PartOneSum}");

//Solve part two
List<Vector2> cogPositions = FindCogPositions(symbols);
// Console.WriteLine($"Found {cogPositions.Count} cogs");
List<PartNumber> CogFactors = new List<PartNumber>();
int PartTwoSum = 0;
foreach(Vector2 pos in cogPositions){
    List<PartNumber> L = FindCogFactors(pos);
    foreach(PartNumber p in L){
        CogFactors.Add(p);
    }
    if(L.Count == 2){
        int power = L[0].Number * L[1].Number;
        PartTwoSum += power;
    }
}
Console.WriteLine($"Part Two: {PartTwoSum}");



//Helper functions
List<PartNumber> FindCogFactors(Vector2 pos){
    List<PartNumber> partNumbers = new List<PartNumber>();

    List<Vector2> testPositions =
    [
        new Vector2(pos.X - 1, pos.Y - 1),
        new Vector2(pos.X, pos.Y - 1),
        new Vector2(pos.X + 1, pos.Y - 1),
        new Vector2(pos.X - 1, pos.Y),
        new Vector2(pos.X + 1, pos.Y),
        new Vector2(pos.X - 1, pos.Y + 1),
        new Vector2(pos.X, pos.Y + 1),
        new Vector2(pos.X + 1, pos.Y + 1),
    ];
    List<Vector2> adjecentPositions = new List<Vector2>();
    foreach(Vector2 testPos in testPositions){
        if(testPos.X >= 0 && testPos.X < width && testPos.Y >= 0 && testPos.Y < height){
            if(char.IsNumber(inputMap[(int)testPos.X,(int)testPos.Y])){
                adjecentPositions.Add(testPos);
            }
        }
    }
    //Find the part numbers for the positions

    foreach(Vector2 p in adjecentPositions){
        PartNumber adjNumber = FindPartNumberFromPos(p);
        if(!CheckIfNumberAlreadyFound(adjNumber, partNumbers)){
            partNumbers.Add(adjNumber);
        }
    }
    return partNumbers;
}

List<Vector2> FindCogPositions(List<Symbol> symbolList){
    List<Vector2> positions = new List<Vector2>();
    foreach(Symbol s in symbolList){
        if(s.Character == '*'){
            positions.Add(s.Position);
        }
    }
    return positions;
}

PartNumber FindPartNumberFromPos(Vector2 pos){
    foreach(PartNumber n in numbersToSum){
        foreach(Vector2 p in n.Positions){
            if(p == pos){
                return n;
            }
        }
    }

    return null;
}

bool CheckIfNumberAlreadyFound(PartNumber check, List<PartNumber> partNumbers){
    foreach(PartNumber n in partNumbers){
        if(ComparePartNumbers(check, n)){
            return true;
        }
    }
    return false;
}

bool ComparePartNumbers(PartNumber a, PartNumber b){
    if(a.Number != b.Number){
        return false;
    }

    bool sameCoordinates = true;
    if(a.Positions.Count != b.Positions.Count){
        sameCoordinates = false;
    }else{
        for(int i = 0; i < a.Positions.Count; i++){
            Vector2 vectorA = new Vector2(a.Positions[i].X,a.Positions[i].Y);
            Vector2 vectorB = new Vector2(b.Positions[i].X,b.Positions[i].Y);
            if(vectorA != vectorB){
                sameCoordinates = false;
            }
        }
    }
    if(!sameCoordinates){
        return false;
    }

    return true;
}

PartNumber FindWholeNumber(Vector2 pos){
    //Find the first digit in the number, stop if not number or edge
    string wholeNumberString = string.Empty;
    List<Vector2> numberPositions = new List<Vector2>();
    Vector2 startPos = new Vector2(pos.X,pos.Y);
    Vector2 endPos = new Vector2(pos.X,pos.Y);
    Vector2 checkPos = new Vector2(pos.X,pos.Y);
    while(checkPos.X >= 0){
        char checkChar = inputMap[(int)checkPos.X, (int)checkPos.Y];
        if(!char.IsDigit(checkChar)){
            startPos = new Vector2(checkPos.X +1, checkPos.Y);
            break;
        }
        startPos = new Vector2(0, checkPos.Y);
        checkPos.X--;
    }
    //Finds all the numbers from the start, to the end
    wholeNumberString += inputMap[(int)startPos.X,(int)startPos.Y].ToString();
    numberPositions.Add(startPos);
    bool foundFullNumber = false;
    checkPos = new Vector2(startPos.X,startPos.Y);
    while(!foundFullNumber){
        checkPos.X++;
        if(checkPos.X >= width){
            break;
        }
        if(char.IsNumber(inputMap[(int)checkPos.X,(int)checkPos.Y])){
            numberPositions.Add(checkPos);
            wholeNumberString += inputMap[(int)checkPos.X,(int)checkPos.Y].ToString();
        }else{
            foundFullNumber = true;
            break;
        }
    }
    PartNumber returnNumber = new PartNumber(int.Parse(wholeNumberString), numberPositions);

    return returnNumber;
}

bool IsAdjecentToSymbol(Vector2 pos){
    List<Vector2> testPositions = new List<Vector2>();
    //topLeft
    testPositions.Add(new Vector2(pos.X - 1, pos.Y - 1));
    //top
    testPositions.Add(new Vector2(pos.X, pos.Y - 1));
    //topRight
    testPositions.Add(new Vector2(pos.X + 1, pos.Y - 1));
    //left
    testPositions.Add(new Vector2(pos.X - 1, pos.Y));
    //right
    testPositions.Add(new Vector2(pos.X + 1, pos.Y));
    //bottomRight
    testPositions.Add(new Vector2(pos.X - 1, pos.Y + 1));
    //bottom
    testPositions.Add(new Vector2(pos.X, pos.Y + 1));
    //bottomRight
    testPositions.Add(new Vector2(pos.X + 1, pos.Y + 1));
    foreach(Vector2 testPos in testPositions){
        if(testPos.X > 0 && testPos.X < width && testPos.Y > 0 && testPos.Y < height){
            char testChar = inputMap[(int)testPos.X,(int)testPos.Y];
            if(!char.IsDigit(testChar) && testChar != '.'){
                return true;
            }
        }
    }


    return false;
}



// List<Vector2> FindAdjecent(Vector2 pos, int w, int h){
//     List<Vector2> returnList = new List<Vector2>();
//     List<Vector2> testPositions = new List<Vector2>();
//     //topLeft
//     testPositions.Add(new Vector2(pos.X - 1, pos.Y - 1));
//     //top
//     testPositions.Add(new Vector2(pos.X, pos.Y - 1));
//     //topRight
//     testPositions.Add(new Vector2(pos.X + 1, pos.Y - 1));
//     //left
//     testPositions.Add(new Vector2(pos.X - 1, pos.Y));
//     //right
//     testPositions.Add(new Vector2(pos.X + 1, pos.Y));
//     //bottomRight
//     testPositions.Add(new Vector2(pos.X - 1, pos.Y + 1));
//     //bottom
//     testPositions.Add(new Vector2(pos.X, pos.Y + 1));
//     //bottomRight
//     testPositions.Add(new Vector2(pos.X + 1, pos.Y + 1));
    
//     foreach(Vector2 testPos in testPositions){
//         if(testPos.X > 0 || testPos.X < w || testPos.Y > 0 || testPos.Y < h){
//             char testChar = inputMap[(int)testPos.X,(int)testPos.Y];
//             if(char.IsDigit(testChar)){
//                 returnList.Add(testPos);
//             }
//         }
//     }

//     return returnList;
// }