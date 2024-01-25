using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormCraft.Business.Contracts.Requests.Form
{
    public record CreateFormRequest(
        string Label,
        AppUser Creator, //Change to AppUserId
        int FormTypeId) : IRequest;
}