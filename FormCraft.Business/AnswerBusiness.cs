using AutoMapper;
using FormCraft.Business.Contracts;
using FormCraft.Business.Contracts.Requests.Answer;
using FormCraft.Business.Contracts.Response.Answer;
using FormCraft.Entities;
using FormCraft.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Business
{
    public class AnswerBusiness : IAnswerBusiness
    {
        private readonly IAnswerRepository _answerRepository;

        private readonly IMapper _mapper;

        public AnswerBusiness(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }

        public async Task<AnswerResponse> Create(CreateAnswerRequest entity)
        {
            var answer = _mapper.Map<Answer>(entity);
            await _answerRepository.Create(answer);

            return _mapper.Map<AnswerResponse>(answer);
        }

        public async Task Delete(DeleteAnswerRequest entity)
        {
            var answer = _mapper.Map<Answer>(entity);
            await _answerRepository.Delete(answer);
        }

        public async Task<List<AnswerResponse>> GetAll()
            => _mapper.Map<List<AnswerResponse>>(await _answerRepository.GetAll());

        public async Task<AnswerResponse?> GetById(Guid id)
            => _mapper.Map<AnswerResponse>(await _answerRepository.GetById(id));

        public async Task<AnswerResponse> Update(UpdateAnswerRequest entity)
        {
            var answerToUpdate = await GetById(entity.Id);

            answerToUpdate.Label = entity.Label;
            answerToUpdate.Question = entity.Question;

            await _answerRepository.Update(_mapper.Map<Answer>(answerToUpdate)); //Type AnswerResponse n'a pas d'ID, comment convertir en Answer

            return _mapper.Map<AnswerResponse>(answerToUpdate);
        }
    }
}
