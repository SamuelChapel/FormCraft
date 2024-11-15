﻿using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using FormCraft.Repositories.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FormCraft.Repositories.Database.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Question> Create(Question entity)
        {
            await _dbContext.Questions.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(Question entity)
        {
            _dbContext.Questions.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Question>> GetAll()
        {
            return await _dbContext.Questions
                .Include(q => q.Answers)
                .ToListAsync();
        }

        public async Task<Question?> GetById(string id)
        {
            return await _dbContext.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Question> Update(Question entity)
        {
            _dbContext.Questions.Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
