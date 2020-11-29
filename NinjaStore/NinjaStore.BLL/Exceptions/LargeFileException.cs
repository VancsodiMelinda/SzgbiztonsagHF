using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.BLL.Exceptions
{
	[Serializable]
	public class LargeFileException : Exception
	{
		public int MaxSize { get; }

		public LargeFileException(int maxSize) : base($"File size exceeded the maximum size of {maxSize} bytes")
		{
			MaxSize = maxSize;
		}
	}
}
