using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppscoreAncestry.Infrastructure.Tests.Mocks
{
    internal class MockData
    {
        public static IEnumerable<T> GetContent<T>(string type)
        {
            var contentString = GetContent(type);
            return JsonConvert.DeserializeObject<IEnumerable<T>>(contentString);
        }

        public static string GetContent(string type)
        {
            var content =  File.ReadAllText("./Mocks/data_small.json");
            var jObj = JsonConvert.DeserializeObject<JObject>(content);
            return jObj[type].ToString();
        }
    }
}
