using ContribeJobTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ContribeJobTest.Models
{
    public class BookStore : IBookstoreService
    {
        List<IBook> Books = new List<IBook>();

        public void AddDummyData() //Adding data objects
        {
            Book b = new Book("Mastering åäö", "Average Swede", 762, 15);
            Book b1 = new Book("How To Spend Money", "Rich Block", 1000000, 1);
            Book b2 = new Book("Generic Title", "First Author", 185.5F, 5);
            Book b3 = new Book("Generic Title", "Second Author", 1748, 3);
            Book b4 = new Book("Random Sales", "Cunning Bastard", 999, 20);
            Book b5 = new Book("Random Sales", "Cunning Bastard", 499.5F, 3);
            Book b6 = new Book("Desired", "Rich Bloke", 564.5F, 0);

            Books.Add(b);
            Books.Add(b1);
            Books.Add(b2);
            Books.Add(b3);
            Books.Add(b4);
            Books.Add(b5);
            Books.Add(b6);
        }

        public List<IBook> GetAll() //Display all books
        {
            return Books;
        }

        public Task<IEnumerable<IBook>> GetBooksAsync(string searchString) //Performing search on basis of Author or Title
        {
            searchString = searchString.ToLower();
            var result = Books.Where(i => i.Author.ToLower().Contains(searchString) || i.Title.ToLower().Contains(searchString));
            return Task.FromResult(result);
        }

    }
}