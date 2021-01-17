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
            List<Objects.Trox> ret;
            try
            {
                string query = @"select * FROM TROX 
                            inner join web_target web on trox.ID_TROX = web.ID_TROX
                            inner join WEB_TARGET_TYPE web_type on web_type.ID_WEB_TARGET_TYPE = web.ID_WEB_TARGET_TYPE
                            inner join web_target_http http on http.ID_WEB_TARGET = web.ID_WEB_TARGET; ";
                ret = _connection.Query<DbModel.Trox, DbModel.WebTarget, DbModel.WebTargetType, DbModel.WebTargetHttp, Objects.Trox>(query, mapperTrox, splitOn: "ID_WEB_TARGET,ID_WEB_TARGET_TYPE,ID_WEB_TARGET").ToList();
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = new List<Objects.Trox>();
            }
            return ret;
        }

        public IList<Objects.Xtrax> GetAllXtrax(int troxId)
        {
            List<Objects.Xtrax> ret;
            try
            {
                string query = @"select * from xtrax_def def 
	                        INNER JOIN xtrax_type typ on def.ID_XTRAX_TYPE = typ.ID_XTRAX_TYPE
                            WHERE def.ID_TROX = @ID_TROX;";
                object param = new { ID_TROX = 4 };
                ret = _connection.Query<DbModel.XtraxDef, DbModel.XtraxType, Objects.Xtrax>(query, mapperXtrax, param: param, splitOn: "ID_XTRAX_TYPE").ToList();
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = new List<Objects.Xtrax>();
            }
            return ret;
        }

        public IList<Objects.IChecker> GetAllChecker(int troxId)
        {
            List<Objects.IChecker> ret;
            try
            {
               // string query = @"select def.ID_TROX, def.ORDER, def.ARG, typ.ID_CHECKER_TYPE, typ.NAME 
                string query = @"select * 
                                    from checker_def def 
                                	INNER JOIN checker_type typ on def.ID_CHECKER_TYPE = typ.ID_CHECKER_TYPE    
                                    WHERE def.ID_TROX = @ID_TROX;";
                object param = new { ID_TROX = 4 };
                ret = _connection.Query<DbModel.CheckerDef, DbModel.CheckerType, Objects.IChecker>(query, mapperChecker, param: param, splitOn: "ID_CHECKER_TYPE").ToList();
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = new List<Objects.IChecker>();
            }
            return ret;
        }

        private IChecker mapperChecker(DbModel.CheckerDef myDef, DbModel.CheckerType myType)
        {
            var xType = (Objects.CheckerType)Enum.Parse(typeof(Objects.CheckerType), myType.Name, true);
            var def = new CheckerParam( xType, myDef.Arg);
            var ret = CheckerFactory.Create(def);
            return ret;
        }

        private Objects.Xtrax mapperXtrax(DbModel.XtraxDef myDef, DbModel.XtraxType myType)
        {
            var xType = (Objects.XtraxType) Enum.Parse(typeof(Objects.XtraxType), myType.Name, true);
            var def = new XtraxDefinition(DbInt(myDef.Order), xType, myDef.Arg);
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
