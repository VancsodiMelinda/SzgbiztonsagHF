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
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NinjaStore.Tests.UnitTests.Account
{
    public class AccountDetailsAdminTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static AccountDetailsAdminTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnGetAsync
        [Fact]
        public async Task OnGetAsync_ReturnsPageResult_WhenAdminGetsDetails()
        {
            //mock
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<DetailsModel>> mockILogger = new Mock<ILogger<DetailsModel>>();

            User user = new User()
            {
                UserName = "Meli",
                Email = "testemail@gmail.com",
            };
            mockHelper.mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user)).Verifiable();

            // test
            var detailsAdminModel = new DetailsAdminModel(
                mockHelper.mockUserManager.Object,
                mockILogger.Object
            )
            {
                Input = new DetailsAdminModel.InputModel
                {
                    Username = "Meli",
                    Email = "testemail@gmail.com",
                    NewPassword = "Super_SECRETnewPassword123?",
                    ConfirmPassword = "Super_SECRETnewPassword123?"
                }
            };

            string id = "Meli";
            var result = await detailsAdminModel.OnGetAsync(id);

            Assert.IsType<PageResult>(result);
        }

        // METHOD: OnPostAsync
        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenPasswordChangeSucceeded()
        {
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<DetailsModel>> mockILogger = new Mock<ILogger<DetailsModel>>();
            Mock<ITempDataDictionary> mockITempDataDictionary = new Mock<ITempDataDictionary>();

            User user = new User()
            {
                UserName = "Meli",
                Email = "testemail@gmail.com",
            };
            mockHelper.mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user)).Verifiable();
            mockHelper.mockUserManager.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            mockHelper.mockUserManager.Setup(x => x.RemovePasswordAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            mockHelper.mockUserManager.Setup(x => x.AddPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            // test
            var detailsAdminModel = new DetailsAdminModel(
                mockHelper.mockUserManager.Object,
                mockILogger.Object
            )
            {
                Input = new DetailsAdminModel.InputModel
                {
                    Username = "Meli",
                    Email = "testemail@gmail.com",
                    NewPassword = "Super_SECRETnewPassword123?",
                    ConfirmPassword = "Super_SECRETnewPassword123?"
                },
                TempData = mockITempDataDictionary.Object
            };

            var result = await detailsAdminModel.OnPostAsync();

            Assert.IsType<PageResult>(result);
        }
        
    }
}
