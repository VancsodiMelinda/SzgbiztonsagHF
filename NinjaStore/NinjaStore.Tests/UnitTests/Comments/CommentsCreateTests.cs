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
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace NinjaStore.Tests.UnitTests.Comments
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

            //List<User> usersList = new List<User> { new User() };
            //var mockUserManager = MockUserManager<User>(usersList);

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var createModel = new CreateModel(storeLogic, mockILogger.Object);
                createModel.ModelState.AddModelError("test", "test");
                string fileID = "testFileID";
                string commentText = "this is a new comment";
                var result = await createModel.OnPostAsync(fileID, commentText);

                Assert.IsType<PageResult>(result);
            }
        }
        
        /*
        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenCommentIsAdded()
        {
            // mock
            Mock<IParserService> mockIParserService = new Mock<IParserService>();
            Mock<ILogger<CreateModel>> mockILogger = new Mock<ILogger<CreateModel>>();

            //var identity = new GenericIdentity("generic user");
            //Thread.CurrentPrincipal = new GenericPrincipal(identity, null);

            // add value to User.Identity.Name
            
            User user = new User() { UserName = "Meli" };

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("name", user.UserName),
            };
            var identity = new ClaimsIdentity(claims, "Test");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var mockPrincipal = new Mock<IPrincipal>();
            mockPrincipal.Setup(x => x.Identity).Returns(identity);
            mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.User).Returns(claimsPrincipal);

            Mock<UserManager<User>> userMgr = new Mock<UserManager<User>>();
            userMgr.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
            

            

            // test
            using (var context = new StoreContext(options))
            {
                StoreLogic storeLogic = new StoreLogic(context, mockIParserService.Object);
                var createModel = new CreateModel(storeLogic, mockILogger.Object);

                

                string fileID = "testFileID";
                string commentText = "this is a new comment";
                var result = await createModel.OnPostAsync(fileID, commentText);

                Assert.IsType<RedirectToPageResult>(result);
            }
        }
        */

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
