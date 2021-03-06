using Dapper;
using System.Data;
using System.Linq;
using System;
using NLog;
using MySpector.Objects;
using MySpector.Repo.DbModel;
using System.Globalization;

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

        public bool WebTargetType()
        {
            bool ret = CheckEnumGeneric<WebTargetType, web_target_type>("WEB_TARGET_TYPE");
            return ret;
        }


        public bool CheckEnumGeneric<TClient, TDb>(string table)
            where TClient : struct, IConvertible
            where TDb : IEnumDef
        {
            bool ret = true;
            if (!typeof(TClient).IsEnum) throw new System.ArgumentException("T must be an enumerated type");

            try
            {
                string query = "select * from " + table;
                var items = _connection.Query<TDb>(query).ToList();
                foreach (var x in items)
                {
                    var type = MyEnum.Parse<TClient>(x.NAME);
                    int clientCode = (int)type.ToInt32(CultureInfo.InvariantCulture);
                    int dbCode = x.ID_TYPE;
                    if (clientCode != dbCode)
                    {
                        _log.Error($"Enum integrity failed: CheckerType.{x.NAME}: different code between client({clientCode}) and DB({dbCode})");
                        ret = false;
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
