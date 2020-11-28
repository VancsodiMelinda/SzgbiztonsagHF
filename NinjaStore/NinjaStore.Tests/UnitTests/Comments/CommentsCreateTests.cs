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
using Microsoft.AspNetCore.Identity;

namespace NinjaStore.Tests.UnitTests.Comments.Create
{
    public class CommentsCreateTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static CommentsCreateTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnPostAsync
        [Fact]
        public async Task OnPostAsync_ReturnsPageResult_WhenModelIsInvalid()
        {    
            // mock
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<CreateModel>> mockILogger = new Mock<ILogger<CreateModel>>();

            List<User> usersList = new List<User> { new User() };
            var mockUserManager = MockUserManager<User>(usersList);

            //var yourMockOfUserManager = new Mock<UserManager<User>>();
            //yourMockOfUserManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).Returns(() => null);

            //Mock<IUserStore<User>> mockIUserStore = new Mock<IUserStore<User>>();
            //var userManager = new UserManager(mockIUserStore.Object, null, null, null, null, null, null, null, null);
            //Mock<UserManager<User>> mockUserManager = new Mock<UserManager<User>>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var createModel = new CreateModel(storeLogic, mockUserManager.Object, mockILogger.Object, mockILogger.Object);
                createModel.ModelState.AddModelError("test", "test");
                string fileID = "testFileID";
                string commentText = "this is a new comment";
                var result = await createModel.OnPostAsync(fileID, commentText);

                Assert.IsType<PageResult>(result);
            }
            
            //throw new NotImplementedException();
        }

        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenModelIsInvalid()
        {
            /*
            // mock
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<CreateModel>> mockILogger = new Mock<ILogger<CreateModel>>();
            Mock<UserManager<User>> mockUserManager = new Mock<UserManager<User>>();

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var createModel = new CreateModel(storeLogic, mockUserManager.Object, mockILogger.Object, mockILogger.Object);
            }
            */
            throw new NotImplementedException();
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
    }
}
