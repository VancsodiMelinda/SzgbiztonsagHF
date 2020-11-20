using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace NinjaStore.Parser.Services
{
	public static class CommandLine
	{
        public static Task<int> ExecuteAsync(string command, params string[] args)
		{
            var taskCompletionSource  = new TaskCompletionSource<int>();

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
				{
                    FileName = command,
                    Arguments = args.Length == 0 ? "" : string.Join(" ", args),
                },
                EnableRaisingEvents = true,
            };

            process.Exited += (sender, args) =>
            {
                taskCompletionSource.SetResult(process.ExitCode);
                process.Dispose();
            };

            process.Start();

            return taskCompletionSource.Task;
        }
	}
}
