using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02;
public class Pick
{
    public Pick(int number)
    {
        Red = number;
        Green = number;
        Blue = number;
    }
    public int Red { get; set; }
    public int Green { get; set; }
    public int Blue { get; set; }
}
