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
            => await _context.Forms.Include(f => f.Creator).ToListAsync();

        public async Task<Form?> GetById(string id)
            => await _context.Forms/*.Include(f => f.Creator.UserName)*/
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

        public async Task<List<Form>> Search(string[] status, string[] type, string? label, int? order, string? currentUserId)
        {
            var result = _context.Forms
                .Include(f => f.FormType)
                .Include(f => f.Status)
                .Include(f => f.Creator)
                .AsQueryable();

            if (label is not null)
            {
                result = result.Where(f =>
                f.Label.Contains(label, StringComparison.InvariantCultureIgnoreCase) ||
                f.Creator!.UserName!.Contains(label, StringComparison.InvariantCultureIgnoreCase));
            }

            if (type.Length != 0)
            {
                result = result.Where(f => type.Any(t => f.FormType!.Label == t));
            }

            if (status.Length != 0)
            {
                result = result.Where(f => status.Any(t => f.Status!.Label == t));
            }

            return order switch
            {
                1 => await result.OrderBy(f => f.StatusId).ToListAsync(),
                2 => await result.OrderBy(f => f.FormTypeId).ToListAsync(),
                3 => await result.OrderBy(f => f.Label).ToListAsync(),
                4 => await result.OrderByDescending(f => f.Label).ToListAsync(),
                5 => await result.OrderBy(f => f.CreatedAt).ToListAsync(),
                6 => await result.OrderByDescending(f => f.CreatedAt).ToListAsync(),
                _ => await result.ToListAsync()
            };
        }
    }
}
