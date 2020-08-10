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
    public class ArtCentralHongKong2020Model : PageModel
    {
        public Models.Models.Exhibition Exhibition { get; set; }
        private readonly IArtPieceRepository artPieceRepository;
        public List<ArtPiece> ArtPieces { get; set; }



        public ArtCentralHongKong2020Model(IArtPieceRepository artPieceRepository)
        {
            this.artPieceRepository = artPieceRepository;
            this.Exhibition = Models.Models.Exhibition.ArtCentral2020;
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