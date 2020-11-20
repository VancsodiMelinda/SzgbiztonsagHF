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

namespace NinjaStore.Tests.UnitTests
{
    public class DeletePageTests
    {
        [Fact]
        public async Task OnGetAsync_ReturnsNotFound_WhenIdIsNull()
        {
            // mock store logic interface
            Mock<IStoreLogic> mockIStoreLogic = new Mock<IStoreLogic>();

            var deleteModel = new DeleteModel(mockIStoreLogic.Object);

            string id = "valami";
            var result = await deleteModel.OnGetAsync(id);

            Assert.IsType<NotFoundResult>(result);
        }

        /*
        [Fact]
        public async Task OnGetAsync_ReturnsNotFound_WhenCaffMetadataIsNull()
        {
            //var optionsBuilder = new DbContextOptionsBuilder<StoreContext>().UseInMemoryDatabase("InMemoryDb");

            //var mockAppDbContext = new Mock<StoreContext>(optionsBuilder.Options);
        }
        */
    }

    public class DetailsPageTests
    {

    }

    public class IndexPageTests
    {

    }

    public class UploadPageTests
    {

    }
}
