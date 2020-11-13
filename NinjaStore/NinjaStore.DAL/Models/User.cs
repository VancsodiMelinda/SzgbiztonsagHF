using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.DAL.Models
{
	public class User
	{
		public string Username { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public UserRole Role { get; set; }
	}

	public enum UserRole
	{
		Admin,
		User,
	}
}
