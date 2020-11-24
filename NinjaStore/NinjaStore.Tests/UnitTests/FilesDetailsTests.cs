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

namespace NinjaStore.Tests.UnitTests.Files.Details
{
    public class FilesDetailsTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static FilesDetailsTests()
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
            var detailsModel = new DetailsModel(mockIStoreLogic.Object);

            string id = null;
            var result = await detailsModel.OnGetAsync(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGetAsync_ReturnsNotFoundResult_WhenCaffMetadataIsNull()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var detailsModel = new DetailsModel(storeLogic);
                string id = "notExistingID";
                var result = await detailsModel.OnGetAsync(id);

                Assert.IsType<NotFoundResult>(result);
            }
        }

        [Fact]
        public async Task OnGetAsync_ReturnsPageResult_WhenCaffMetadataIsNotNull()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var detailsModel = new DetailsModel(storeLogic);
                string id = "testFileID";
                var result = await detailsModel.OnGetAsync(id);

                Assert.IsType<PageResult>(result);
            }
        }
    }
}
