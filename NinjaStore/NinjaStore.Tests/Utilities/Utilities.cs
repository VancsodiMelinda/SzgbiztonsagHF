using System;
using System.Collections.Generic;
using System.Text;
using NinjaStore.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NinjaStore.DAL.Models;

namespace NinjaStore.Tests
{
    public static class Utilities
    {
        public static DbContextOptions<StoreContext> TestDbContextOptions()
        {
            // Create a new service provider to create a new in-memory database.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance using an in-memory database and 
            // IServiceProvider that the context should resolve all of its 
            // services from.
            var builder = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        private static CaffFile generateCaffFile()
        {
            byte[] data = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            DateTime dateTime = new DateTime(2020, 1, 1, 1, 1, 1);
            byte[] byteArray = { 0, 100, 120, 210, 255 };
            string fileID = "testFileID";
            string userName = "Test User";

            CaffFile caffFile = new CaffFile
            {
                FileId = fileID,
                Data = data
            };

            Comment comment_1 = new Comment
            {
                CaffMetadataFileId = fileID,
                Username = "Test User 1",
                Text = "This is a test comment by Test User 1.",
                PostingTimestamp = new DateTimeOffset(dateTime)
            };

            Comment comment_2 = new Comment
            {
                CaffMetadataFileId = fileID,
                Username = "Test User 2",
                Text = "This is a test comment by Test User 2.",
                PostingTimestamp = new DateTimeOffset(dateTime)
            };

            List<Comment> commentList = new List<Comment>();
            commentList.Add(comment_1);
            commentList.Add(comment_2);

            CaffMetadata caffMetadata = new CaffMetadata
            {
                FileId = fileID,
                FileName = "testFileName",
                Description = "test description",
                Username = userName,
                UploadTimestamp = new DateTimeOffset(dateTime),
                FileSize = 1,
                Lenght = 7,
                DownloadCounter = 5,
                Preview = byteArray,
                File = caffFile,  // caffFile metadata is null
                Comments = commentList
            };

            caffFile.Metadata = caffMetadata;

            return caffFile;
        }
    }
}
