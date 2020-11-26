using Microsoft.EntityFrameworkCore;
using NinjaStore.DAL;
using NinjaStore.DAL.Models;
using NinjaStore.Parser.Data;
using NinjaStore.Parser.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NinjaStore.BLL
{
	public class StoreLogic : IStoreLogic
	{
		private const int MAX_FILE_SIZE = 100 * 1024 * 1024;

		private StoreContext _storeContext;
		private IParserService _parserService;

		public StoreLogic(StoreContext storeContext, IParserService parserService)
		{
			_storeContext = storeContext;
			_parserService = parserService;
		}

		public async Task<CaffMetadata> GetMetadataWithCommentsAsync(string fileId)
		{
			CaffMetadata metadata = await _storeContext.CaffMetadata
				.Include(cm => cm.Comments)
				.FirstOrDefaultAsync(cm => cm.FileId == fileId);

			return metadata;
		}

		public async Task<List<CaffMetadata>> QueryMetadataByFreeTextAsync(string filter)
		{
			// TODO Dani: use free text filter
			List<CaffMetadata> result = await _storeContext.CaffMetadata
				.Where(cm => string.IsNullOrWhiteSpace(filter) || cm.FileName.Contains(filter))
				.ToListAsync();

			return result;
		}

		public async Task<string> UploadFileAsync(string username, string fileName, string description, byte[] content)
		{
			if (string.IsNullOrWhiteSpace(fileName))
			{
				// TODO Dani: throw exception
			}

			// TODO Dani: validate file name with regex

			// TODO Dani: check if file name already exists for user

			if (content == null || content.Length == 0)
			{
				// TODO Dani: throw exception
			}

			if (content.Length > MAX_FILE_SIZE)
			{
				// TODO Dani: throw exception
			}

			string fileId = Guid.NewGuid().ToString("N");

			ParserResult parserResult = await _parserService.ParseAsync(fileId, content);

			CaffMetadata metadata = new CaffMetadata
			{
				FileId = fileId,
				FileName = fileName,
				Description = description,
				Username = username,
				UploadTimestamp = DateTimeOffset.UtcNow,
				FileSize = content.Length,
				Lenght = parserResult.LenghtInSeconds,
				DownloadCounter = 0,
				Preview = parserResult.Preview,
			};

			CaffFile file = new CaffFile
			{
				FileId = fileId,
				Data = content,
			};

			metadata.File = file;
			file.Metadata = metadata;

			await _storeContext.CaffFiles.AddAsync(file);
			await _storeContext.SaveChangesAsync();

			return fileId;
		}

		public async Task<CaffFile> DownloadFileAsync(string fileId)
		{
			CaffFile file = await _storeContext.CaffFiles
				.Include(cf => cf.Metadata)
				.FirstOrDefaultAsync(cf => cf.FileId == fileId);

			if (file != null)
			{
				file.Metadata.DownloadCounter++;
				await _storeContext.SaveChangesAsync();
			}
			
			return file;
		}

		public async Task DeleteFileAsync(string fileId)
		{
			CaffFile file = await _storeContext.CaffFiles.FindAsync(fileId);
			if (file != null)
			{
				_storeContext.CaffFiles.Remove(file);
				await _storeContext.SaveChangesAsync(); 
			}
		}

		public async Task<Comment> GetCommentAsync(int id)
		{
			Comment comment = await _storeContext.Comments.FindAsync(id);
			return comment;
		}

		public async Task<int> InsertCommentAsync(string fileId, string username, string comment)
		{
			if (string.IsNullOrWhiteSpace(comment))
			{
				// TODO Dani: throw exception
			}

			CaffMetadata metadata = await GetMetadataWithCommentsAsync(fileId);

			if (metadata == null)
			{
				// TODO Dani: throw exception
			}

			Comment newComment = new Comment
			{
				CaffMetadataFileId = metadata.FileId,
				Username = username,
				Text = comment,
				PostingTimestamp = DateTimeOffset.UtcNow,
			};

			metadata.Comments.Add(newComment);

			await _storeContext.SaveChangesAsync();

			return newComment.Id;
		}

		public async Task DeleteCommentAsync(int id)
		{
			Comment comment = await _storeContext.Comments.FindAsync(id);
			if (comment != null)
			{
				_storeContext.Comments.Remove(comment);
				await _storeContext.SaveChangesAsync();
			}
		}
	}
}
