using Microsoft.EntityFrameworkCore;
using NinjaStore.BLL.Exceptions;
using NinjaStore.DAL;
using NinjaStore.DAL.Models;
using NinjaStore.Parser.Data;
using NinjaStore.Parser.Services;
using System;
using System.Collections.Generic;
using System.IO;
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
				.Include(u => u.User)
				.Include(cm => cm.Comments)
				.ThenInclude(c => c.User)
				.FirstOrDefaultAsync(cm => cm.FileId == fileId);

			return metadata;
		}

		public async Task<List<CaffMetadata>> QueryMetadataByFreeTextAsync(string filter)
		{
			// TODO Dani: use free text filter
			List<CaffMetadata> result = await _storeContext.CaffMetadata
				.Where(cm => string.IsNullOrWhiteSpace(filter) || cm.FileName.Contains(filter))
				.Include(u => u.User)
				.OrderByDescending(cm => cm.UploadTimestamp)
				.ToListAsync();

			return result;
		}

		public async Task<string> UploadFileAsync(string username, string fileName, string description, byte[] content)
		{
			if (string.IsNullOrWhiteSpace(fileName))
			{
				throw new InvalidFileNameException();
			}

			char[] invalidCharacters = Path.GetInvalidFileNameChars();
			if (fileName.IndexOfAny(invalidCharacters) != -1)
			{
				throw new InvalidFileNameException();
			}

			if (content == null || content.Length == 0)
			{
				throw new EmptyFileException();
			}

			if (content.Length > MAX_FILE_SIZE)
			{
				throw new LargeFileException(MAX_FILE_SIZE);
			}

			User user = await _storeContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

			if (user == null)
			{
				throw new UsernameNotFoundException(username);
			}

			bool fileNameAlreadyExistsForUser = await _storeContext.CaffMetadata
				.AnyAsync(cm => cm.User.Id == user.Id && cm.FileName == fileName);

			if (fileNameAlreadyExistsForUser)
			{
				throw new FileNameTakenException(fileName, username);
			}

			string fileId = Guid.NewGuid().ToString("N");

			ParserResult parserResult = await _parserService.ParseAsync(fileId, content);

			CaffMetadata metadata = new CaffMetadata
			{
				FileId = fileId,
				FileName = fileName,
				Description = description,
				User = user,
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
				.ThenInclude(cm => cm.User)
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

		public async Task RemoveUserFromFilesAsync(string username)
		{
			List<CaffMetadata> metadataList = await _storeContext.CaffMetadata
				.Where(cm => cm.User.UserName == username)
				.Include(cm => cm.User)
				.ToListAsync();

			foreach (var metadata in metadataList)
			{
				metadata.User = null;
			}

			_storeContext.SaveChanges();
		}

		public async Task<Comment> GetCommentAsync(int id)
		{
			Comment comment = await _storeContext.Comments
								.Include(u => u.User)
								.FirstOrDefaultAsync(c => c.Id == id);
			return comment;
		}

		public async Task<int> InsertCommentAsync(string fileId, string username, string comment)
		{
			if (string.IsNullOrWhiteSpace(comment))
			{
				throw new EmptyCommentException();
			}

			CaffMetadata metadata = await GetMetadataWithCommentsAsync(fileId);

			if (metadata == null)
			{
				throw new FileNotFoundException(fileId);
			}

			User user = await _storeContext.Users.FirstOrDefaultAsync(u => u.UserName == username);

			if(user == null)
			{
				throw new UsernameNotFoundException(username);
			}

			Comment newComment = new Comment
			{
				CaffMetadataFileId = metadata.FileId,
				User = user,
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
