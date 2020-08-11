using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Services
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
 
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<NewsSnippet> NewsSnippets { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<CartItem> ShoppingCartItems { get; set; }
        public DbSet<ArtPiece> ArtPieces { get; set; }



    }
}
