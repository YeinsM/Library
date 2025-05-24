using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories.Interfaces;
using Library.WebApi.DTOs.Author;
using Library.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController(ApplicationContext context, IGenericRepository<Author> authorRepository) : ControllerBase
    {

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await authorRepository.GetAllAsync();
            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await authorRepository.GetByIdAsync(id);

            if (author == null)
            {
                return NotFound(ApiResponse<Author>.Fail("Autor no encontrado", code: "NOT_FOUND"));
            }

            return Ok(ApiResponse<Author>.Ok(author));
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest(ApiResponse<Author>.Fail("Error en los datos, verifique e intente nuevamente", code: "BAD_REQUEST"));
            }

            context.Entry(author).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return NotFound(ApiResponse<Author>.Fail("Autor no encontrado", code: "NOT_FOUND"));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(CreateAuthorDto dto)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", errors, code: "BAD_REQUEST"));
            }

            var author = new Author {
                Name = dto.Name,
                Nationality = dto.Nationality
            };

            await authorRepository.AddAsync(author);
            await context.SaveChangesAsync();

            return Ok(ApiResponse<Author>.Ok(author, "Autor creado correctamente."));
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound(ApiResponse<Author>.Fail("Autor no encontrado", code: "NOT_FOUND"));
            }

            context.Authors.Remove(author);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return context.Authors.Any(e => e.AuthorId == id);
        }
    }
}
