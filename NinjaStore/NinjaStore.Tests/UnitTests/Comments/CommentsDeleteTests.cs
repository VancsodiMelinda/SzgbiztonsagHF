using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;
using NinjaStore.DAL;
using NinjaStore.BLL;
using Moq;
using System.Threading.Tasks;
using NinjaStore.Pages.Comments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.DAL.Models;
using NinjaStore.Tests.Helper;
using NinjaStore.Parser.Services;
using Microsoft.Extensions.Logging;
using NinjaStore.Tests.Utilities;
using System.Security.Principal;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Identity;

namespace NinjaStore.Tests.UnitTests.Comments
{
    public class CommentsDeleteTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static CommentsDeleteTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        
        // METHOD: OnPostAsync
        [Fact]
        public async Task OnPostAsync_ReturnsNotFoundResult_WhenIdIsNull()
        {
            // mock parser service interface
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<DeleteModel>> mockILogger = new Mock<ILogger<DeleteModel>>();

            Mock<ClaimsPrincipal> mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            mockHelper.mockUserManager.Setup(x => x.GetUserAsync(mockClaimsPrincipal.Object)).Returns(Task.FromResult(new User() { }));
            mockHelper.mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockHelper.mockIParserService.Object);
                var deleteModel = new DeleteModel(mockHelper.mockUserManager.Object, storeLogic, mockILogger.Object);
                int? id = null;
                var result = await deleteModel.OnPostAsync(id);

                Assert.IsType<NotFoundResult>(result);
            }
        }
        
        [Fact]
        public async Task OnPostAsync_ReturnsNotFoundResult_WhenCommentIsNull()
        {
            // mock parser service interface
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<DeleteModel>> mockILogger = new Mock<ILogger<DeleteModel>>();

            Mock<ClaimsPrincipal> mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            mockHelper.mockUserManager.Setup(x => x.GetUserAsync(mockClaimsPrincipal.Object)).Returns(Task.FromResult(new User() { }));
            mockHelper.mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockHelper.mockIParserService.Object);
                var deleteModel = new DeleteModel(mockHelper.mockUserManager.Object, storeLogic, mockILogger.Object);
                int id = 72;
                var result = await deleteModel.OnPostAsync(id);

                Assert.IsType<NotFoundResult>(result);
            }
        }
        
        /*
        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenCommentIsDeleted()
        {
            // mock parser service interface
            MockHelper mockHelper = new MockHelper();
            Mock<ILogger<DeleteModel>> mockILogger = new Mock<ILogger<DeleteModel>>();

            Mock<ClaimsPrincipal> mockClaimsPrincipal = new Mock<ClaimsPrincipal>();
            mockHelper.mockUserManager.Setup(x => x.GetUserAsync(mockClaimsPrincipal.Object)).Returns(Task.FromResult(new User() { }));
            mockHelper.mockUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockHelper.mockIParserService.Object);
                var deleteModel = new DeleteModel(mockHelper.mockUserManager.Object, storeLogic, mockILogger.Object);
                int id = 1;
                var result = await deleteModel.OnPostAsync(id);

                Assert.IsType<RedirectToPageResult>(result);
            }
        }
        */
    }
}
