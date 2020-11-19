using NinjaStore.Parser.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace NinjaStore.Parser.Services
{
	public class ParserService : IParserService
	{
		private static Task<int> RunParserAsync(Process process)
		{
			var tcs = new TaskCompletionSource<int>();

			process.Exited += (s, ea) => tcs.SetResult(process.ExitCode);
			process.OutputDataReceived += (s, ea) => Console.WriteLine(ea.Data);
			process.ErrorDataReceived += (s, ea) => Console.WriteLine("ERR: " + ea.Data);

			bool started = process.Start();
			if (!started)
			{
				//you may allow for the process to be re-used (started = false) 
				//but I'm not sure about the guarantees of the Exited event in such a case
				throw new InvalidOperationException("Could not start process: " + process);
			}

			process.BeginOutputReadLine();
			process.BeginErrorReadLine();

			return tcs.Task;
		}


		public async Task<ParserResult> ParseAsync(string fileId, byte[] content)
		{
			ParserResult result = new ParserResult();

			// TODO Gergo: 1. create a directory named {fileId}
			string path = Path.Combine(Directory.GetCurrentDirectory(), fileId);

			DirectoryInfo di = Directory.CreateDirectory(path);

			try
			{

				Directory.SetCurrentDirectory(path);

				// TODO Gergo: 2. write the content to a file named {fileId}/content.caff (asynchronously)
				await Task.Run(() => File.WriteAllBytes(Path.Combine(path, "content.caff"), content));

				// TODO Gergo: 3. execute parser command (asynchronously)
				// Hint: https://stackoverflow.com/a/31492250

				using (var process = new Process
				{
					StartInfo =
					{
						FileName = "D:\\SzgbiztonsagHF\\Parser\\x64\\Debug\\Parser.exe", Arguments = path + "\\content.caff " + "D:\\SzgbiztonsagHF\\ParserTestFiles\\TEST",
						UseShellExecute = false, CreateNoWindow = true,
						RedirectStandardOutput = true, RedirectStandardError = true
					},
					EnableRaisingEvents = true
				})

				await RunParserAsync(process).ConfigureAwait(false);

				// TODO Gergo: 5. read Parser Metadata from {fileId}/content.json (asynchronously)
				// Hint: ParserMetadata metadata = await JsonSerializer.DeserializeAsync<ParserMetadata>(...)
				FileStream fileStream = new FileStream("content.json", FileMode.OpenOrCreate);

				ParserMetadata metadata = await JsonSerializer.DeserializeAsync<ParserMetadata>(fileStream);

				// TODO Gergo: 6. calculate Lenght from Parser Metadata Frames (use LINQ)
				// Hint: result.Lenght = metadata.Frames.Sum(...)

				result.LenghtInSeconds = (int)((metadata.frames[0].duration)/1000);

				// TODO Gergo: 7. read Preview from {fileId}/content_preview.bmp (asynchronously)
				// Hint: result.Preview = await File.ReadAllBytesAsync(...)
				result.Preview = await File.ReadAllBytesAsync(Path.Combine(path, "content_preview.bmp"));

			}
			catch (Exception e)
			{
				// TODO Gergo: 4. throw exception for error code
				Console.WriteLine(e.Message);
			}
			finally
			{
				// TODO Gergo: 8. remove {fileId} directory
				di.Delete();
			}

			return result;
		}
	}
}
