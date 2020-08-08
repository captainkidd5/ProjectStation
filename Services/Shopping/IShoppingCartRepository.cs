﻿using Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Shopping
{
    public interface IShoppingCartRepository
    {
        string GetCartID();
        CartItem AddItem(int itemID, int quantity);
        CartItem RemoveItem(int itemID, int quantity);
        List<CartItem> GetItems(int itemID);
        double TotalCost();
        bool UpdateCartData();

    }
}
