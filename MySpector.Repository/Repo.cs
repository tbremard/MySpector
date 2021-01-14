using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using NLog;

namespace MySpector.Repo
{
    //https://www.youtube.com/watch?v=Et2khGnrIqc&feature=youtu.be
    public class Repo
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        IDbConnection _connection;

        public bool Connect()
        {
            bool ret = false;
            try
            {
                string connectionString = "Server=localhost;Database=MYSPECTOR;Uid=root; Pwd=123456789;";
                _log.Debug($"Connecting to: {connectionString}");
                //IDbConnection connection = new SqlConnection(connectionString);
                _connection = new MySqlConnection(connectionString);
                ret = true;
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
            return ret;
        }

        public bool Disconnect()
        {
            _connection.Close();
            _connection.Dispose();
            return true;
        }

        public List<string> GetNames()
        {
            var ret = _connection.Query<string>("select NAME from TROX ").ToList();
            return ret;
        }
    }
}
