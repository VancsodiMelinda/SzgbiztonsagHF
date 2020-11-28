﻿using System;
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

namespace NinjaStore.Tests.UnitTests.Comments.Delete
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
            throw new NotImplementedException();
        }

        [Fact]
        public async Task OnPostAsync_ReturnsNotFoundResult_WhenCommentIsNull()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public async Task OnPostAsync_ReturnsRedirectToPageResult_WhenCommentIsDeleted()
        {
            throw new NotImplementedException();
        }
    }
}