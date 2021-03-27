using NLog;
using NLog.Config;
using NLog.Targets;
using System.Text;

namespace MySpector.Core
{
    public class MemoryLogger
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        MemoryTarget _memoryTarget;
        LoggingRule _memoryRule;

        public void Start()
        {
            _memoryTarget = new MemoryTarget("memoryTarget");
            _memoryTarget.Layout = "${longdate} | ${pad:padding=20:inner=${logger:shortName=true:truncate=20}} | ${pad:padding=5:inner=${level:uppercase=true}} | ${message}";
            LogManager.Configuration.AddTarget(_memoryTarget);
            _memoryRule = new LoggingRule("memoryRule");
            _memoryRule.EnableLoggingForLevels(LogLevel.Trace, LogLevel.Fatal);
            _memoryRule.Targets.Add(_memoryTarget);
            _memoryRule.LoggerNamePattern = "*";
            LogManager.Configuration.LoggingRules.Add(_memoryRule);
            LogManager.ReconfigExistingLoggers();
        }

        public StringBuilder Stop()
        {
            LogManager.Configuration.RemoveTarget(_memoryTarget.Name);
            bool removed = LogManager.Configuration.LoggingRules.Remove(_memoryRule);
            if (!removed)
                _log.Error("cannot remove rule "+_memoryRule.RuleName);
            LogManager.ReconfigExistingLoggers();
            var ret = new StringBuilder();
            foreach (string s in _memoryTarget.Logs)
            {
                ret.AppendLine(s);
            }
            return ret;
        }
    }
}