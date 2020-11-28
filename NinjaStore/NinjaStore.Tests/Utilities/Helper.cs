using System;
using System.Collections.Generic;
using System.Text;
using NinjaStore.DAL.Models;
using NinjaStore.DAL;
using Microsoft.EntityFrameworkCore;

namespace NinjaStore.Tests.Helper
{
    public class Helper
    {
        public DbContextOptions<StoreContext> options { get; set; }
        public CaffFile testData;

        public Helper()
        {
            // create test data
            testData = generateCaffFile();

            // create in memory database
            /*
            options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryDbForTesting")
                .Options;
            */

            options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // fill up in memoy database with test data
            using (var context = new StoreContext(options))
            {
                // table: Files
                context.CaffFiles.Add(testData);
                context.CaffMetadata.Add(testData.Metadata);

                // table: Comments
                foreach (Comment comment in testData.Metadata.Comments)
                {
                    context.Comments.Add(comment);
                }

                context.SaveChanges();
            }
        }

        public CaffFile generateCaffFile()
        {
            byte[] data = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            DateTime dateTime = new DateTime(2020, 1, 1, 1, 1, 1);
            byte[] byteArray = { 0, 100, 120, 210, 255 };
            string fileID = "testFileID";
            string userName = "Test User";

            User user = new User() { UserName = userName };

            CaffFile caffFile = new CaffFile
            {
                FileId = fileID,
                Data = data
            };

            Comment comment_1 = new Comment
            {
                Id = 1,
                CaffMetadataFileId = fileID,
                //Username = "Test User 1",
                User = user,
                Text = "This is a test comment by Test User 1.",
                PostingTimestamp = new DateTimeOffset(dateTime)
            };

            Comment comment_2 = new Comment
            {
                Id = 2,
                CaffMetadataFileId = fileID,
                //Username = "Test User 2",
                User = user,
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
                //Username = userName,
                User = user,
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
