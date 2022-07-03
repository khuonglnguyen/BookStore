using BookStore.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookStore.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private DbCon _context;
        public BookRepository(DbCon context)
        {
            _context = context;

            if (!_context.Books.Any())
            {

                _context.Books.Add(new Book
                {
                    Name = "Tom and Jerry",
                    Price = 123.5,
                    Author = "Unknow",
                    Publisher = "Kim Dong"
                });

                _context.Books.Add(new Book
                {
                    Name = "Steve Job",
                    Price = 200,
                    Author = "Unknow",
                    Publisher = "BBC"
                });

                _context.Books.Add(new Book
                {
                    Name = "Golden Axe",
                    Price = 199,
                    Author = "David Copy and Paste",
                    Publisher = "SEGA"
                });
                _context.SaveChanges();
            }
        }
        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBookDetails(int bookId)
        {
            return _context.Books.FirstOrDefault(x => x.Id == bookId);
        }
    }
    public interface IBookRepository
    {
        void AddBook(Book book);
        Book GetBookDetails(int bookId);
        IEnumerable<Book> GetAllBooks();
    }
}
