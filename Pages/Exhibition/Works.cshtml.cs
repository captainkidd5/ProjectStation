using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.ArtWork;

namespace ProjectStation.Pages.Exhibition
{
    public class WorksModel : PageModel
    {
        private readonly IArtPieceRepository artPieceRepository;

        public WorksModel(IArtPieceRepository artPieceRepository)
        {
            this.artPieceRepository = artPieceRepository;
        }
        public void OnGet()
        {

        }
    }
}