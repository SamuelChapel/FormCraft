using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using FormCraft.Repositories.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            => await _context.Forms.Where(f => f.Id == entity.Id).ExecuteDeleteAsync<Form>();

        public async Task<List<Form>> GetAll()
            => await _context.Forms.ToListAsync();

        public async Task<Form?> GetById(string id)
            => await _context.Forms.FirstOrDefaultAsync(f => f.Id == id);

        public async Task<Form> Update(Form entity)
        {
            _context.Forms.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
