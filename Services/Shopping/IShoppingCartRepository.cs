﻿using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Shopping
{
    public interface IShoppingCartRepository
    {
        ShoppingCart CreateCart(string userID, string cartID);
        ShoppingCart GetCart(string id);
        CartItem AddItem(int itemID, int quantity);
        CartItem RemoveItem(int itemID, int quantity);
        List<CartItem> GetItems(string cartId);
        double TotalCost();
        bool UpdateCartData();

    }
}
