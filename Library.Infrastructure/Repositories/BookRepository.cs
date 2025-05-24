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
        public void Update(Book entity) => context.Books.Update(entity);
        public void Delete(Book entity) => context.Books.Remove(entity);
        public async Task SaveChangesAsync() => await context.SaveChangesAsync();
    }
}