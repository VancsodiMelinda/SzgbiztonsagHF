using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.DAL.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string FileId { get; set; }
		public string Username { get; set; }
		public string Text { get; set; }
		public DateTimeOffset PostingTimestamp { get; set; }
	}
}
