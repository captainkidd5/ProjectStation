using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Services;

namespace ProjectStation.Pages.ArtCRUD
{
    [Authorize(Roles = "Administrator")]
    public class DeleteModel : PageModel
    {
        private readonly Services.AppDbContext _context;

        public DeleteModel(Services.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ArtPiece ArtPiece { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArtPiece = await _context.ArtPieces.FirstOrDefaultAsync(m => m.Id == id);

            if (ArtPiece == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArtPiece = await _context.ArtPieces.FindAsync(id);

            if (ArtPiece != null)
            {
                _context.ArtPieces.Remove(ArtPiece);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
