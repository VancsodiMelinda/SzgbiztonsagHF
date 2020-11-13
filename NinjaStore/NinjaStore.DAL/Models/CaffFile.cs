using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.DAL.Models
{
	public class CaffFile
	{
		public CaffMetadata Metadata { get; set; }
		public byte[] Data { get; set; }
	}
}
