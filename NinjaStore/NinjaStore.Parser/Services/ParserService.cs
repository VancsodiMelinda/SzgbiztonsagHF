using NinjaStore.Parser.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;
using NinjaStore.Parser.Exceptions;

namespace NinjaStore.Parser.Services
{
	public class ParserService : IParserService
	{
		private const string CAFF_FILE_NAME = "content.caff";
		private const string METADATA_FILE_NAME = "content.json";
		private const string PREVIEW_FILE_NAME = "content_preview.bmp";

		public async Task<ParserResult> ParseAsync(string fileId, byte[] content)
		{
			string tempFileDirectory = Path.Combine(Path.GetTempPath(), fileId);

			try
			{
				// TODO Gergo: 1. create a directory named {fileId}
				Directory.CreateDirectory(tempFileDirectory);

				// TODO Gergo: 2. write the content to a file named {fileId}/content.caff (asynchronously)
				string file = Path.Combine(tempFileDirectory, CAFF_FILE_NAME);
				await File.WriteAllBytesAsync(file, content);

				// TODO: Dani: resolve Parser.exe path
				string parserCommand = @"C:\WORK\SzgbiztonsagHF\Parser\x64\Release\Parser.exe";

				// TODO Gergo: 3. execute parser command (asynchronously)
				// Hint: https://stackoverflow.com/a/31492250
				int resultCode = await CommandLine.ExecuteAsync(parserCommand, file, tempFileDirectory + Path.DirectorySeparatorChar);

				// TODO Gergo: 4. throw exception for error code
				switch ((ErrorCodes)resultCode)
				{
					case ErrorCodes.SUCCESS:
						break;
					case ErrorCodes.UNKNOWN_ERROR:
						throw new InternalParserException("Unknown error");
					case ErrorCodes.EARGC_MISMATCH:
						throw new InternalParserException("Argument mismatch");
					case ErrorCodes.ENO_TRAILING_SEP:
						throw new InternalParserException("Trailing directory separator is missing from output directory");
					case ErrorCodes.EDOT_FOUND:
						throw new InternalParserException("Input file name contains '.'");
					case ErrorCodes.EIN_NOT_FOUND:
						throw new InternalParserException("Input file not found");
					case ErrorCodes.EOUT_OPEN_FAIL:
						throw new InternalParserException("Failed to open output file");
					case ErrorCodes.EINVALID_ID:
						throw new InvalidCaffFileContentException("Invalid id");
					case ErrorCodes.EBAD_MAGIC:
						throw new InvalidCaffFileContentException("Bad magic");
					case ErrorCodes.EHEAD_ID_MISMATCH:
						throw new InvalidCaffFileContentException("Header id mismatch");
					case ErrorCodes.EHEAD_SIZE_MISMATCH:
						throw new InvalidCaffFileContentException("Header size mismatch");
					case ErrorCodes.ETOO_MUCH_BLOCKS:
						throw new InvalidCaffFileContentException("Too much blocks");
					case ErrorCodes.EOF_IN_CAPTION:
						throw new InvalidCaffFileContentException("EoF in caption");
					case ErrorCodes.ELONG_CIFF_HEAD:
						throw new InvalidCaffFileContentException("Long ciff header");
					case ErrorCodes.EOF_IN_TAGS:
						throw new InvalidCaffFileContentException("EoF in tags");
					case ErrorCodes.EMISSING_TAG_END:
						throw new InvalidCaffFileContentException("Missing tag end");
					case ErrorCodes.ESIZE_TRUNC:
						throw new InvalidCaffFileContentException("Size truncation");
					case ErrorCodes.ECONT_SIZE_MISMATCH:
						throw new InvalidCaffFileContentException("Size mismatch");
					case ErrorCodes.ELONGER_CONTENT:
						throw new InvalidCaffFileContentException("Longer content");
					case ErrorCodes.EBS_MISMATCH:
						throw new InvalidCaffFileContentException("Block size mismatch");
					case ErrorCodes.ECIFF_HEAD_SIZE_MISMATCH:
						throw new InvalidCaffFileContentException("Ciff header size mismatch");
					case ErrorCodes.EMULTIPLE_CREDITS:
						throw new InvalidCaffFileContentException("Multiple credits");
					case ErrorCodes.EEMPTY_FRAME:
						throw new InvalidCaffFileContentException("Empty frame");
					default:
						throw new InternalParserException("Unknown error");
				}

				ParserResult result = new ParserResult();

				// TODO Gergo: 5. read Parser Metadata from {fileId}/content.json (asynchronously)
				// Hint: ParserMetadata metadata = await JsonSerializer.DeserializeAsync<ParserMetadata>(...)
				string metadataFile = Path.Combine(tempFileDirectory, METADATA_FILE_NAME);
				using (FileStream fileStream = new FileStream(metadataFile, FileMode.Open))
				{
					ParserMetadata metadata = await JsonSerializer.DeserializeAsync<ParserMetadata>(fileStream);

					// TODO Gergo: 6. calculate Lenght from Parser Metadata Frames (use LINQ)
					// Hint: result.Lenght = metadata.Frames.Sum(...)
					result.LenghtInSeconds = metadata.frames.Sum(f => f.duration) / 1000;
				}

				// TODO Gergo: 7. read Preview from {fileId}/content_preview.bmp (asynchronously)
				// Hint: result.Preview = await File.ReadAllBytesAsync(...)
				string previewFile = Path.Combine(tempFileDirectory, PREVIEW_FILE_NAME);
				result.Preview = await File.ReadAllBytesAsync(previewFile);

				return result;
			}
			finally
			{
				// TODO Gergo: 8. remove {fileId} directory
				if (Directory.Exists(tempFileDirectory)) Directory.Delete(tempFileDirectory, true);
			}
		}
	}
}
