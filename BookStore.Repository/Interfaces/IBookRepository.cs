using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Repository.Dtos;
using BookStore.Repository.Helpers;

namespace BookStore.Repository.Interfaces
{
    public interface IBookRepository
    {

        Task<PagedList<BookDto>> GetAllBooksAsync(UserParams userParams);
        Task<BookDto> GetBookByIdAsync(int bookId);
        Task<int> AddBookAsync(AddBookDto addBookDto);
        Task UpdateBookAsync(int bookId, UpdateBookDto addBookDto);
        Task DeleteBookAsync(int bookId);
        Task<PagedList<BookDto>> BooksSearchAsync(UserParams userParams);

    }
}
