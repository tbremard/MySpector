namespace MySpector.Objects
{
    public class SqlTarget : IWebTarget
    {
        public string ConnectionString { get; }
        public string SqlQuery { get; }
        public WebTargetType WebTargetType => WebTargetType.SQL;

        public SqlTarget(string connectionString, string sqlQuery)
        {
            ConnectionString = connectionString;
            SqlQuery = sqlQuery;
        }
    }
}