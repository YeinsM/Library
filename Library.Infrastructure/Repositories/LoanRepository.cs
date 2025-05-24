using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class LoanRepository(ApplicationContext context) : ILoanRepository
    {
        public async Task<IEnumerable<Loan>> GetAllAsync() => await context.Loans.AsNoTracking().ToListAsync();
        public async Task<Loan?> GetByIdAsync(int id) => await context.Loans.FindAsync(id);
        public async Task AddAsync(Loan entity) => await context.Loans.AddAsync(entity);
        public void Update(Loan entity) => context.Loans.Update(entity);
        public void Delete(Loan entity) => context.Loans.Remove(entity);
        public async Task SaveChangesAsync() => await context.SaveChangesAsync();
    }
}
