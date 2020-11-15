using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NinjaStore.DAL.Models
{
	public class CaffFile
	{
		[Key]
		[MaxLength(32)]
		public string FileId { get; set; }

		[Required]
		virtual public CaffMetadata Metadata { get; set; }

		[Required]
		public byte[] Data { get; set; }
	}
}
