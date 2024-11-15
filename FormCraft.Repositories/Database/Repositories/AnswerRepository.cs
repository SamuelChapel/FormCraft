﻿using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using FormCraft.Repositories.Contracts.Response;
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

        public async Task AddUserAnswer(AppUserAnswer userAnswer)
        {
            await _context.AppUserAnswer.AddAsync(userAnswer);
            await _context.SaveChangesAsync();
        }
        public async Task<List<AnswerResultResponseRepository>> ChoiceByQuestion(string formId, string questionId)
        {
            return await _context.AppUserAnswer
                .Include(a => a.Answer)
                .ThenInclude(a => a!.Question)
                .ThenInclude(q => q.Form)
                .Where(a => a.Answer!.Question.FormId == formId && a.Answer!.QuestionId == questionId)
                .GroupBy(a => a.AnswerId).Select(g => new AnswerResultResponseRepository(g.Key, g.Count())).ToListAsync();
        }
    }
}
