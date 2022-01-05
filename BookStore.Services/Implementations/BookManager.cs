using System;
using BookStore.Repository.Interfaces;

namespace BookStore.Services.Implementations
{
    public class BookManager 
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
    }
}
