using System.Diagnostics;

namespace Telomeres.Services
{
    public class AgilentService : INodeServices
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<T> InvokeAsync<T>(string moduleName, params object[] args)
        {
            throw new NotImplementedException();
        }

        public Task<T> InvokeAsync<T>(string moduleName, CancellationToken cancellationToken, params object[] args)
        {
            throw new NotImplementedException();
        }

        public Task<T> InvokeExportAsync<T>(string moduleName, string exportedFunctionName, params object[] args)
        {
            throw new NotImplementedException();
        }

        public Task<T> InvokeExportAsync<T>(string moduleName, string exportedFunctionName, CancellationToken cancellationToken, params object[] args)
        {
            throw new NotImplementedException();
        }

        public string Run(string script, string[] args)
        {
            using Process process = new()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "agilent",
                    Arguments = $"{script} {string.Join(" ", args)}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            string s = string.Empty;
            string e = string.Empty;
            process.OutputDataReceived += (sender, data) => s += data.Data;
            process.ErrorDataReceived += (sender, data) => e += data.Data;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
            process.Close();
            return s;
        }
    }
}
