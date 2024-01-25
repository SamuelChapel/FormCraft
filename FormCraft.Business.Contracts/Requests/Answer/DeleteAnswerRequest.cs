using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormCraft.Business.Contracts.Requests.Answer
{
    public record DeleteAnswerRequest(string Id) : IRequest;
}