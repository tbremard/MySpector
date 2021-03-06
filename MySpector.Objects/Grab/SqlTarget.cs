﻿namespace MySpector.Objects
{
    public class SqlTarget : IGrabTarget
    {
        public TargetType TargetType => TargetType.SQL;
        public string Name { get; set; }
        public int TimeoutMs { get; set; }
        public string ConnectionString { get; }
        public string SqlQuery { get; }
        public string Provider { get; }

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