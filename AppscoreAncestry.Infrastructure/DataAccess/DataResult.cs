using System;
using AppscoreAncestry.Infrastructure.Exceptions;
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
            try
            {
                return JsonConvert.DeserializeObject<T>(Content);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Not a valid passed type", e);
            }
        }
    }
}