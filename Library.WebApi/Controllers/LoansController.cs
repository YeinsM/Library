using Library.Domain.Entities;
using Library.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoansController(ILoanRepository loanRepository) : ControllerBase
    {
        [HttpGet("not-returned")]
        public async Task<IActionResult> GetNotReturnedLoans()
        {
            var loans = await loanRepository.GetAllAsync();
            var result = loans.Where(l => l.DueDate == null)
                               .Select(l => new
                               {
                                   l.Book.Author.AuthorId,
                                   AuthorName = l.Book.Author.Name,
                                   l.BookId,
                                   BookTitle = l.Book.Title
                               });
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReturnDate(int id, [FromBody] DateTime returnDate)
        {
            var loan = await loanRepository.GetByIdAsync(id);
            if (loan == null)
                return NotFound();

            loan.DueDate = returnDate;
            loanRepository.Update(loan);
            await loanRepository.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loan = await loanRepository.GetByIdAsync(id);
            if (loan == null)
                return NotFound();

            loanRepository.Delete(loan);
            await loanRepository.SaveChangesAsync();

            return Ok();
        }
    }

}
