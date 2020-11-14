using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NinjaStore.Parser.Exceptions
{
	public class EmptyFrameException : Exception
	{
		public EmptyFrameException()
		{
		}

		public EmptyFrameException(string message) : base(message)
		{
		}
	}
}
