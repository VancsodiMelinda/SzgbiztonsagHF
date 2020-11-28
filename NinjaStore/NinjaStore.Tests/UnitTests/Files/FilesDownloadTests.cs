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

namespace NinjaStore.Tests.UnitTests.Files.Download
{
    public class FilesDownloadTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static FilesDownloadTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnGetAsync
        [Fact]
        public async Task OnGetAsync_ReturnsNotFoundResult_WhenIdIsNullOrWhiteSpace()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<DownloadModel>> mockILogger = new Mock<ILogger<DownloadModel>>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var downloadModel = new DownloadModel(storeLogic, mockILogger.Object);
                string id = null;
                var result = await downloadModel.OnGetAsync(id);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task OnGetAsync_ReturnsFileResult_WhenFileExists()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<DownloadModel>> mockILogger = new Mock<ILogger<DownloadModel>>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var downloadModel = new DownloadModel(storeLogic, mockILogger.Object);
                string id = "testFileID";
                var result = await downloadModel.OnGetAsync(id);

                Assert.IsType<FileContentResult>(result);
            }
        }

        [Fact]
        public async Task OnGetAsync_ReturnsNotFoundResult_WhenFileDoesNotExists()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<DownloadModel>> mockILogger = new Mock<ILogger<DownloadModel>>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var downloadModel = new DownloadModel(storeLogic, mockILogger.Object);
                string id = "notExistingFileID";
                var result = await downloadModel.OnGetAsync(id);

                Assert.IsType<NotFoundResult>(result);
            }
        }
        
    }
}
