﻿using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System;
using NLog;
using MySpector.Objects;
using MySpector.Repo.DbModel;
using System.Net.Http.Headers;
using System.Net.Http;

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
                _connection.Open();
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

        public IList<Objects.Trox> GetAllTroxes(List<int> troxIds)
        {
            _log.Debug("GetAllTroxes()");
            IList<Objects.Trox> ret;
            try
            {
                object param = new { IDS = troxIds };
                string query = @"select * FROM TROX WHERE ID_TROX IN @IDS";
                var troxes = _connection.Query<DbModel.trox>(query, param).ToList();
                ret = CreateTroxesFromDb(troxes);
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = new List<Objects.Trox>();
            }
            return ret;
        }

        public IList<Objects.Trox> GetAllTroxes()
        {
            _log.Debug("GetAllTroxes()");
            IList<Objects.Trox> ret;
            try
            {
                string query = @"select * FROM TROX";
                var troxes = _connection.Query<DbModel.trox>(query).ToList();
                ret = CreateTroxesFromDb(troxes);
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = new List<Objects.Trox>();
            }
            return ret;
        }

        private IList<Objects.Trox> CreateTroxesFromDb(List<trox> troxes)
        {
            var ret = new List<Objects.Trox>();
            foreach (var x in troxes)
            {
                if (DbToBool(x.IS_DIRECTORY))
                    continue;
                var target = GetWebTarget(x.ID_WEB_TARGET);
                var xtrax = GetAllXtrax(x.ID_TROX);
                var check = GetAllChecker(x.ID_TROX);
                var notifier = GetAllNotifier(x.ID_TROX);
                var trox = new Trox(x.NAME, DbToBool(x.ENABLED), target, XtraxFactory.CreateChain(xtrax), check.FirstOrDefault(), notifier.FirstOrDefault());
                ret.Add(trox);
                _log.Debug("Loaded Trox: " + trox.ToString());
            }
            return ret;
        }

        public int? SaveTrox(Trox trox)
        {
            if (_currentTransaction == null)
            {
                _log.Error("You must first create a transaction");
                return 0;
            }
            int? targetDbId = SaveWebTarget(trox.Target);
            int? troxDbId = SaveTroxCore(trox);
            AttachWebTargetToTrox(targetDbId, troxDbId);
            return troxDbId;
        }

        private bool AttachWebTargetToTrox(int? targetDbId, int? troxDbId)
        {
            int nbRows;
            string query = $"UPDATE TROX SET ID_WEB_TARGET={targetDbId} WHERE ID_TROX={troxDbId};";
            if (_currentTransaction == null)
            {
                _log.Error("You must first create a transaction");
                return false;
            }
            try
            {
                nbRows = _currentTransaction.Connection.Execute(query);
            }
            catch (Exception e)
            {
                _log.Error(e);
                return false;
            }
            if (nbRows == 0)
                return false;
            return true;
        }

        private int? SaveTroxCore(Trox trox)
        {
            int? troxDbId;
            if (_currentTransaction == null)
            {
                _log.Error("You must first create a transaction");
                return 0;
            }
            var dbTrox = new trox() { ENABLED = BoolToDb(trox.Enabled), NAME = trox.Name, IS_DIRECTORY = BoolToDb(false) };
            string q = @"INSERT INTO TROX(NAME, ENABLED, IS_DIRECTORY) values(@NAME, @ENABLED, @IS_DIRECTORY);
                         SELECT LAST_INSERT_ID();";
            try
            {
                troxDbId = _currentTransaction.Connection.Query<int>(q, dbTrox).Single();
            }
            catch (Exception e)
            {
                _log.Error(e);
                troxDbId = null;
            }
            return troxDbId;
        }

        public int SaveWebTarget(IWebTarget target)
        {
            if (_currentTransaction == null)
            {
                _log.Error("You must first create a transaction");
                return 0;
            }
            int ret = 0;
            switch (target.WebTargetType)
            {
                case WebTargetType.HTTP:
                    string q = @"INSERT INTO WEB_TARGET(ID_WEB_TARGET_TYPE) values(1);
                                 SELECT LAST_INSERT_ID();";
                    int webTargetId = _currentTransaction.Connection.Query<int>(q).Single();
                    var http = target as HttpTarget;
                    http.DbId = webTargetId;
                    var dbHttp = new web_target_http() { ID_WEB_TARGET = http.DbId.Value, METHOD = http.Method.ToString(), URI = http.Uri };
                    string query = @"INSERT into WEB_TARGET_HTTP(ID_WEB_TARGET, METHOD, URI) values(@ID_WEB_TARGET, @Method, @Uri);";
                    _currentTransaction.Connection.Execute(query, dbHttp);
                    ret = webTargetId;
                    break;
                case WebTargetType.SQL:
                    break;
                default:
                    break;
            }
            return ret;
        }

        IDbTransaction _currentTransaction;
        public void BeginTransaction()
        {
            _currentTransaction = _connection.BeginTransaction();
        }

        public void RollBack()
        {
            _currentTransaction.Rollback();
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }

        public void Commit()
        {
            _currentTransaction.Commit();
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }

        public IWebTarget GetWebTarget(int? webTargetId)
        {
            if (webTargetId == null)
                return null;
            IWebTarget ret;
            try
            {
                string query = @"select ID_WEB_TARGET, web.ID_WEB_TARGET_TYPE, NAME  from web_target web 
                                inner join WEB_TARGET_TYPE web_type on web_type.ID_WEB_TARGET_TYPE = web.ID_WEB_TARGET_TYPE
                                where web.ID_WEB_TARGET =  @ID_WEB_TARGET;";
                object param = new { ID_WEB_TARGET = webTargetId };
                var target = _connection.Query<DbModel.web_target, DbModel.web_target_type, WebTargetReference>(query, mapperWebTarget, param: param, splitOn: "ID_WEB_TARGET_TYPE").FirstOrDefault();
                switch (target.Type)
                {
                    case Objects.WebTargetType.HTTP:
                        ret = GetTargetHttp(target.IdWebTarget);
                        break;
                    case Objects.WebTargetType.SQL:
                        ret = GetTargetSql(target.IdWebTarget);
                        break;
                    default:
                        _log.Error("invalid value for WebTargetType: " + target.Type);
                        ret = new InvalidTarget();
                        break;
                }
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = new InvalidTarget();
            }
            return ret;
        }

        public IWebTarget GetTargetHttp(int idWebTarget)
        {
            HttpTarget ret;
            try
            {
                string query = @"select * from web_target_http http where http.ID_WEB_TARGET = @ID_WEB_TARGET;";
                object param = new { ID_WEB_TARGET = idWebTarget };
                var target = _connection.Query<DbModel.web_target_http>(query, param: param).FirstOrDefault();
                ret = new HttpTarget(target.URI);
                ret.Method = new HttpMethod(target.METHOD);
                ret.Version = target.VERSION;
                if(!string.IsNullOrEmpty(target.HEADERS))
                {
                    var lines = target.HEADERS.Split('\n').ToList();
                    foreach (var item in lines)
                    {
                        var entry = new HeaderEntry("key", "value");
                        ret.Headers.Add(entry);
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = null;
            }
            return ret;
        }

        public IWebTarget GetTargetSql(int idWebTarget)
        {
            IWebTarget ret;
            try
            {
                string query = @"select * from web_target_sql sq where sq.ID_WEB_TARGET = @ID_WEB_TARGET;";
                object param = new { ID_WEB_TARGET = idWebTarget };
                var target = _connection.Query<DbModel.web_target_sql>(query, param: param).FirstOrDefault();
                ret = new SqlTarget(target.CONNECTION_STRING, target.QUERY, target.PROVIDER);
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = null;
            }
            return ret;
        }

        private WebTargetReference mapperWebTarget(DbModel.web_target target, DbModel.web_target_type myType)
        {
            var ret = new WebTargetReference();
            ret.Type = MyEnumParser<Objects.WebTargetType>(myType.NAME);
            ret.IdWebTarget = target.ID_WEB_TARGET;
            return ret;
        }

        public IList<Objects.XtraxDefinition> GetAllXtrax(int troxId)
        {
            List<Objects.XtraxDefinition> ret;
            try
            {
                string query = @"select * from xtrax_def def 
	                        INNER JOIN xtrax_type typ on def.ID_XTRAX_TYPE = typ.ID_XTRAX_TYPE
                            WHERE def.ID_TROX = @ID_TROX;";
                object param = new { ID_TROX = troxId };
                ret = _connection.Query<DbModel.xtrax_def, DbModel.xtrax_type, Objects.XtraxDefinition>(query, mapperXtrax, param: param, splitOn: "ID_XTRAX_TYPE").ToList();
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = new List<Objects.XtraxDefinition>();
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
                object param = new { ID_TROX = troxId };
                ret = _connection.Query<DbModel.checker_def, DbModel.checker_type, Objects.IChecker>(query, mapperChecker, param: param, splitOn: "ID_CHECKER_TYPE").ToList();
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = new List<Objects.IChecker>();
            }
            return ret;
        }

        public IList<Objects.Notifier> GetAllNotifier(int troxId)
        {
            List<Objects.Notifier> ret;
            try
            {
                // string query = @"select def.ID_TROX, def.ORDER, def.ARG, typ.ID_CHECKER_TYPE, typ.NAME 
                string query = @"select * 
                                    from NOTIFY_DEF def 
                                	INNER JOIN NOTIFY_TYPE typ on def.ID_NOTIFY_TYPE = typ.ID_NOTIFY_TYPE    
                                    WHERE def.ID_TROX = @ID_TROX;";
                object param = new { ID_TROX = troxId };
                ret = _connection.Query<DbModel.notify_def, DbModel.notify_type, Objects.Notifier>(query, mapperNotifier, param: param, splitOn: "ID_NOTIFY_TYPE").ToList();
            }
            catch (Exception e)
            {
                _log.Error(e);
                ret = new List<Objects.Notifier>();
            }
            return ret;
        }

        private Notifier mapperNotifier(DbModel.notify_def myDef, DbModel.notify_type myType)
        {
            var xType = MyEnumParser<Objects.NotifierType>(myType.NAME);
            var def = new NotifierParam(xType, myDef.ARG);
            var ret = NotifyFactory.Create(def);
            return ret;
        }

        private T MyEnumParser<T>(string value)
        {
            var ret = (T)Enum.Parse(typeof(T), value, true);
            return ret;
        }

        private IChecker mapperChecker(DbModel.checker_def myDef, DbModel.checker_type myType)
        {
            var xType = MyEnumParser<Objects.CheckerType>(myType.NAME);
            var def = new CheckerParam( xType, myDef.ARG);
            var ret = CheckerFactory.Create(def);
            return ret;
        }

        private Objects.XtraxDefinition mapperXtrax(DbModel.xtrax_def myDef, DbModel.xtrax_type myType)
        {
            var xType = MyEnumParser<Objects.XtraxType>(myType.NAME);
            var ret = new XtraxDefinition(DbToInt(myDef.ORDER), xType, myDef.ARG);
            return ret;
        }

        private Objects.Trox mapperTrox(DbModel.trox trox, DbModel.web_target webTarget, DbModel.web_target_type webTargetType, DbModel.web_target_http webTargetHttp)
        {
            var ret = new Objects.Trox(trox.NAME, DbToBool(trox.ENABLED), null, null, null, null);
            return ret;
        }

        public List<string> GetNames()
        {
            var ret = _connection.Query<string>("select NAME from TROX ").ToList();
            return ret;
        }


        private bool DbToBool(byte? b)
        {
            if (!b.HasValue)
                return false;
            bool ret = b != 0;
            return ret;
        }

        private byte? BoolToDb(bool b)
        {
            byte ret;
            ret = (byte) (b ? 1 : 0);
            return ret;
        }

        private int DbToInt(int? b)
        {
            if (!b.HasValue)
                return 0;
            int ret = b.Value;
            return ret;
        }

    }
}
