using Library.Domain.DTOs.Stats;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class LoanRepository(ApplicationContext context) : ILoanRepository
    {
        public async Task<IEnumerable<Loan>> GetAllAsync() => await context.Loans.AsNoTracking().Include(l => l.Book).ToListAsync();
        public async Task<Loan?> GetByIdAsync(int id) => await context.Loans.FindAsync(id);
        public async Task AddAsync(Loan entity) => await context.Loans.AddAsync(entity);
        public void Update(Loan entity) => context.Loans.Update(entity);
        public void Delete(Loan entity) => context.Loans.Remove(entity);
        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

        public async Task<IEnumerable<LoansByGenreDto>> GetLoansByGenreAsync()
        {
            return await context.Loans
                .Where(l => l.Book.Genre != null)
                .GroupBy(l => l.Book.Genre!)
                .Select(g => new LoansByGenreDto
                {
                    Genre = g.Key,
                    TotalLoans = g.Count()
                })
                .OrderByDescending(x => x.TotalLoans)
                .ToListAsync();
        }

        public async Task<IEnumerable<LoansByAuthorDto>> GetLoansByAuthorAsync()
        {
            return await context.Loans
                .GroupBy(l => new { l.Book.Author.AuthorId, l.Book.Author.Name })
                .Select(g => new LoansByAuthorDto
                {
                    AuthorId = g.Key.AuthorId,
                    AuthorName = g.Key.Name,
                    TotalLoans = g.Count()
                })
                .OrderByDescending(x => x.TotalLoans)
                .ToListAsync();
        }

    }
}
