using Day01;

List<string> inputList = File.ReadAllLines("./input.txt").ToList();

//Part 1
int sum = 0;
foreach (string line in inputList)
{
    string numberString = new string(line.Where(c => char.IsDigit(c)).ToArray());
    string calibrationNumber = numberString[0].ToString() + numberString[numberString.Length - 1].ToString();
    sum += int.Parse(calibrationNumber);
}

Console.WriteLine($"Part one: {sum}");


//Part 2
sum = 0; //Resets the sum

List<numberSearch> searchList = new List<numberSearch>();
searchList.Add(new numberSearch("one", 1, 3));
searchList.Add(new numberSearch("two", 2, 3));
searchList.Add(new numberSearch("three", 3, 5));
searchList.Add(new numberSearch("four", 4, 4));
searchList.Add(new numberSearch("five", 5, 4));
searchList.Add(new numberSearch("six", 6, 3));
searchList.Add(new numberSearch("seven", 7, 5));
searchList.Add(new numberSearch("eight", 8, 5));
searchList.Add(new numberSearch("nine", 9, 4));

foreach (string line in inputList)
{
    string numberString = string.Empty;
    for (int i = 0; i < line.Length; i++)
    {
        if (int.TryParse(line[i].ToString(), out int result)){
            numberString += result.ToString();
        }
        else
        {
            foreach (numberSearch value in searchList)
            {
                string searchWord = value.TextNumber;
                int wordLength = value.Length;
                if(line.Length - i >= wordLength)
                {
                    string searchString = line.Substring(i, wordLength);
                    if(searchWord == searchString)
                    {
                        numberString += value.Value.ToString();
                    }
                }
            }
        }
    }
    string tempNumber = numberString[0].ToString() + numberString[numberString.Length - 1].ToString();
    sum += int.Parse(tempNumber);
}


Console.WriteLine($"Part two: {sum}");
