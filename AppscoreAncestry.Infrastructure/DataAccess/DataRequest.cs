namespace AppscoreAncestry.Infrastructure.DataAccess
{
    public class DataRequest
    {
        public string DataSetName { get; }

        public DataRequest(string dataSetName)
        {
            DataSetName = dataSetName;
        }
    }
}