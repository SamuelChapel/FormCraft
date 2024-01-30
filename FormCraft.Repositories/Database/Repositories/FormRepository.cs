using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using FormCraft.Repositories.Database.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FormCraft.Repositories.Database.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public FormRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            //UserManager to get own form

            return order switch
            {
                1 => result.OrderBy(f => f.StatusId).ToList(),
                2 => result.OrderBy(f => f.FormTypeId).ToList(),
                3 => result.OrderBy(f => f.Label).ToList(),
                4 => result.OrderBy(f => f.CreatedAt).ToList(),
                5 => result.OrderByDescending(f => f.CreatedAt).ToList(),
                _ => result
            };
        }
    }
}
