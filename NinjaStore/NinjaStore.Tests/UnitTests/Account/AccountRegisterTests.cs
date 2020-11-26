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
        public async Task OnGet_ReturnsPageResult()
        {
            /*
            Mock<IStoreLogic> mockIStoreLogic = new Mock<IStoreLogic>();
            var registerModel = new RegisterModel(mockIStoreLogic.Object);

            string id = null;
            var result = await deleteModel.OnGetAsync(id);

            Assert.IsType<NotFoundResult>(result);
            */

            throw new NotImplementedException();
        }

        // METHOD: OnPostAsync
        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenModelIsInvalid()
        {
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
