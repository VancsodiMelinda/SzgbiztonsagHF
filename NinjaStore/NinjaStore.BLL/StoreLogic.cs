using NinjaStore.DAL;
using NinjaStore.DAL.Models;
using NinjaStore.Parser.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NinjaStore.BLL
{
	public class StoreLogic : IStoreLogic
	{
		private StoreContext _storeContext;
		private IParserService _parserService;

		public StoreLogic(StoreContext storeContext, IParserService parserService)
		{
			_storeContext = storeContext;
			_parserService = parserService;
		}

		public Task<CaffMetadata> GetMetadataWithCommentsByFileIdAsync(string fileId)
		{
			// TODO Dani: implement
			throw new NotImplementedException();
		}

		public Task<List<CaffMetadata>> QueryMetadataByFreeTextAsync(string filter)
		{
			// TODO Dani: implement
			throw new NotImplementedException();
		}

		public Task<string> UploadFileAsync(string username, string fileName, string description, byte[] content)
		{
			// TODO Dani: implement
			throw new NotImplementedException();
		}

		public Task<CaffFile> DownloadFileAsync(string fileId)
		{
			// TODO Dani: implement
			throw new NotImplementedException();
		}

		public Task DeleteFileAsync(string fileId)
		{
			// TODO Dani: implement
			throw new NotImplementedException();
		}

		public Task<Comment> GetCommentByIdAsync(int id)
		{
			// TODO Dani: implement
			throw new NotImplementedException();
		}

		public Task<int> InsertCommentAsync(string fileId, string usename, string comment)
		{
			// TODO Dani: implement
			throw new NotImplementedException();
		}

		public Task DeleteCommentAsync(int id)
		{
			// TODO Dani: implement
			throw new NotImplementedException();
		}
	}
}
