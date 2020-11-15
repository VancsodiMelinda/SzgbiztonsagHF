using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NinjaStore.DAL.Models
{
	public class User
	{
		[Key]
		[MaxLength(16)]
		public string Username { get; set; }

		[Required]
		[MaxLength(40)]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public UserRole Role { get; set; }
	}

	public enum UserRole
	{
		Admin = 0,
		User = 1,
	}
}
