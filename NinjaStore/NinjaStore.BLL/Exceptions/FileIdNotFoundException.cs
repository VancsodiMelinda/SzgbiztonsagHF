using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.BLL.Exceptions
{
	[Serializable]
	public class FileIdNotFoundException : Exception
	{
		public string FileId { get; }

		public FileIdNotFoundException(string fileId) : base($"File ID '{fileId}' not found")
		{
			FileId = fileId;
		}
	}
}
