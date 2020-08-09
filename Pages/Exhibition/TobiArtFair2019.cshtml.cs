using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using Services.ArtWork;

namespace ProjectStation.Pages.Exhibition
{
    public class TobiArtFair2019Model : PageModel
    {

            public Models.Models.Exhibition Exhibition { get; set; }
            private readonly IArtPieceRepository artPieceRepository;
            public List<ArtPiece> ArtPieces { get; set; }



            public TobiArtFair2019Model( IArtPieceRepository artPieceRepository)
            {
                this.artPieceRepository = artPieceRepository;
                this.Exhibition = Models.Models.Exhibition.TobiArtFair2019;
            }

            public void OnPost()
            {

            }

            public void OnGet()
            {
                this.ArtPieces = artPieceRepository.GetExhibitionPieces(this.Exhibition).ToList();


                foreach (ArtPiece piece in ArtPieces)
                {
                    piece.PhotoPath = "~/SiteAssets/exhibitions/" + piece.PhotoPath;
                }


            }
        
    }
}