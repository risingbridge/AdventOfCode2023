using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01;
public class numberSearch
{
    public numberSearch(string textNumber, int value, int length)
    {
        TextNumber = textNumber;
        Value = value;
        Length = length;
    }

    public string TextNumber { get; set; }
    public int Value { get; set; }
    public int Length { get; set; }
}
