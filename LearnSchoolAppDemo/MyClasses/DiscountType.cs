using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnSchoolAppDemo.MyClasses
{
    public class DiscountType
    {
        public DiscountType(string name, int min, int max)
        {
            Name = name;
            Min = min * 0.01;
            Max = max * 0.01;
        }

        public string Name { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
    }
}
