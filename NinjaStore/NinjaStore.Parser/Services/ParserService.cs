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
				await Task.Run(() => File.WriteAllBytes(Path.Combine(path, "content.caff"), content));

				// TODO Gergo: 5. read Parser Metadata from {fileId}/content.json (asynchronously)
				// Hint: ParserMetadata metadata = await JsonSerializer.DeserializeAsync<ParserMetadata>(...)
				FileStream fileStream = new FileStream("content.json", FileMode.OpenOrCreate);

				ParserMetadata metadata = new ParserMetadata();

				metadata = await JsonSerializer.DeserializeAsync<ParserMetadata>(fileStream);

				// TODO Gergo: 6. calculate Lenght from Parser Metadata Frames (use LINQ)
				// Hint: result.Lenght = metadata.Frames.Sum(...)

				result.LenghtInSeconds = metadata.ReturnSum();

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
