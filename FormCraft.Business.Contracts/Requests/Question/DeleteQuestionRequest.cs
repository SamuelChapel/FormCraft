using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormCraft.Business.Contracts.Requests.Question
{
    public record DeleteQuestionRequest(string Id) : IRequest;
}