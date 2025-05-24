using Library.Domain.Entities;
using Library.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(IBookRepository bookRepository, IGenericRepository<Author> authorRepository) : ControllerBase
    {
        [HttpGet("before-2000")]
        public async Task<IActionResult> GetBooksBefore2000()
        {
            var books = await bookRepository.GetAllAsync();
            var filtered = books.Where(b => b.YearPublished < 2000)
                                 .Select(b => new { b.BookId, b.Title, b.YearPublished });

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Book book)
        {
            var author = await authorRepository.GetByIdAsync(book.AuthorId);
            if (author == null)
                return BadRequest();

            await bookRepository.AddAsync(book);
            await bookRepository.SaveChangesAsync();

            return Ok();
        }
    }

}
