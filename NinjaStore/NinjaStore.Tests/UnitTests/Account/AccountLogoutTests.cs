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
    public class AccountLogoutTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static AccountLogoutTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnPostAsync
        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenSignedOut()
        {
            //mock
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<LogoutModel>> mockILogger = new Mock<ILogger<LogoutModel>>();

            mockHelper.mockSignInManager.Setup(x => x.SignOutAsync()).Returns(Task.CompletedTask);

            // test
            var logoutModel = new LogoutModel(
                mockHelper.mockSignInManager.Object,
                mockILogger.Object
            );
            logoutModel.ModelState.AddModelError("test", "test");
            var result = await logoutModel.OnPostAsync();

            Assert.IsType<RedirectToPageResult>(result);
        }
    }
}
