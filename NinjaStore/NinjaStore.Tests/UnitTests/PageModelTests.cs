﻿using System;
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

namespace NinjaStore.Tests.UnitTests.What
{
    public class DeletePageTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static DeletePageTests()
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
            var deleteModel = new DeleteModel(mockIStoreLogic.Object);

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

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var deleteModel = new DeleteModel(storeLogic);
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

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var deleteModel = new DeleteModel(storeLogic);
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
            var deleteModel = new DeleteModel(mockIStoreLogic.Object);

            string id = null;
            var result = await deleteModel.OnPostAsync(id);

            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenFileIsDeleted()
        {
            
            Mock<IStoreLogic> mockIStoreLogic = new Mock<IStoreLogic>();
            var deleteModel = new DeleteModel(mockIStoreLogic.Object);

            string id = "someID";
            var result = await deleteModel.OnPostAsync(id);

            Assert.IsType<RedirectToPageResult>(result);
            
            /*
            // mock parser service interface
            Mock<IParserService> mockIParserService = new Mock<IParserService>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var deleteModel = new DeleteModel(storeLogic);
                string id = "testFileID";
                var result = await deleteModel.OnPostAsync(id);

                Assert.IsType<RedirectToPageResult>(result);
            }
            */
        }
    }

    public class DetailsPageTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static DetailsPageTests()
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

    public class DownloadPageTests
    {
        [Fact]
        public async Task OnGetAsync_ReturnsNotFoundResult_WhenIdIsNullOrWhiteSpace()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task OnGetAsync_ReturnsFileResult_WhenFileExists()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task OnGetAsync_ReturnsNotFoundResult_WhenFileDoesNotExists()
        {
            throw new NotImplementedException();
        }
    }

    public class IndexPageTests
    {

    }

    public class UploadPageTests
    {

    }
}

namespace NinjaStore.Tests.UnitTests.Comments
{
    public class CreatePageTests
    {

    }

    public class DeletePageTests
    {

    }
}