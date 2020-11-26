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
            throw new NotImplementedException();
        }

        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenModelIsInvalid()
        {
            throw new NotImplementedException();
        }
    }
}
