using Library.Domain.DTOs.Stats;
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
    public class LoansController(ILoanRepository loanRepository) : ControllerBase
    {
        [HttpGet("no-retornados")]
        public async Task<IActionResult> GetNotReturnedLoans()
        {
            var loans = await loanRepository.GetAllAsync();
            var result = loans.Where(l => l.DueDate == null)
                               .Select(l => new
                               {
                                   l.BookId,
                                   BookTitle = l.Book.Title
                               });
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateReturnDate(int id, [FromBody] DateTime returnDate)
        {
            var loan = await loanRepository.GetByIdAsync(id);
            if (loan == null)
                return NotFound();

            loan.DueDate = returnDate;
            loanRepository.Update(loan);
            await loanRepository.SaveChangesAsync();

            return Ok(ApiResponse<Loan>.Ok(loan, "Fecha de devolución actualizada."));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var loan = await loanRepository.GetByIdAsync(id);
            if (loan == null)
                return NotFound(ApiResponse<object>.Fail("Préstamo no encontrado.", code: "NOT_FOUND"));

            loanRepository.Delete(loan);
            await loanRepository.SaveChangesAsync();

            return Ok(ApiResponse<object>.Ok("Préstamo eliminado correctamente."));
        }

        [HttpGet("prestamos-por-genero")]
        public async Task<IActionResult> GetLoansByGenre()
        {
            var result = await loanRepository.GetLoansByGenreAsync();
            return Ok(ApiResponse<IEnumerable<LoansByGenreDto>>.Ok(result));
        }

        [HttpGet("prestamos-por-autor")]
        public async Task<IActionResult> GetLoansByAuthor()
        {
            var result = await loanRepository.GetLoansByAuthorAsync();
            return Ok(ApiResponse<IEnumerable<LoansByAuthorDto>>.Ok(result));
        }
    }

}
