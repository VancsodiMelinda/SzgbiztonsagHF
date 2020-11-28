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
    public class AccountLoginTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static AccountLoginTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnPostAsync
        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenModelIsInvalid()
        {
            //mock
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<LoginModel>> mockILogger = new Mock<ILogger<LoginModel>>();

            // test
            var loginModel = new LoginModel(
                mockHelper.mockSignInManager.Object,
                mockILogger.Object
            );
            loginModel.ModelState.AddModelError("test", "test");
            var result = await loginModel.OnPostAsync();

            Assert.IsType<PageResult>(result);
        }

        
        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenSignInSucceeded()
        {
            // mock
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<LoginModel>> mockILogger = new Mock<ILogger<LoginModel>>();

            mockHelper.mockSignInManager.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Success));

            // test
            var loginModel = new LoginModel(
                mockHelper.mockSignInManager.Object,
                mockILogger.Object)
            {
                Username = "Meli",
                Password = "Super_SECRETpassword123?",
            };

            var result = await loginModel.OnPostAsync();

            Assert.IsType<RedirectToPageResult>(result);
        }
        
    }
}
