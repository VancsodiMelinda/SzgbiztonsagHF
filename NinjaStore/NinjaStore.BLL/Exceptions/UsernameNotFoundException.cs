using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.BLL.Exceptions
{
	[Serializable]
	public class UsernameNotFoundException : Exception
	{
		public string Username { get; }

		public UsernameNotFoundException(string username) : base($"Username '{username}' not found")
		{
			Username = username;
		}
	}
}
