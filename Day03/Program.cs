using System.Collections.Specialized;
using System.Numerics;
using System.Runtime.CompilerServices;

List<string> inputList = File.ReadAllLines("./debug.txt").ToList();
int width = inputList[0].Length;
int height = inputList.Count;
char[,] inputMap = new char[width,height];
for(int y = 0; y < inputList.Count; y++){
    for(int x = 0; x < width; x++){
        char c = inputList[y][x];
        inputMap[x,y] = c;
    }
}

for(int y = 0; y < height; y++){
    for(int x = 0; x < width; x++){
        char c = inputMap[x,y];
        //Check if its a symbol, then find adjecent tiles.
        if(!char.IsNumber(c)){
            if(c != '.'){
                Console.WriteLine($"Symbol at position X{x} Y{y} - Symbol is {c}");
                List<Vector2> adjecents = FindAdjecent(new Vector2(x,y), width, height);
                Console.WriteLine($"The position has {adjecents.Count} adjecent numbers. They are:");
                foreach(Vector2 p in adjecents){
                    Console.WriteLine(inputMap[(int)p.X,(int)p.Y].ToString() + "at pos " + p);
                }
                Console.WriteLine();
            }
        }
    }
}

List<Vector2> FindAdjecent(Vector2 pos, int w, int h){
    List<Vector2> returnList = new List<Vector2>();
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
        if(testPos.X > 0 || testPos.X < w || testPos.Y > 0 || testPos.Y < h){
            char testChar = inputMap[(int)testPos.X,(int)testPos.Y];
            if(char.IsDigit(testChar)){
                returnList.Add(testPos);
            }
        }
    }

    return returnList;
}