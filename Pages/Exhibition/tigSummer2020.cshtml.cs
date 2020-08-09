using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Models;
using Services.ArtWork;

namespace ProjectStation.Pages.Exhibition
{
    public class TigSummer2020Model : PageModel
    {
        public Models.Models.Exhibition Exhibition { get; set; }
        private readonly IArtPieceRepository artPieceRepository;
        public List<ArtPiece> ArtPieces { get; set; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        public ArtPiece[,] ArtArray { get; set; }

        public TigSummer2020Model(IWebHostEnvironment webHostEnvironment, IArtPieceRepository artPieceRepository)
        {
            this.WebHostEnvironment = webHostEnvironment;
            this.artPieceRepository = artPieceRepository;
            this.Exhibition = Models.Models.Exhibition.IamBecome;
        }

        public void OnPost()
        {

        }

        public void OnGet()
        {
            this.ArtPieces = artPieceRepository.GetExhibitionPieces(this.Exhibition).ToList();

            ArtArray = new ArtPiece[ArtPieces.Count / 2,2];

            foreach(ArtPiece piece in ArtPieces)
            {
                piece.PhotoPath = "~/SiteAssets/exhibitions/" + piece.PhotoPath;
            }

            for(int i =0; i < ArtArray.GetLength(0); i++)
            {
                for(int j =0; j < ArtArray.GetLength(1);j++)
                {
                    if(j ==0)
                    {
                        ArtArray[i, j] = ArtPieces[i];
                    }
                    else if(i < ArtPieces.Count)
                    {
                        ArtArray[i, j] = ArtPieces[i + 1];
                    }
                }
            }
        }
    }
}