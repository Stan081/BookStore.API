using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.API.Extentions;
using BookStore.Repository.Dtos;
using BookStore.Repository.Helpers;
using BookStore.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks([FromQuery] UserParams userParams)
        {
            var result = await _bookRepository.GetAllBooksAsync(userParams);
            Response.AddPaginationHeader(result.CurrentPage, result.PageSize, result.Totalcount, result.TotalPages);
            return Ok(result);

        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDto>>> BooksSearch([FromBody] UserParams userParams)
        {
            var result = await _bookRepository.BooksSearchAsync(userParams);
            Response.AddPaginationHeader(result.CurrentPage, result.PageSize, result.Totalcount, result.TotalPages);
            return Ok(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var mybook = await _bookRepository.GetBookByIdAsync(id);
            if (mybook == null) {
                return NotFound();
            }
            return Ok(mybook);
        }

        [Authorize(Roles = "Manager, Admin")]
        [HttpPost("add-book")]
        public async Task<IActionResult> AddBook([FromBody] AddBookDto addBookDto)
        {
            if (ModelState.IsValid)
            {
                var id = await _bookRepository.AddBookAsync(addBookDto);
                return CreatedAtAction(nameof(GetBookById), new { id, controller = "books" }, id);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("update-book/{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookDto addBookDto, [FromRoute] int id)
         {
            await _bookRepository.UpdateBookAsync(id, addBookDto);
            return Ok();
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("delete-book/{id}")]
        public async Task<IActionResult> Deletebook([FromRoute] int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return Ok();
        }
    }

}
