using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContribeJobTest.Models
{
    public class Cart
    {
        public string title { get; set; }

        public string author { get; set; }

        public string message { get; set; }

        public float amount { get; set; }

        public float price { get; set; }

        public bool fullOrderPlaced { get; set; }


        public static List<Cart> PlaceOrder(IEnumerable<Cart> cart, BookStore bookStore) //Placing Order in Cart
        {
            List<Cart> result = new List<Cart>();

            foreach (Cart c in cart)
            {
                Book book = bookStore.GetAll().Where(i => i.Title == c.title && i.Author == c.author).FirstOrDefault() as Book;
                if ((book.InStock - c.amount) > -1)
                {
                    c.message = String.Format("You ordered {0} copy(s). Order is placed successfully.", c.amount);
                    c.fullOrderPlaced = true;
                }
                else
                {
                    c.message = String.Format("You ordered {0} copy(s). Order is placed successfully for {1} copy(s) because more than that are not available.", c.amount, book.InStock);
                    c.amount = book.InStock;
                    c.fullOrderPlaced = false;
                }
                result.Add(c);
            }
            return result;
        }
    }
}