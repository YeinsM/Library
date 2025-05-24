using Library.Domain.DTOs.Books;
using Library.Domain.Entities;

namespace Library.Infrastructure.Repositories.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksPublishedBefore2000Async();
        Task AddAsync(CreateBookDto entity);
        Task<bool> AuthorExistsAsync(int authorId);
        Task<IEnumerable<BookWithLoanCountDto>> GetMostLoanedBooksLast6MonthsAsync();
    }
}
