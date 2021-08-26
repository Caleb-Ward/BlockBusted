using MovieRentals_20020992.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRentals_20020992.Models
{
    public class Cart
    {
        private string CartID { get; set; }
        private const string CartSessionKey = "CartID";
        private StoreContext db = new StoreContext();
        private string GetCartID()
        {
            if (HttpContext.Current.Session[CartSessionKey] == null)
            {
                if
               (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[CartSessionKey] =
                   HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();

                HttpContext.Current.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[CartSessionKey].ToString();
        }
        public static Cart GetCart()
        {
            Cart cart = new Cart();
            cart.CartID = cart.GetCartID();
            return cart;
        }
        public void AddToCart(int movieID, int rentTime)
        {
            var cartLine = db.CartLines.FirstOrDefault(c => c.CartID == CartID && c.MovieID == movieID);
            if (cartLine == null)
            {
                cartLine = new CartLine
                {
                    MovieID = movieID,
                    CartID = CartID,
                    RentTime = rentTime,
                    DateCreated = DateTime.Now
                };
                db.CartLines.Add(cartLine);
            }
            else
            {
                cartLine.RentTime = rentTime;
            }
            db.SaveChanges();
        }
        public void RemoveLine(int movieID)
        {
            var cartLine = db.CartLines.FirstOrDefault(c => c.CartID == CartID
           && c.MovieID == movieID);
            if (cartLine != null)
            {
                db.CartLines.Remove(cartLine);
            }
            db.SaveChanges();
        }
        public void UpdateCart(List<CartLine> lines)
        {
            foreach (var line in lines)
            {
                var cartLine = db.CartLines.FirstOrDefault(c => c.CartID ==
               CartID && c.MovieID == line.MovieID);
                if (cartLine != null)
                {
                    if (line.RentTime == 0)
                    {
                        RemoveLine(line.MovieID);
                    }
                    else
                    {

                    cartLine.RentTime = line.RentTime;
                    }
                }
            }
            db.SaveChanges();
        }
        public void EmptyCart()
        {
            var cartLines = db.CartLines.Where(c => c.CartID == CartID);
            foreach (var cartLine in cartLines)
            {
                db.CartLines.Remove(cartLine);
            }
            db.SaveChanges();
        }
        public List<CartLine> GetCartLines()
        {
            return db.CartLines.Where(c => c.CartID == CartID).ToList();
        }
        public decimal GetTotalCost()
        {
            decimal cartTotal = decimal.Zero;
            if (GetCartLines().Count > 0)
            {
                cartTotal = db.CartLines.Where(c => c.CartID == CartID).Sum(c
               => c.Movie.Price * c.RentTime);
            }
            return cartTotal;
        }
        public int GetNumberOfWeeks()
        {
            int numberOfWeeks = 0;
            if (GetCartLines().Count > 0)
            {
                numberOfWeeks = db.CartLines.Where(c => c.CartID ==
               CartID).Sum(c => c.RentTime);
            }
            return numberOfWeeks;
        }
        public int GetNumberOfItems()
        {
            int numberOfItems = 0;
            if (GetCartLines().Count > 0)
            {
                numberOfItems = db.CartLines.Where(c => c.CartID ==
               CartID).Count();
            }
            return numberOfItems;
        }
        public void MigrateCart(string userName)
        {
            //find the current cart and store it in memory using ToList()
            var cart = db.CartLines.Where(c => c.CartID == CartID).ToList();
            //find if the user already has a cart or not and store it in memory
             var usersCart = db.CartLines.Where(c => c.CartID == userName).ToList();
            //if the user has a cart then add the current rent time to it
            if (usersCart != null)
            {
                //set the CartID to the userName
                string prevID = CartID;
                CartID = userName;
                //add the lines in anonymous cart to the user's cart
                foreach (var line in cart)
                {

                AddToCart(line.MovieID, line.RentTime);
                }
                //delete the lines in the anonymous cart from the database
                CartID = prevID;
                EmptyCart();
            }
            else
            {
                foreach (var cartLine in cart)
                {
                    cartLine.CartID = userName;
                }
                db.SaveChanges();
            }
            HttpContext.Current.Session[CartSessionKey] = userName;
        }
        public decimal CreateOrderLines(int orderID)
        {
            decimal orderTotal = 0;
            var cartLines = GetCartLines();
            foreach (var item in cartLines)
            {
                OrderLine orderLine = new OrderLine
                {
                    Movie = item.Movie,
                    MovieID = item.MovieID,
                    MovieTitle = item.Movie.Title,
                    RentTime = item.RentTime,
                    WeeklyPrice = item.Movie.Price,
                    OrderID = orderID
                };
                orderTotal += (item.RentTime * item.Movie.Price);
                db.OrderLines.Add(orderLine);
            }
            db.SaveChanges();
            EmptyCart();
            return orderTotal;
        }

    }
}
