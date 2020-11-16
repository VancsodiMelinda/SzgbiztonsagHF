using NinjaStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NinjaStore.BLL
{
	public interface IStoreLogic
	{
		Task<CaffMetadata> GetMetadataWithCommentsAsync(string fileId);
		Task<List<CaffMetadata>> QueryMetadataByFreeTextAsync(string filter);
		Task<string> UploadFileAsync(string username, string fileName, string description, byte[] content);
		Task<CaffFile> DownloadFileAsync(string fileId);
		Task DeleteFileAsync(string fileId);
		Task<Comment> GetCommentAsync(int id);
		Task<int> InsertCommentAsync(string fileId, string username, string comment);
		Task DeleteCommentAsync(int id);
	}
}
