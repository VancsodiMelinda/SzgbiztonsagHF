using Microsoft.EntityFrameworkCore;
using NinjaStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.DAL
{
	public class StoreContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<CaffMetadata> CaffMetadata { get; set; }
		public DbSet<CaffFile> CaffFiles { get; set; }
		public DbSet<Comment> Comments { get; set; }

		public StoreContext(DbContextOptions options) : base(options)
		{
		}
	}
}
