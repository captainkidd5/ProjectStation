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
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly Services.AppDbContext _context;

        public IndexModel(Services.AppDbContext context)
        {
            _context = context;
        }

        public IList<ArtPiece> ArtPiece { get;set; }

        public async Task OnGetAsync()
        {
            ArtPiece = await _context.ArtPieces.ToListAsync();
        }
    }
}
