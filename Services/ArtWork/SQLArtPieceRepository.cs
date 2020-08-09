using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.ArtWork
{
    public class SQLArtPieceRepository : IArtPieceRepository
    {
        private readonly AppDbContext context;

        public SQLArtPieceRepository(AppDbContext context)
        {
            this.context = context;
        }

        public ArtPiece AddArtPiece(ArtPiece artPiece)
        {
            throw new NotImplementedException();
        }

        public ArtPiece Delete(int id)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<ArtPiece> GetAllArtPieces()
        {
            return context.ArtPieces
                .FromSqlRaw<ArtPiece>("SELECT * FROM ArtPieces")
                .ToList();
        }

        public ArtPiece GetArtPiece(int id)
        {
            return context.ArtPieces.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<ArtPiece> GetExhibitionPieces(Exhibition exhibition)
        {
            return context.ArtPieces.Where(x => x.Exhibition == exhibition);
        }

        public ArtPiece Update(ArtPiece artPiece)
        {
            throw new NotImplementedException();
        }
    }
}
