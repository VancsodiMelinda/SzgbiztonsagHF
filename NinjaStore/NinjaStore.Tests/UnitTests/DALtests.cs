using System;
using Xunit;
using NinjaStore.Pages.Files;
using Microsoft.EntityFrameworkCore;
using NinjaStore.DAL.Models;
using NinjaStore.DAL;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NinjaStore.BLL;
using NinjaStore.Parser.Services;
using System.Diagnostics;

namespace NinjaStore.Tests.UnitTests
{
    public class DALtests
    {
        static DbContextOptions<StoreContext> options { get; set;}
        static CaffFile testData;

        static DALtests()
        {
            // create test data
            testData = generateCaffFile();

            // create in memory database
            options = new DbContextOptionsBuilder<StoreContext>()
                .UseInMemoryDatabase(databaseName: "inMemoryDbForTesting")
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

        [Fact]
        public async Task GetMetadataWithCommentsAsync_ReturnCaffmetadataByFileId()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                string fileID = "testFileID";
                CaffMetadata result = await storeLogic.GetMetadataWithCommentsAsync(fileID);

                //CaffMetadata expectedResult = testData.Metadata;

                Assert.Equal("testFileID", result.FileId);
            }
        }

        [Fact]
        public async Task QueryMetadataByFreeTextAsync_ReturnCaffmetadataListByFileName()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                string filter = "test";
                List<CaffMetadata> tempResult = await storeLogic.QueryMetadataByFreeTextAsync(filter);

                List<bool> result = new List<bool>();
                List<bool> expectedResult = new List<bool>(tempResult.Count) { true };
                foreach (CaffMetadata metadata in tempResult)
                {
                    result.Add(metadata.FileName.Contains(filter));
                }

                //List<CaffMetadata> expectedResult = new List<CaffMetadata>();
                //expectedResult.Add(testData.Metadata);

                Assert.Equal(expectedResult, result);
            }
        }

        static CaffFile generateCaffFile()
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
