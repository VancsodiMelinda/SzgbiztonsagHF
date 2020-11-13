using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.DAL.Models
{
	public class CaffMetadata
	{
		public string FileId { get; set; }
		public string FileName { get; set; }
		public string Description { get; set; }
		public string Username { get; set; }
		public DateTimeOffset UploadTimestamp { get; set; }
		public int FileSize { get; set; }
		public TimeSpan Lenght { get; set; }
		public int DownloadCounter { get; set; }
		public byte[] Preview { get; set; }
		public List<Comment> Comments { get; set; }
	}
}
