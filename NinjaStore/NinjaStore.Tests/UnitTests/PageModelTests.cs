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

namespace NinjaStore.Tests.UnitTests.Files
{
    public class DeletePageTests
    {
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
            Mock<IStoreLogic> mockIStoreLogic = new Mock<IStoreLogic>();
            var deleteModel = new DeleteModel(mockIStoreLogic.Object);

            string id = "notExistingID";
            var result = await deleteModel.OnGetAsync(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task OnGetAsync_ReturnsPageResult_WhenCaffMetadataExistsWithId()
        {
            throw new NotImplementedException();
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
        }

    }

    public class DetailsPageTests
    {

    }

    public class DownloadPageTests
    {

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