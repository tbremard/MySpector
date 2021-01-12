using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace MySpector.Repository
{
    //https://www.youtube.com/watch?v=Et2khGnrIqc&feature=youtu.be
    public class Repo
    {
        public List<string> GetNames()
        {
            IDbConnection connection = new SqlConnection();
            var ret = connection.Query<string>("select NAME from TROX ").ToList();
            return ret;
        }
    }
}
