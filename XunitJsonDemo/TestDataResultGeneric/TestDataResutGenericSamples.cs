using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XunitJsonDemo.TestDataResultGeneric
{
    public class TestDataResutGenericSamples
    {
        private readonly Calculator _calculator;

        public TestDataResutGenericSamples()
        {
            _calculator = new Calculator();
        }

        [Theory]
        [JsonFileData("TestDataResultGeneric/data.json", "AddData", typeof(List<int>), typeof(int))]
        public void CanAdd(List<int> data, int expectedResult)
        {
            var result = _calculator.Add(data);
            Assert.Equal(expectedResult, result);
        }
    }
}
