using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using FormCraft.Repositories.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FormCraft.Repositories.Database.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _context;

        public AnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Answer> Create(Answer entity)
        {
            await _context.Answers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(Answer entity)
            => await _context.Answers.Where(a => a.Id == entity.Id).ExecuteDeleteAsync<Answer>();

        public async Task<List<Answer>> GetAll()
            => await _context.Answers.ToListAsync();

        public async Task<Answer?> GetById(string id)
            => await _context.Answers.FirstOrDefaultAsync(a => a.Id == id);

        public async Task<Answer> Update(Answer entity)
        {
            _context.Answers.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
