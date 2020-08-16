using Microsoft.AspNetCore.Http;
using Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web;

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


        /// <summary>
        /// leave context null if user is signed in.
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="userID"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public CartItem AddItem(ShoppingCart cart, int productId, int quantity, string userID, HttpContext context = null)
        {
            CartItem cartItem = new CartItem()
            {
                CartId = cart.CartId,
                ProductId = productId,
                Quantity = quantity,
                DateCreated = DateTime.Now
            };

            if (context == null)
            {
                appDbContext.ShoppingCartItems.Add(cartItem);
                appDbContext.SaveChanges();
            }
            else
            {
                List<CartItem> cartItems = context.Session.GetObjectFromJson<List<CartItem>>("CartItems");
                if (cartItems == null)
                {
                    cartItems = new List<CartItem>();
                }
                context.Session.SetObjectAsJson("CartItems", cartItems);
                CartItem existingCartItem = cartItems.Find(x => x.ProductId == productId);
                if (existingCartItem != null)
                {
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    cartItems.Add(cartItem);

                }

                context.Session.SetObjectAsJson("CartItems", cartItems);
            }

            return cartItem;
        }

        public ShoppingCart CreateCart(string userID, string cartID)
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            { DateCreated = DateTime.Now, CartId = cartID, UserId = userID };

            appDbContext.ShoppingCarts.Add(shoppingCart);
            appDbContext.SaveChanges();
            return shoppingCart;
        }

        /// <summary>
        /// Leave context as null if user is signed in.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public ShoppingCart GetCart(string id = null, HttpContext context = null)
        {
            if (context != null)
            {
                if (context.Session.Get("ShoppingCart") != null)
                {
                    ShoppingCart cart = context.Session.GetObjectFromJson<ShoppingCart>("ShoppingCart");
                    if (!cart.CheckedOut)
                    {
                        return cart;
                    }
                    else
                    {
                        ShoppingCart shoppingCart = new ShoppingCart()
                        { CartId = Guid.NewGuid().ToString(), DateCreated = DateTime.Now, UserId = null };
                        context.Session.SetObjectAsJson("ShoppingCart", shoppingCart);
                        return shoppingCart;

                    }

                }
                else
                {
                    ShoppingCart shoppingCart = new ShoppingCart()
                    { CartId = Guid.NewGuid().ToString(), DateCreated = DateTime.Now, UserId = null };
                    context.Session.SetObjectAsJson("ShoppingCart", shoppingCart);
                    return shoppingCart;

                }
            }

            if (id == null)
            {
                throw new Exception("Cart does not exist!");
            }
            else
            {
                ShoppingCart cart = appDbContext.ShoppingCarts.FirstOrDefault(x => x.UserId == id);

                if (cart == null)
                {
                    string cartID = Guid.NewGuid().ToString();
                    cart = CreateCart(id, cartID);
                }
                return cart;
            }
        }

        /// <summary>
        /// Leave context null if user is signed in.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public List<CartItem> GetItems(string cartId, HttpContext context = null)
        {
            if (context != null)
            {
                if (context.Session.Get("CartItems") != null)
                {
                    return context.Session.GetObjectFromJson<List<CartItem>>("CartItems");
                }
                else
                {
                    List<CartItem> cartItems = new List<CartItem>();
                    context.Session.SetObjectAsJson("CartItems", cartItems);
                    return cartItems;

                }
            }

            return appDbContext.ShoppingCartItems.Where(x => x.CartId == cartId).ToList();
        }


        public bool UpdateQuantity(string cartId, int productId, int newQuantity, HttpContext context = null)
        {
            List<CartItem> cartItems = new List<CartItem>();
            if (context != null)
            {
                if (context.Session.Get("CartItems") != null)
                {
                    cartItems = context.Session.GetObjectFromJson<List<CartItem>>("CartItems");
                }
                else
                {
                    throw new Exception("Cart items were handed to method null. Should not happen.");

                }
            }
            else
            {
                cartItems = appDbContext.ShoppingCartItems.Where(x => x.CartId == cartId).ToList();
            }

            CartItem cartItem = cartItems.Find(x => x.ProductId == productId);
            if (cartItem != null)
            {
                if (newQuantity == 0)
                {
                    cartItems.Remove(cartItem);
                    appDbContext.ShoppingCartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = newQuantity;
                }

                if (context == null)
                {
                    appDbContext.SaveChanges();
                }
                else
                {
                    context.Session.SetObjectAsJson("CartItems", cartItems);
                }
                return true; //we were able to alter the collection               
            }

            throw new Exception("Tried to remove item which does not exist in collection");

        }

        /// <summary>
        /// Leave context null if user is signed in.
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool RemoveItem(string cartId, int productId, int quantity, HttpContext context = null)
        {
            List<CartItem> cartItems = new List<CartItem>();
            if (context != null)
            {
                if (context.Session.Get("CartItems") != null)
                {
                    cartItems = context.Session.GetObjectFromJson<List<CartItem>>("CartItems");
                }
                else
                {
                    throw new Exception("Cart items were handed to method null. Should not happen.");

                }
            }

            CartItem cartItem = cartItems.Find(x => x.ProductId == productId);
            if (cartItem != null)
            {
                if (cartItem.Quantity >= quantity)
                {
                    cartItem.Quantity -= quantity;
                    if (cartItem.Quantity == 0)
                    {
                        cartItems.Remove(cartItem);
                    }
                    if (context == null)
                    {
                        appDbContext.SaveChanges();
                    }
                    else
                    {
                        context.Session.SetObjectAsJson("CartItems", cartItems);
                    }
                    return true; //we were able to remove the items.
                }
                return false; //tried to remove more items than are in the cart.                     
            }

            throw new Exception("Tried to remove item which does not exist in collection");
            return false; //Cart does not contain item to begin with 



        }

        public float TotalCost(string cartId, IProductRepository productRepository, HttpContext context = null)
        {
            List<CartItem> cartItems = GetItems(cartId, context);
            float cost = 0f;
            foreach (var item in cartItems)
            {
                for (int i = 0; i < item.Quantity; i++)
                {
                    cost += productRepository.GetProduct(item.ProductId).Price;
                }

            }
            return cost;
        }

        public bool UpdateCartData()
        {
            throw new NotImplementedException();
        }

        public bool SetCartToCheckedOut(ShoppingCart cart)
        {
            cart.CheckedOut = true;

            var Cart = appDbContext.ShoppingCarts.Attach(cart);
            Cart.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            appDbContext.SaveChanges();
            return true;
        }
    }
}
