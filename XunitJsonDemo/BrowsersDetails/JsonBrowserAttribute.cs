using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace XunitJsonDemo.BrowsersDetails
{
    class JsonBrowserAttribute : DataAttribute
    {
        private readonly string filePath;

        public JsonBrowserAttribute(string filePath)
        {
            this.filePath = filePath;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var dataList = JsonConvert.DeserializeObject<TestDataBrowsersDetails>(File.ReadAllText(filePath));

            var objectList = new List<object[]>();
            foreach (var data in dataList.BrowsersDetails)
            {
                objectList.Add(new object[] { data });

            }
            return objectList;
        }
    }
}
