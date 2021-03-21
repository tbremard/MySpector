namespace MySpector.Objects
{
    public class SqlTarget : IGrabTarget
    {
        public string ConnectionString { get; }
        public string SqlQuery { get; }
        public string Provider { get; }
        public TargetType TargetType => TargetType.SQL;

        public SqlTarget(string connectionString, string sqlQuery, string provider)
        {
            ConnectionString = connectionString;
            SqlQuery = sqlQuery;
            Provider = provider;
        }

        public override string ToString()
        {
            return "SqlTarget->" + Provider + "/"+ SqlQuery;
        }
    }
}