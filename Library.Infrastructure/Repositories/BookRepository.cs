using Library.Domain.DTOs.Books;
using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository(ApplicationContext context) : IBookRepository
    {
        public async Task<IEnumerable<Book>> GetAllAsync() => await context.Books.AsNoTracking().ToListAsync();
        public async Task<Book?> GetByIdAsync(int id) => await context.Books.FindAsync(id);
        public async Task AddAsync(Book entity) => await context.Books.AddAsync(entity);
        public async Task AddAsync(CreateBookDto entity)
        {
            var book = new Book
            {
                Title = entity.Title,
                AuthorId = entity.AuthorId,
                YearPublished = entity.YearPublished,
                Genre = entity.Genre
            };
            await context.Books.AddAsync(book);
        }
        public void Update(Book entity) => context.Books.Update(entity);
        public void Delete(Book entity) => context.Books.Remove(entity);
        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

        public async Task<IEnumerable<Book>> GetBooksPublishedBefore2000Async()
        {
            return await context.Books
                .AsNoTracking()
                .Where(b => b.YearPublished < 2000)
                .ToListAsync();
        }

        public async Task<bool> AuthorExistsAsync(int authorId)
        {
            return await context.Authors.AnyAsync(a => a.AuthorId == authorId);
        }

        public async Task<IEnumerable<BookWithLoanCountDto>> GetMostLoanedBooksLast6MonthsAsync()
        {
            var sixMonthsAgo = DateTime.UtcNow.AddMonths(-6);

            return await context.Loans
                .Where(l => l.LoanDate >= sixMonthsAgo)
                .GroupBy(l => l.Book)
                .Select(g => new BookWithLoanCountDto
                {
                    BookId = g.Key.BookId,
                    Title = g.Key.Title,
                    TotalLoans = g.Count()
                })
                .OrderByDescending(x => x.TotalLoans)
                .ToListAsync();
        }

    }
}