using System.Diagnostics;
using System.IO;
using System.Text;

namespace CommandLineDeploymentTool.Helpers
{
    static class ProcessHelper
    {
        /// <summary>
        /// runs the requested program and returns the results in a string
        /// </summary>
        /// <param name="fileName">program to run</param>
        /// <param name="arguments">program parameters</param>
        /// <returns></returns>
        public static string CallNativeWindowProcess(string fileName, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = fileName;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.Arguments = arguments;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;

            StringBuilder sb = new StringBuilder();
            using (Process exeProcess = Process.Start(startInfo))
            {
                using (StreamReader sr = exeProcess.StandardError)
                {
                    sb.Append(sr.ReadToEnd());
                }
                using (StreamReader sr = exeProcess.StandardOutput)
                {
                    sb.Append(sr.ReadToEnd());
                }
                exeProcess.WaitForExit();
            }

            return sb.ToString();
        }
    }
}
