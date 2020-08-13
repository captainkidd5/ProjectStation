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
        CartItem RemoveItem(int itemID, int quantity);
        List<CartItem> GetItems(string cartId, HttpContext context = null);
        double TotalCost();
        bool UpdateCartData();

    }
}
