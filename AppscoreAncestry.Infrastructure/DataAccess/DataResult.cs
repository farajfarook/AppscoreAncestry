using Newtonsoft.Json;

namespace AppscoreAncestry.Infrastructure.DataAccess
{
    public class DataResult
    {
        public DataResult(string content)
        {
            Content = content;
        }

        public string Content { get; }

        public T GetContent<T>()
        {
            return JsonConvert.DeserializeObject<T>(Content);
        }
    }
}