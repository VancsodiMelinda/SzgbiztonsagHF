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
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace NinjaStore.Tests.UnitTests.Files.Index
{
    public class FilesIndexTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static FilesIndexTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnGetAsync
        [Fact]
        public async Task OnGetAsync_ReturnsPageResult_WhenFilterFilesByFileName()
        {
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<IndexModel>> mockILogger = new Mock<ILogger<IndexModel>>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var indexModel = new IndexModel(storeLogic, mockILogger.Object);
                indexModel.ModelState.SetModelValue("Filter", new ValueProviderResult("test"));
                var result = await indexModel.OnGetAsync();

                Assert.IsType<PageResult>(result);
            }
        }
        
    }
}
