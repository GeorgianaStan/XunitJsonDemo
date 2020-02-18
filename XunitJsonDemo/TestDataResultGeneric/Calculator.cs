using System.Collections.Generic;
using System.Linq;

namespace XunitJsonDemo.TestDataResultGeneric
{
    public class Calculator
    {
        public int Add(List<int> data)
        {
            return data.Sum();
        }
        public int Average(List<int> data)
        {
            return (int)data.Average();
        }
    }
}
