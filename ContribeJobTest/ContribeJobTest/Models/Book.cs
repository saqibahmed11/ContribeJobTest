using ContribeJobTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContribeJobTest.Models
{
    public class Book : IBook
    {
        public Book(string title, string author, float price, int inStock)
        {
            Title = title;
            Author = author;
            Price = price;
            InStock = inStock;
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public float Price { get; set; }

        public int InStock { get; set; }
    }
}