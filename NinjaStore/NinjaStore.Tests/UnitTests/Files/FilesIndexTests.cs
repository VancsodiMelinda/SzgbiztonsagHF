using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.EntityFrameworkCore;
using NinjaStore.DAL;
using NinjaStore.BLL;
using Moq;
using System.Threading.Tasks;
using NinjaStore.Pages.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.DAL.Models;
using NinjaStore.Tests.Helper;
using NinjaStore.Parser.Services;

namespace NinjaStore.Tests.UnitTests.Files.Index
{
    public class FilesIndexTests
    {
        static DbContextOptions<StoreContext> options { get; set; }
        static CaffFile testData;

        static FilesIndexTests()
        {
            Helper.Helper helper = new Helper.Helper();
            options = helper.options;
            testData = helper.testData;
        }

        // METHOD: OnGetAsync
        /*
        // the OnGetAsync method doesn't return anything
        [Fact]
        public async Task OnGetAsync_NoReturn()
        {

        }
        */
    }
}
