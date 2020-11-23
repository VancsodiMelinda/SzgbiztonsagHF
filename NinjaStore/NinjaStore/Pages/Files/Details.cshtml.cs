﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NinjaStore.BLL;
using NinjaStore.DAL.Models;

namespace NinjaStore.Pages.Files
{
    public class DetailsModel : PageModel
    {
        private readonly IStoreLogic _logic;

        public DetailsModel(IStoreLogic logic)
        {
            _logic = logic;
        }

        public CaffMetadata CaffMetadata { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CaffMetadata = await _logic.GetMetadataWithCommentsAsync(id);
            if (CaffMetadata == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
