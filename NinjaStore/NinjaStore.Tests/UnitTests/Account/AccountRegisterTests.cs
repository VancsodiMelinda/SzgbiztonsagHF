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
using NinjaStore.Tests.Helper;
using NinjaStore.Parser.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Routing;

namespace NinjaStore.Tests.UnitTests.Account.Register
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
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<RegisterModel>> mockILogger = new Mock<ILogger<RegisterModel>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new UserManager<User>(store.Object, null, null, null, null, null, null, null, null);
            var mockSignInManager = new Mock<SignInManager<User>>(
                userManager,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object
            );

            // test
            var registerModel = new RegisterModel(userManager, mockSignInManager.Object, mockILogger.Object);
            var result = registerModel.OnGet();

            Assert.IsType<PageResult>(result);
        }

        // METHOD: OnPostAsync
        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenModelIsInvalid()
        {
            // mock
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<RegisterModel>> mockILogger = new Mock<ILogger<RegisterModel>>();

            var store = new Mock<IUserStore<User>>();
            var userManager = new UserManager<User>(store.Object, null, null, null, null, null, null, null, null);
            var mockSignInManager = new Mock<SignInManager<User>>(
                userManager,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object
            );

            // test
            var registerModel = new RegisterModel(userManager, mockSignInManager.Object, mockILogger.Object);
            registerModel.ModelState.AddModelError("test", "test");
            var result = await registerModel.OnPostAsync();

            Assert.IsType<PageResult>(result);
        }

        
        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenUserCreationSucceeded()
        {
            // mock
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<RegisterModel>> mockILogger = new Mock<ILogger<RegisterModel>>();

            var mockIUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockIUserStore.Object, null, null, null, null, null, null, null, null);
            var mockSignInManager = new Mock<SignInManager<User>>(
                mockUserManager.Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object
            );

            User user = new User
            {
                UserName = "Meli",
                Email = "testemail@gmail.com",
            };

            mockSignInManager.Setup(x => x.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // test
            var registerModel = new RegisterModel(mockUserManager.Object, mockSignInManager.Object, mockILogger.Object)
            {
                Input = new RegisterModel.InputModel {
                    Username = user.UserName,
                    Email = user.Email,
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
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<RegisterModel>> mockILogger = new Mock<ILogger<RegisterModel>>();

            var mockIUserStore = new Mock<IUserStore<User>>();
            var mockUserManager = new Mock<UserManager<User>>(mockIUserStore.Object, null, null, null, null, null, null, null, null);
            var mockSignInManager = new Mock<SignInManager<User>>(
                mockUserManager.Object,
                new HttpContextAccessor(),
                new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<ILogger<SignInManager<User>>>().Object,
                new Mock<IAuthenticationSchemeProvider>().Object,
                new Mock<IUserConfirmation<User>>().Object
            );

            User user = new User
            {
                UserName = "Meli",
                Email = "testemail@gmail.com",
            };

            mockSignInManager.Setup(x => x.SignInAsync(It.IsAny<User>(), It.IsAny<bool>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            //IdentityError[] errors = new IdentityError[];
            mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            mockUserManager.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);

            // test
            var registerModel = new RegisterModel(mockUserManager.Object, mockSignInManager.Object, mockILogger.Object)
            {
                Input = new RegisterModel.InputModel {
                    Username = user.UserName,
                    Email = user.Email,
                    Password = "Super_SECRETpassword123?",
                    ConfirmPassword = "Super_SECRETpassword123?"
                }
            };

            var result = await registerModel.OnPostAsync();

            Assert.IsType<PageResult>(result);
        }
        
    }
}
