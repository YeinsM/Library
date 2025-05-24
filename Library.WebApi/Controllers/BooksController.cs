using Library.Domain.DTOs.Books;
using Library.Domain.Entities;
using Library.Infrastructure.Repositories;
using Library.Infrastructure.Repositories.Interfaces;
using Library.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController(IBookRepository bookRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await bookRepository.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Book>>.Ok(books));
        }

        [HttpGet("antes-de-2000")]
        public async Task<IActionResult> GetBooksBefore2000()
        {
            var books = await bookRepository.GetBooksPublishedBefore2000Async();

            return Ok(ApiResponse<IEnumerable<Book>>.Ok(books));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound(ApiResponse<Book>.Fail("Libro no encontrado", code: "NOT_FOUND"));
            }
            return Ok(ApiResponse<Book>.Ok(book));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([FromBody] CreateBookDto book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<object>.Fail("Datos incorrectos",
                    [.. ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)]));
            }

            var author = await bookRepository.AuthorExistsAsync(book.AuthorId);
            if (!author)
                return BadRequest(ApiResponse<object>.Fail("El autor especificado no existe.", code: "NOT_FOUND"));

            await bookRepository.AddAsync(book);
            await bookRepository.SaveChangesAsync();

            return Ok(ApiResponse<CreateBookDto>.Ok(book, "Libro creado correctamente."));
        }

        [HttpGet("populares")]
        public async Task<IActionResult> GetPopularBooks()
        {
            var result = await bookRepository.GetMostLoanedBooksLast6MonthsAsync();
            return Ok(ApiResponse<IEnumerable<BookWithLoanCountDto>>.Ok(result));
        }

    }

}
