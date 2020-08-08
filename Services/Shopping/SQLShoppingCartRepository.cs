using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Shopping
{
    public class SQLShoppingCartRepository : IShoppingCartRepository
    {
        public string ShoppingCartID { get; set; }
        private readonly AppDbContext appDbContext;

        public const string CartSessionKey = "CartId";

        public SQLShoppingCartRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public CartItem AddItem(int itemID, int quantity)
        {
            ShoppingCartID = GetCartID();
            var cartItem = appDbContext.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartID
            && c.ProductId == itemID);

            if(cartItem == null)
            {
                cartItem = new CartItem()
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = itemID,
                    CartId = ShoppingCartID,
                    Product = appDbContext.Products.SingleOrDefault(
                p => p.ID == itemID),
                    Quantity = 1,
                    DateCreated = DateTime.Now

                };
                appDbContext.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }
            appDbContext.SaveChanges();
            return cartItem;
        }

        public string GetCartID()
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetItems(int itemID)
        {
            throw new NotImplementedException();
        }

        public CartItem RemoveItem(int itemID, int quantity)
        {
            throw new NotImplementedException();
        }

        public double TotalCost()
        {
            throw new NotImplementedException();
        }

        public bool UpdateCartData()
        {
            throw new NotImplementedException();
        }
    }
}
