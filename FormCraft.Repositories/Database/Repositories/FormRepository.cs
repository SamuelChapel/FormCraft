using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using FormCraft.Repositories.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FormCraft.Repositories.Database.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Form> Create(Form entity)
        {
            await _context.Forms.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Form entity)
            => await _context.Forms.Where(f => f.Id == entity.Id).ExecuteDeleteAsync();

        public async Task<List<Form>> GetAll()
            => await _context.Forms.ToListAsync();

        public async Task<Form?> GetById(string id)
            => await _context.Forms
            .Include(f => f.Questions.OrderBy(q => q.Number))
            .ThenInclude(q => q.Answers)
            .AsSplitQuery()
            .FirstOrDefaultAsync(f => f.Id == id);

        public async Task<Form> Update(Form entity)
        {
            _context.Forms.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Form>> Search(FormTypeEnum? type, StatusEnum? status, string? label, int? order)
        {
            var result = await _context.Forms.Where(f =>
            EF.Functions.Like(f.Label, $"%{label}%") ||
            (f.FormTypeId == type) ||
            (f.StatusId == status)
            ).ToListAsync();

            return order switch
            {
                1 => [.. result.OrderBy(f => f.StatusId)],
                2 => [.. result.OrderBy(f => f.FormTypeId)],
                3 => [.. result.OrderBy(f => f.Label)],
                4 => [.. result.OrderBy(f => f.CreatedAt)],
                5 => [.. result.OrderByDescending(f => f.CreatedAt)],
                _ => result
            };
        }
    }
}
