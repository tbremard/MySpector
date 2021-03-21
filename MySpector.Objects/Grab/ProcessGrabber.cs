using NLog;
using System;
using System.Diagnostics;

namespace MySpector.Objects
{
    public class ProcessGrabber : IGrabber
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public GrabResponse Grab(IGrabTarget target)
        {
            GrabResponse ret;
            if (target.TargetType != GrabTargetType.PROCESS)
            {
                return new InvalidResponse("target has invalid type:" + target.TargetType, TimeSpan.Zero);
            }
            var processTarget = target as ProcessTarget;
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                var startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = processTarget.FileName;
                startInfo.Arguments = processTarget.Arguments;
                startInfo.UseShellExecute = false; // Setting this property to false enables you to redirect input, output, and error streams.
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardInput = true;
                var process = Process.Start(startInfo);
                process.StandardInput.Write(processTarget.StandardInput);
                string stdOut = process.StandardOutput.ReadToEnd();
                if(processTarget.WaitForExitMs.HasValue)
                    process.WaitForExit(processTarget.WaitForExitMs.Value);
                else
                    process.WaitForExit();
                watch.Stop();
                _log.Debug("process.FileName: " + processTarget.FileName);
                _log.Debug("process.Arguments: " + processTarget.Arguments);
                _log.Debug("process.HasExited: " + process.HasExited);
                _log.Debug("process.ExitCode: "+ process.ExitCode);
                _log.Debug("process.Elapsed: " + watch.Elapsed);
                ret = new GrabResponse(stdOut, true, watch.Elapsed);
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                _log.Error(message);
                return new InvalidResponse(message, TimeSpan.Zero);
            }
            return ret;
        }
    }
}