using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NinjaStore.DAL.Models
{
	public class Comment
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(32)]
		public string CaffMetadataFileId { get; set; }

		[Required]
		[MaxLength(16)]
		public string Username { get; set; }

		[Required]
		[MaxLength(100)]
		public string Text { get; set; }

		[Required]
		public DateTimeOffset PostingTimestamp { get; set; }
	}
}
