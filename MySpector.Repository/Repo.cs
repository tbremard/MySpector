using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using NLog;
using MySpector.Objects;
using MySpector.Repo.DbModel;

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

        public IList<Objects.Trox> GetAllTroxes()
        {
            string query = @"select * FROM TROX 
                            inner join web_target web on trox.ID_TROX = web.ID_TROX
                            inner join WEB_TARGET_TYPE web_type on web_type.ID_WEB_TARGET_TYPE = web.ID_WEB_TARGET_TYPE
                            inner join web_target_http http on http.ID_WEB_TARGET = web.ID_WEB_TARGET; ";
            var ret = _connection.Query<DbModel.Trox, DbModel.WebTarget, DbModel.WebTargetType, DbModel.WebTargetHttp, Objects.Trox>(query, mapperTrox, splitOn: "ID_WEB_TARGET,ID_WEB_TARGET_TYPE,ID_WEB_TARGET").ToList();
            return ret;
        }

        public IList<Objects.Xtrax> GetAllXtrax(int troxId)
        {
            string query = @"select * from xtrax_def def 
	                        INNER JOIN xtrax_type typ on def.ID_XTRAX_TYPE = typ.ID_XTRAX_TYPE
                            WHERE def.ID_TROX = 4;";//@ID_TROX
            var ret = _connection.Query<DbModel.XtraxDef, DbModel.XtraxType, Objects.Xtrax>(query, mapperXtrax, splitOn: "ID_XTRAX_TYPE").ToList();
            return ret;
        }

        private Objects.Xtrax mapperXtrax(XtraxDef xtraxDef, DbModel.XtraxType xtraxType)
        {
            var xType = (Objects.XtraxType) Enum.Parse(typeof(Objects.XtraxType), xtraxType.Name, true);
            var def = new XtraxDefinition(DbInt(xtraxDef.Order), xType, xtraxDef.Arg);
            var ret = XtraxFactory.Create(def);
            return ret;
        }

        private Objects.Trox mapperTrox(DbModel.Trox trox, DbModel.WebTarget webTarget, DbModel.WebTargetType webTargetType, DbModel.WebTargetHttp webTargetHttp)
        {
            var ret = new Objects.Trox(trox.Name, null, DbBool(trox.Enabled), null, null, null);
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


        private bool DbBool(byte? b)
        {
            if (!b.HasValue)
                return false;
            bool ret = b != 0;
            return ret;
        }

        private int DbInt(int? b)
        {
            if (!b.HasValue)
                return 0;
            int ret = b.Value;
            return ret;
        }

    }
}
