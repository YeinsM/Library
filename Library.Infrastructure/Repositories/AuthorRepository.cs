using Library.Domain.Entities;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class AuthorRepository(ApplicationContext _context) : IGenericRepository<Author>
    {
        public async Task<IEnumerable<Author>> GetAllAsync() => await _context.Authors.AsNoTracking().ToListAsync();

        public async Task<Author?> GetByIdAsync(int id) => await _context.Authors.FindAsync(id);

        public async Task AddAsync(Author author) => await _context.AddAsync(author);

        public void Update(Author author) => _context.Authors.Update(author);

        public void Delete(Author author) => _context.Authors.Remove(author);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
