using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;
using NinjaStore.DAL;
using NinjaStore.BLL;
using Moq;
using System.Threading.Tasks;
using NinjaStore.Pages.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.DAL.Models;
using NinjaStore.Tests.Helper;
using NinjaStore.Parser.Services;
using Microsoft.Extensions.Logging;

namespace NinjaStore.Tests.UnitTests.Files.Delete
{
    public class FilesDeleteTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static FilesDeleteTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnGetAsync
        [Fact]
        public async Task OnGetAsync_ReturnsNotFoundResult_WhenIdIsNull()
        {
            Mock<IStoreLogic> mockIStoreLogic = new Mock<IStoreLogic>();
            Mock<ILogger<DeleteModel>> mockILogger = new Mock<ILogger<DeleteModel>>();
            var deleteModel = new DeleteModel(mockIStoreLogic.Object, mockILogger.Object);

            string id = null;
            var result = await deleteModel.OnGetAsync(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGetAsync_ReturnsNotFoundResult_WhenCaffMetadataIsNull()
        {
            /*
            Mock<IStoreLogic> mockIStoreLogic = new Mock<IStoreLogic>();
            var deleteModel = new DeleteModel(mockIStoreLogic.Object);

            string id = "notExistingID";
            var result = await deleteModel.OnGetAsync(id);

            Assert.IsType<NotFoundResult>(result);
            */

            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<DeleteModel>> mockILogger = new Mock<ILogger<DeleteModel>>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var deleteModel = new DeleteModel(storeLogic, mockILogger.Object);
                string id = "notExistingID";
                var result = await deleteModel.OnGetAsync(id);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task OnGetAsync_ReturnsPageResult_WhenCaffMetadataExistsWithId()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<DeleteModel>> mockILogger = new Mock<ILogger<DeleteModel>>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var deleteModel = new DeleteModel(storeLogic, mockILogger.Object);
                string id = "testFileID";
                var result = await deleteModel.OnGetAsync(id);

                Assert.IsType<PageResult>(result);
            }
        }

        // METHOD: OnPostAsync
        [Fact]
        public async Task OnPostAsync_ReturnsNotFoundResult_WhenIdIsNull()
        {
            Mock<IStoreLogic> mockIStoreLogic = new Mock<IStoreLogic>();
            Mock<ILogger<DeleteModel>> mockILogger = new Mock<ILogger<DeleteModel>>();

            var deleteModel = new DeleteModel(mockIStoreLogic.Object, mockILogger.Object);

            string id = null;
            var result = await deleteModel.OnPostAsync(id);

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenFileIsDeleted()
        {
            /*
            Mock<IStoreLogic> mockIStoreLogic = new Mock<IStoreLogic>();
            var deleteModel = new DeleteModel(mockIStoreLogic.Object);

            string id = "someID";
            var result = await deleteModel.OnPostAsync(id);

            Assert.IsType<RedirectToPageResult>(result);
            */
            
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<DeleteModel>> mockILogger = new Mock<ILogger<DeleteModel>>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var deleteModel = new DeleteModel(storeLogic, mockILogger.Object);
                string id = "testFileID";
                var result = await deleteModel.OnPostAsync(id);

                Assert.IsType<RedirectToPageResult>(result);
            }
        }
    }
}
