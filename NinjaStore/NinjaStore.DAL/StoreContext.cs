using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NinjaStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.DAL
{
	public class StoreContext : IdentityDbContext<User>
	{
		public DbSet<CaffMetadata> CaffMetadata { get; set; }
		public DbSet<CaffFile> CaffFiles { get; set; }
		public DbSet<Comment> Comments { get; set; }

		public StoreContext(DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CaffMetadata>().ToTable("Files");
			modelBuilder.Entity<CaffFile>().ToTable("Files");

			modelBuilder.Entity<Comment>()
				.Property(c => c.CaffMetadataFileId)
				.HasColumnName("FileId");

			modelBuilder.Entity<Comment>()
				.HasOne(c => c.User)
				.WithMany()
				.IsRequired();
		}
	}
}
