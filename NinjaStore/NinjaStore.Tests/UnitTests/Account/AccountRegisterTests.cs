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
using System.Security.Claims;

namespace NinjaStore.Tests.UnitTests.Account
{
    public class AccountRegisterTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static AccountRegisterTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnGet
        [Fact]
        public void OnGet_ReturnsPageResult()
        {
            // mock
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<RegisterModel>> mockILogger = new Mock<ILogger<RegisterModel>>();

            Mock<ClaimsPrincipal> mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            mockHelper.mockSignInManager.Setup(x => x.IsSignedIn(mockClaimsPrincipal.Object)).Returns(true);

            // test
            var registerModel = new RegisterModel(
                mockHelper.mockUserManager.Object,
                mockHelper.mockSignInManager.Object,
                mockILogger.Object
            );

            var result = registerModel.OnGet();

            Assert.IsType<PageResult>(result);
        }

        // METHOD: OnPostAsync
        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenModelIsInvalid()
        {
            //mock
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<RegisterModel>> mockILogger = new Mock<ILogger<RegisterModel>>();

            // test
            var registerModel = new RegisterModel(
                mockHelper.mockUserManager.Object,
                mockHelper.mockSignInManager.Object,
                mockILogger.Object
            );
            registerModel.ModelState.AddModelError("test", "test");
            var result = await registerModel.OnPostAsync();

            Assert.IsType<PageResult>(result);
        }

        
        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenUserCreationSucceeded()
        {
            // mock
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<RegisterModel>> mockILogger = new Mock<ILogger<RegisterModel>>();

            mockHelper.mockSignInManager.Setup(x => x.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            mockHelper.mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mockHelper.mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mockHelper.mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // test
            var registerModel = new RegisterModel(
                mockHelper.mockUserManager.Object,
                mockHelper.mockSignInManager.Object,
                mockILogger.Object)
            {
                Input = new RegisterModel.InputModel {
                    Username = "Meli",
                    Email = "testemail@gmail.com",
                    Password = "Super_SECRETpassword123?",
                    ConfirmPassword = "Super_SECRETpassword123?"
                }
            };

            var result = await registerModel.OnPostAsync();

            Assert.IsType<RedirectToPageResult>(result);
        }

        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenUserCreatonFailed()
        {
            // mock
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<RegisterModel>> mockILogger = new Mock<ILogger<RegisterModel>>();

            mockHelper.mockSignInManager.Setup(x => x.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            mockHelper.mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            mockHelper.mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mockHelper.mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // test
            var registerModel = new RegisterModel(
                mockHelper.mockUserManager.Object,
                mockHelper.mockSignInManager.Object,
                mockILogger.Object)
            {
                Input = new RegisterModel.InputModel {
                    Username = "Meli",
                    Email = "testemail@gmail.com",
                    Password = "Super_SECRETpassword123?",
                    ConfirmPassword = "Super_SECRETpassword123?"
                }
            };

            var result = await registerModel.OnPostAsync();

            Assert.IsType<PageResult>(result);
        }
        
    }
}
