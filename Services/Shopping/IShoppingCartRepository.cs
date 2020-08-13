using Microsoft.AspNetCore.Http;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Shopping
{
    public interface IShoppingCartRepository
    {
        ShoppingCart CreateCart(string userID, string cartID);
        ShoppingCart GetCart(string id, HttpContext context = null);
        CartItem AddItem(ShoppingCart cart, int productId, int quantity, string userID, HttpContext context = null);
        bool UpdateQuantity(string cardId, int productId, int newQuantity, HttpContext context = null);
        bool RemoveItem(string cartId, int productId, int quantity, HttpContext context = null);
        List<CartItem> GetItems(string cartId, HttpContext context = null);
        double TotalCost(string cartId, IProductRepository productRepository, HttpContext context = null);
        bool UpdateCartData();

    }
}
