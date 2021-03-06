﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using Services;

namespace ProjectStation.Pages.ArtCRUD
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly Services.AppDbContext _context;

        public CreateModel(Services.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ArtPiece ArtPiece { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ArtPieces.Add(ArtPiece);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
