using NinjaStore.Parser.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NinjaStore.Parser.Services
{
	public class ParserService : IParserService
	{
		public async Task<ParserResult> ParseAsync(string fileId, byte[] content)
		{
			ParserResult result = new ParserResult();

			// TODO Gergo: 1. create a directory named {fileId}

			// TODO Gergo: 2. write the content to a file named {fileId}/content.caff (asynchronously)

			// TODO Gergo: 3. execute parser command (asynchronously)
			// Hint: https://stackoverflow.com/a/31492250

			// TODO Gergo: 4. throw exception for error code

			// TODO Gergo: 5. read Parser Metadata from {fileId}/content.json (asynchronously)
			// Hint: ParserMetadata metadata = await JsonSerializer.DeserializeAsync<ParserMetadata>(...)

			// TODO Gergo: 6. calculate Lenght from Parser Metadata Frames (use LINQ)
			// Hint: result.Lenght = metadata.Frames.Sum(...)

			// TODO Gergo: 7. read Preview from {fileId}/content_preview.bmp (asynchronously)
			// Hint: result.Preview = await File.ReadAllBytesAsync(...)

			// TODO Gergo: 8. remove {fileId} directory

			return result;
		}
	}
}
