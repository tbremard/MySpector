using Dapper;
using System.Data;
using System.Linq;
using System;
using NLog;
using MySpector.Objects;
using MySpector.Repo.DbModel;
using System.Globalization;
using System.Collections.Generic;

namespace MySpector.Repo
{
    public class EnumIntegrity
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        IDbConnection _connection;

        public EnumIntegrity(IDbConnection connection)
        {
            _connection = connection;
        }

        public bool CheckerType()
        {
            bool ret = CheckEnumGeneric<CheckerType, checker_type>("CHECKER_TYPE");
            return ret;
        }

        public bool XtraxType()
        {
            bool ret = CheckEnumGeneric<XtraxType, xtrax_type>("XTRAX_TYPE");
            return ret;
        }

        public bool NotifyType()
        {
            bool ret = CheckEnumGeneric<NotifyType, notify_type>("NOTIFY_TYPE");
            return ret;
        }

        public bool TargetType()
        {
            bool ret = CheckEnumGeneric<TargetType, web_target_type>("TARGET_TYPE");
            return ret;
        }

        public bool CheckEnumGeneric<TClient, TDb>(string table)
            where TClient : struct, IConvertible
            where TDb : IEnumDef
        {
            bool ret = true;
            if (!typeof(TClient).IsEnum) throw new ArgumentException("T must be an enumerated type");

            try
            {
                var clientValues = Enum.GetValues(typeof(TClient));
                var resolved = new Dictionary<TClient, bool>();
                foreach (var item in clientValues)
                {
                    resolved.Add((TClient)item, false);
                }
                string query = "select * from " + table;
                var items = _connection.Query<TDb>(query).ToList();
                foreach (var x in items)
                {
                    try
                    {
                        var type = MyEnum.Parse<TClient>(x.NAME);
                        int clientCode = (int)type.ToInt32(CultureInfo.InvariantCulture);
                        int dbCode = x.ID_TYPE;
                        if (clientCode != dbCode)
                        {
                            _log.Error($"Enum integrity failed: CheckerType.{x.NAME}: different code between client({clientCode}) and DB({dbCode})");
                            ret = false;
                        }
                        resolved[type] = true;
                    }
                    catch (ArgumentException ex)
                    {
                        _log.Error($"Cannot parse value from DB: '{x.NAME}' to local Enum." +typeof(TClient).Name+": " + ex.Message);
                        ret = false;
                    }
                }
                var unresolved = resolved.Where(x => x.Value == false).Select(x => x.Key).ToList();
                if(unresolved.Count > 0)
                {
                    ret = false;
                    foreach (var item in unresolved)
                    {
                        _log.Error("Enum: " +typeof(TClient).Name+"." + item + " has no correspondancy in database");
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ret = false;
            }
            return ret;
        }

    }
}
