using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;
using FormCraft.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormCraft.Business.Contracts.Requests.Answer
{
    public record UpdateAnswerRequest(string Id, string Label) :IRequest;
}