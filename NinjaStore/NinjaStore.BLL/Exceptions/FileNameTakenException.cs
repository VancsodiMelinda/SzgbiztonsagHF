using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.BLL.Exceptions
{
	[Serializable]
	public class FileNameTakenException : Exception
	{
		public string FileName { get; }
		public string Username { get; }

		public FileNameTakenException(string fileName, string username)
			: base($"File name '{fileName}' already taken by user '{username}'")
		{
			FileName = fileName;
			Username = username;
		}
	}
}
