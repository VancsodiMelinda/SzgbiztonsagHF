using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;
using NinjaStore.DAL;
using NinjaStore.BLL;
using Moq;
using System.Threading.Tasks;
using NinjaStore.Pages.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.DAL.Models;
using Microsoft.AspNetCore.Identity;
using NinjaStore.Tests.Utilities;
using Microsoft.Extensions.Logging;

namespace NinjaStore.Tests.UnitTests.Account
{
    public class AccountListTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static AccountListTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnGetAsync
        /*
        [Fact]
        public async Task OnGetAsync_ReturnsPageResult_WhenListIsCreated()
        {
            //mock
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<ListModel>> mockILogger = new Mock<ILogger<ListModel>>();

            // can't figure out what's the return
            List<User> userList = new List<User>();
            mockHelper.mockUserManager.Setup(x => x.GetUsersInRoleAsync(It.IsAny<string>()))
                .ReturnsAsync(Task.FromResult(userList));

            // test
            var listModel = new ListModel(
                mockHelper.mockUserManager.Object,
                mockILogger.Object
            );

            var result = await listModel.OnGetAsync();

            Assert.IsType<PageResult>(result);
        }
        */
    }
}
