using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit.Sdk;

namespace XunitJsonDemo.TestDataResultGeneric
{
    public class JsonFileDataAttribute : DataAttribute
    {
        private readonly string filePath;
        private readonly string propertyName;
        private readonly Type dataType;
        private readonly Type resultType;
    

        public JsonFileDataAttribute(string filePath, Type dataType, Type resultType)
        {
            this.filePath = filePath;
            this.dataType = dataType;
            this.resultType = resultType;
        }

        public JsonFileDataAttribute(string filePath, string propertyName, Type dataType, Type resultType)
        {
            this.filePath = filePath;
            this.propertyName = propertyName;
            this.dataType = dataType;
            this.resultType = resultType;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            if (testMethod == null) throw new ArgumentNullException(nameof(testMethod));

            var parameters = testMethod.GetParameters();

            var path = Path.IsPathRooted(filePath) ? filePath : Path.GetRelativePath(Directory.GetCurrentDirectory(), filePath);

            if (!File.Exists(path)) throw new ArgumentException($"Could not find file at path: {path}");

            var fileData = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(propertyName))
            {
                return GetData(fileData);
            }

            var allData = JObject.Parse(fileData);
            var data = allData[propertyName].ToString();
            return GetData(data);

        }

        private IEnumerable<object[]> GetData(string jsonData)
        {
            var specific = typeof(TestObject<,>).MakeGenericType(dataType, resultType);
            var generic = typeof(List<>).MakeGenericType(specific);

            dynamic dataList = JsonConvert.DeserializeObject(jsonData, generic);
            var objectList = new List<object[]>();
            foreach (var data in dataList)
            {
                objectList.Add(new object[] { data.Data, data.Result });
            }
            return objectList;
        }
    }
}
