﻿using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvcUi.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext _db)
        {
            this._db = _db;
        }

        public async Task<IEnumerable<Genre>> LoadGenres()
        {
            return await _db.Genres.ToListAsync();
        }

        public async Task<IEnumerable<Book>> DisplayBooks(string sTerm = "", int genreId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Book> books = await (from book in _db.Books
                                             join genre in _db.Genres
                                             on book.GenreId equals genre.Id
                                             join stock in _db.Stocks
                                             on book.Id equals stock.BookId
                                             into book_stocks
                                             from bookWithStock in book_stocks.DefaultIfEmpty()
                                             where string.IsNullOrWhiteSpace(sTerm) || (book != null && book.BookName.ToLower().StartsWith(sTerm))
                                             select new Book
                                             {
                                                 Id = book.Id,
                                                 Image = book.Image,
                                                 AuthorName = book.AuthorName,
                                                 BookName = book.BookName,
                                                 GenreId = book.GenreId,
                                                 Price = book.Price,
                                                 GenreName = genre.GenreName,
                                                 Quantity = bookWithStock == null ? 0 : bookWithStock.Quantity
                                             }
                         ).ToListAsync();
            if (genreId > 0)
            {
                books = books.Where(a => a.GenreId == genreId).ToList();
            }
            return books;
        }
    }
}
