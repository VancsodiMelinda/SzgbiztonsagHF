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

namespace NinjaStore.Tests.UnitTests.Files.Upload
{
    public class FilesUploadTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static FilesUploadTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnPostAsync
        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenModelStateIsInvalid()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<UploadModel>> mockILogger = new Mock<ILogger<UploadModel>>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var uploadModel = new UploadModel(storeLogic, mockILogger.Object, mockILogger.Object);
                uploadModel.ModelState.AddModelError("test", "test");
                var result = await uploadModel.OnPostAsync();

                Assert.IsType<PageResult>(result);
            }
        }

        /*
        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenModelStateIsInvalid()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var uploadModel = new UploadModel(storeLogic);

                var result = await uploadModel.OnPostAsync();

                Assert.IsType<RedirectToPageResult>(result);
            }
        }
        */
    }
}
