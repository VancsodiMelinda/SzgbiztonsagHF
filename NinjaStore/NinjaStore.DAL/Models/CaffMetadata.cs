using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NinjaStore.DAL.Models
{
	public class CaffMetadata
	{
		[Key]
		[MaxLength(32)]
		public string FileId { get; set; }

		[Required]
		[MaxLength(32)]
		public string FileName { get; set; }

		[MaxLength(200)]
		public string Description { get; set; }

		public User User { get; set; }

		[Required]
		public DateTimeOffset UploadTimestamp { get; set; }

		[Required]
		public int FileSize { get; set; }

		public TimeSpan Duration => TimeSpan.FromSeconds(Lenght);

		[Required]
		public int Lenght { get; set; }

		[Required]
		public int DownloadCounter { get; set; }

		[Required]
		public byte[] Preview { get; set; }

		[Required]
		virtual public CaffFile File { get; set; }
		
		public ICollection<Comment> Comments { get; set; }
	}
}
