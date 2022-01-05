using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Repository.DataContext;
using BookStore.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using BookStore.Repository.Dtos;
using BookStore.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Repository.Helpers;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repository.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;


        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            //var _config = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDto>().ReverseMap());
            //_mapper = new Mapper(_config);
        }
        
        
        public async Task<PagedList<BookDto>> GetAllBooksAsync(UserParams userParams)
        {
            //var records = _context.Book.Select(x => new BookDto()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description
            //}).ToListAsync();

            //return await records;

            //var records = await _context.Book.ToListAsync();
            //return _mapper.Map<List<BookDto>>(records);

            var query = _context.Book.ProjectTo<BookDto>(_mapper.ConfigurationProvider).AsNoTracking();
            return await PagedList<BookDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);

        }

        public async Task<PagedList<BookDto>> BooksSearchAsync(UserParams userParams)
        {
            var query = _context.Book.AsQueryable();
            query = query.Where(b => b.Description.Contains(userParams.search)
                || b.Title.Contains(userParams.search));

            //Sorting Functionality
            //query = userParams.orderParam switch
            //{
            //    "orderbychoice" => query.OrderByDescending(u => u.orderByParam),
            //    _ => query.OrderByDescending(u => u.orderByDefault)-- > this is the defaualt choice to order by
            //};

            return await PagedList<BookDto>.CreateAsync(query
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .AsNoTracking(), userParams.PageNumber, userParams.PageSize);

        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            //var record = _context.Book.Where(x => x.Id == bookId).Select(x => new BookDto()
            //{
            //    Id = x.Id,
            //    Title = x.Title,
            //    Description = x.Description
            //}).FirstOrDefaultAsync();
            //return await record;

            var book = await _context.Book.FindAsync(id);
            return _mapper.Map<BookDto>(book);

            //var book = _context.Book.FindAsync(bookId);
            //return await ProjectTo<BookDto>(_mapper.ConfigurationProvider);



        }


        public async Task<int> AddBookAsync(AddBookDto addBookDto)
        {
            //var book = new Book()
            //{
            //    Id = addBookDto.Id,
            //    Title = addBookDto.Title,
            //    Description = addBookDto.Description

            //};
            //_context.Book.Add(book);
            //await _context.SaveChangesAsync();

            //return book.Id;
            var book = _mapper.Map<Book>(addBookDto);
            _context.Book.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;


        }

        public async Task UpdateBookAsync(int id, UpdateBookDto addBookDto)
        {
            //var book = await _context.Book.FindAsync(bookId);
            //if (book != null)
            //{
            //    book.Title = addBookDto.Title;
            //    book.Description = addBookDto.Description;

            //    await _context.SaveChangesAsync();

            //}

            //var book = new Book()
            //{
            //    Id = id,
            //    Title = addBookDto.Title,
            //    Description = addBookDto.Description
            //};

            var book = await _context.Book.FindAsync(id);
            if (!String.IsNullOrEmpty(addBookDto.Title))
                book.Title = addBookDto.Title;

            if (!String.IsNullOrEmpty(addBookDto.Description))
                book.Description = addBookDto.Description;

            _context.Book.Update(book);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteBookAsync(int id)
        {
            var book = new Book()
            {
                Id =id
            };
            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
        }

    }
}