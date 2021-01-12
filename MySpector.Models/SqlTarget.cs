using MySpector.Models;

namespace MySpector.Core
{
    public class SqlTarget : IWebTarget
    {
        public string ConnectionString { get; }
        public string SqlQuery { get; }
        public WebTargetType WebTargetType => WebTargetType.SQL;
    }
}