using FormCraft.Business.Contracts.Common;
using FormCraft.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormCraft.Business.Contracts.Response.Form
{
    public record FormResponse(
        string Id,
        AppUser Creator,
        FormType FormType,
        List<Question> Questions,
        Status Status) : IRequest;
}
