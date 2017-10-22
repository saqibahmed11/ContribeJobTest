using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContribeJobTest.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using ContribeJobTest.Interfaces;
using System.Linq;

namespace ContribeJobTest.Test
{
    [TestClass]
    public class BookStoreTest
    {
        BookStore book = new BookStore();

        [TestInitialize]
        public void SetupTest()
        {
            book.AddDummyData();
        }

        [TestMethod]
        public async Task SearchString()
        {
            IEnumerable<IBook> result = await book.GetBooksAsync("author");
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void OneItemWithFullOrdersPlacedAsTheyAreInStockAndOneItemWithNotFullOrderPlaceAsTheyAreNotInStock()
        {
            List<Cart> carts = new List<Cart>();

            carts.Add(new Cart
            {
                author = book.GetAll()[0].Author,
                price = book.GetAll()[0].Price,
                title = book.GetAll()[0].Title,
                amount = book.GetAll()[0].InStock + 1,
            });

            carts.Add(new Cart
            {
                author = book.GetAll()[1].Author,
                price = book.GetAll()[1].Price,
                title = book.GetAll()[1].Title,
                amount = book.GetAll()[1].InStock,
            });

            var data = Cart.PlaceOrder(carts, book);
            Assert.AreEqual(1, data.Where(i => i.fullOrderPlaced).Count());
            Assert.AreEqual(1, data.Where(i => !i.fullOrderPlaced).Count());
        }
    }
}
