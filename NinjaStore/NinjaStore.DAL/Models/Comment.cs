using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

		[ForeignKey("User")]
		[Required]
		public string Username { get; set; }

		public User User { get; set; }

		[Required]
		[MaxLength(100)]
		public string Text { get; set; }

		[Required]
		public DateTimeOffset PostingTimestamp { get; set; }
	}
}
