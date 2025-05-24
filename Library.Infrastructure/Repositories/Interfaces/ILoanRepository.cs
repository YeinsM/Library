using Library.Domain.DTOs.Stats;
using Library.Domain.Entities;

namespace Library.Infrastructure.Repositories.Interfaces
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
        Task<IEnumerable<LoansByGenreDto>> GetLoansByGenreAsync();
        Task<IEnumerable<LoansByAuthorDto>> GetLoansByAuthorAsync();

    }
}
