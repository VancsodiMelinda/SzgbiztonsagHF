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
            /*
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
            var result = registerModel.OnPostAsync();

            Assert.IsType<PageResult>(result);
            */
            throw new NotImplementedException();
        }

        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenUserCreationSucceeded()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenUserCreatonFailed()
        {
            // ezt egyáltalán lehet tesztelni?
            throw new NotImplementedException();
        }
    }
}
