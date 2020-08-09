using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.ArtWork
{
   
    public interface IArtPieceRepository
    {
        IEnumerable<ArtPiece> GetExhibitionPieces(Exhibition exhibition);
        IEnumerable<ArtPiece> GetAllArtPieces();
        ArtPiece GetArtPiece(int id);
        ArtPiece Update(ArtPiece artPiece);
        ArtPiece AddArtPiece(ArtPiece artPiece);
        ArtPiece Delete(int id);
    }
}
