using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NinjaStore.DAL.Models;
using NinjaStore.Pages.Account;
using NinjaStore.Parser.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NinjaStore.Tests.Utilities
{
    public class MockHelper
    {
        public Mock<IParserService> mockIParserService;
        public Mock<ILogger<RegisterModel>> mockILogger;
        public Mock<UserManager<User>> mockUserManager;
        public Mock<SignInManager<User>> mockSignInManager;

        // mock pareser interface, logger interface, user manager, sign in manager
        public MockHelper()
        {
            mockIParserService = new Mock<IParserService>();
            mockILogger = new Mock<ILogger<RegisterModel>>();

            Mock<IUserStore<User>> mockIUserStore = new Mock<IUserStore<User>>();
            mockUserManager = new Mock<UserManager<User>>(
                mockIUserStore.Object,
                null, null, null, null, null, null, null, null
            );

            mockSignInManager = new Mock<SignInManager<User>>(
                mockUserManager.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null, null, null, null
            );
        }
    }
}
