using NLog;
using System;
using System.Diagnostics;
using System.Text;

namespace MySpector.Objects
{
    public class ProcessGrabber : IGrabber
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        StringBuilder stdOut = new StringBuilder();

        public GrabResponse Grab(IGrabTarget target)
        {
            GrabResponse ret;
            if (target.TargetType != TargetType.PROCESS)
            {
                return new InvalidResponse(null, TimeSpan.Zero, "Target has invalid type:" + target.TargetType);
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
                _log.Debug("process.FileName : " + processTarget.FileName);
                _log.Debug("process.Arguments: " + processTarget.Arguments);
                _log.Debug("Starting process...");
                stdOut.Clear();
                var process = Process.Start(startInfo);
                process.OutputDataReceived += OutputDataReceivedHandler;
                process.BeginOutputReadLine();
                process.StandardInput.Write(processTarget.StandardInput);
                bool success;
                if (processTarget.TimeoutMs > 0)
                {
                    _log.Debug($"Timeout is set to: {processTarget.TimeoutMs}ms");
                    success = process.WaitForExit(processTarget.TimeoutMs);
                    if(!success)
                    {
                        _log.Error("Timeout occured!");
                        TryKill(process);
                    }
                    if (!process.HasExited)
                    {
                        _log.Error("Cannot stop process");
                    }
                }
                else
                {
                    _log.Debug("WaitForExit without timeout...");
                    process.WaitForExit();
                    success = true;
                }
                process.OutputDataReceived -= OutputDataReceivedHandler;
                _log.Debug("process.HasExited: " + process.HasExited);
                _log.Debug("process.ExitCode : " + process.ExitCode);
                process.CancelOutputRead();
                watch.Stop();
                _log.Debug("process.Elapsed  : " + watch.Elapsed);
                ret = new GrabResponse(stdOut.ToString(), success, watch.Elapsed, null);
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                _log.Error(message);
                return new InvalidResponse(message, TimeSpan.Zero, ex.Message);
            }
            return ret;
        }

        private void TryKill(Process process)
        {
            try
            {
                process.Kill();
            }
            catch (Exception e)
            {
                _log.Error(e);
            }
        }

        private void OutputDataReceivedHandler(object sender, DataReceivedEventArgs e)
        {
            stdOut.AppendLine(e.Data);
        }
    }
}