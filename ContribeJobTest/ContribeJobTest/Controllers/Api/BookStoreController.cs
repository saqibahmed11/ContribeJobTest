using ContribeJobTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ContribeJobTest.Controllers.Api
{
    public class BookStoreController : ApiController
    {
        [HttpGet]
        public dynamic GetAll() //API Endpoint to return all books
        {
            BookStore bookStore = new BookStore();
            bookStore.AddDummyData();
            return bookStore.GetAll();
        }

        [HttpGet]
        public async Task<dynamic> GetBooks(string searchString) //API Endpoint to return searched books 
        {
            BookStore bookStore = new BookStore();
            bookStore.AddDummyData();
            return await bookStore.GetBooksAsync(searchString);
        }

        [HttpPost]
        public dynamic PlaceOrder(IEnumerable<Cart> cart) //API Endpoint to return placed order in cart
        {
            BookStore bookStore = new BookStore();
            bookStore.AddDummyData();
            return Cart.PlaceOrder(cart, bookStore);
        }
    }
}
